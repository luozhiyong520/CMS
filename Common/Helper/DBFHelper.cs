using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Common
{
    public class DBFHelper
    {
        public const int DBFHeaderSize = 32;
        /**/
        /* �汾��־
                 0x02    FoxBASE  
                0x03    FoxBASE+/dBASE III PLUS���ޱ�ע  
                0x30    Visual FoxPro  
                0x43    dBASE IV SQL ���ļ����ޱ�ע  
                0x63    dBASE IV SQL ϵͳ�ļ����ޱ�ע  
                0x83    FoxBASE+/dBASE III PLUS���б�ע  
                0x8B    dBASE IV �б�ע  
                0xCB    dBASE IV SQL ���ļ����б�ע  
                0xF5    FoxPro 2.x�������汾���б�ע  
                0xFB    FoxBASE  
    */
        public sbyte Version;
        /**/
        /* �������� */
        public byte LastModifyYear;
        /**/
        /* �������� */
        public byte LastModifyMonth;
        /**/
        /* �������� */
        public byte LastModifyDay;
        /**/
        /* �ļ��������ܼ�¼�� */
        public uint RecordCount;
        /**/
        /* ��һ����¼��ƫ��ֵ�����ֵҲ���Ա�ʾ�ļ�ͷ���� */
        public ushort HeaderLength;
        /**/
        /* ��¼���ȣ�����ɾ����־*/
        public ushort RecordLength;
        /**/
        /* ���� */
        public byte[] Reserved = new byte[16];
        /**/
        /* ��ı�־
                 0x01���� .cdx �ṹ���ļ�
                0x02�ļ�������ע
                0x04�ļ������ݿ⣨.dbc�� 
                ��־��OR 
    */
        public sbyte TableFlag;
        /**/
        /* ����ҳ��־ */
        public sbyte CodePageFlag;
        /**/
        /* ���� */
        public byte[] Reserved2 = new byte[2];
    }
    internal class DBFField
    {
        public const int DBFFieldSize = 32;
        /**/
        /* �ֶ����� */
        public byte[] Name = new byte[11];
        /**/
        /* �ֶ����� C - �ַ���  
                Y - ������  
                N - ��ֵ��  
                F - ������  
                D - ������  
                T - ����ʱ����  
                B - ˫������  
                I - ����  
                L - �߼��� 
                M - ��ע��  
                G - ͨ����  
                C - �ַ��ͣ������ƣ� 
                M - ��ע�ͣ������ƣ� 
                P - ͼƬ��  
    */
        public sbyte Type;
        /**/
        /* �ֶ�ƫ���� */
        public uint Offset;
        /**/
        /* �ֶγ��� */
        public byte Length;
        /**/
        /* ������С�����ֳ��� */
        public byte Precision;
        /**/
        /* ���� */
        public byte[] Reserved = new byte[2];
        /**/
        /* dBASE IV work area id */
        public sbyte DbaseivID;
        /**/
        /* */
        public byte[] Reserved2 = new byte[10];
        /**/
        /* */
        public sbyte ProductionIndex;
    }
    /**/
    /// <summary>
    /// .dbf�ļ�������
    /// </summary>
    public class DBFFile : IDisposable
    {
        private const string MSG_OPEN_FILE_FAIL = "���ܴ��ļ�{0}";

        private bool _isFileOpened;
        private byte[] _recordBuffer;
        private DBFField[] _dbfFields;
        private System.IO.FileStream _fileStream = null;
        private System.IO.BinaryReader _binaryReader = null;
        private string _fileName = string.Empty;
        private uint _fieldCount = 0;
        private int _recordIndex = -1;
        private uint _recordCount = 0;
        private DBFHelper _dbfHeader = null;
        private string _tableName = string.Empty;

        /**/
        /// <summary>
        /// ���캯��
        /// </summary>
        public DBFFile()
        {
        }

        /**/
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="fileName"></param>
        public DBFFile(string fileName)
        {
            if (null != fileName && 0 != fileName.Length)
                this._fileName = fileName;
        }

        /**/
        /// <summary>
        /// ������������ʹ�õ���Դ��
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._recordBuffer = null;
                this._dbfHeader = null;
                this._dbfFields = null;

                if (this.IsFileOpened && null != this._fileStream)
                {
                    this._fileStream.Close();
                    this._binaryReader.Close();
                }
                this._fileStream = null;
                this._binaryReader = null;

                this._isFileOpened = false;
                this._fieldCount = 0;
                this._recordCount = 0;
                this._recordIndex = -1;
            }
        }

        /**/
        /// <summary>
        /// ��dbf�ļ�
        /// </summary>
        /// <returns></returns>
        public bool Open()
        {
            try
            {
                return this.Open(null);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /**/
        /// <summary>
        /// ��dbf�ļ�
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool Open(string fileName)
        {
            if (null != fileName)
                this._fileName = fileName;

            bool ret = false;

            try
            {
                if (!this.OpenFile())
                {
                    // ���ܴ�dbf�ļ����׳����ܴ��ļ��쳣
                    throw new Exception(string.Format(MSG_OPEN_FILE_FAIL, this._fileName));
                }

                // ��ȡ�ļ�ͷ��Ϣ
                ret = this.ReadFileHeader();

                // ��ȡ�����ֶ���Ϣ
                if (ret)
                    ret = this.ReadFields();

                // �����¼������
                if (ret && null == this._recordBuffer)
                {
                    this._recordBuffer = new byte[this._dbfHeader.RecordLength];

                    if (null == this._recordBuffer)
                        ret = false;
                }

                // ������ļ����ȡ��Ϣ���ɹ����ر�dbf�ļ�
                if (!ret)
                    this.Close();
            }
            catch (Exception e)
            {
                throw e;
            }

            // ���õ�ǰ��¼����Ϊ
            this._recordIndex = -1;

            // ���ش��ļ����Ҷ�ȡ��Ϣ�ĳɹ�״̬
            return ret;
        }
        /// <summary>
        /// ���ֶ�����ת��Ϊϵͳ��������
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private Type FieldTypeToColumnType(sbyte type)
        {
            switch (type)
            {
                // C - �ַ��͡��ַ��ͣ������ƣ�
                case (sbyte)'C':
                    return typeof(System.String);

                // Y - ������
                case (sbyte)'Y':
                    return typeof(System.Decimal);    // ��Ȼdbf��'Y'����Ϊ64λ������Double�ľ��Ȳ���������ָ��Decimal

                // N - ��ֵ��
                case (sbyte)'N':
                    return typeof(System.Decimal);    // dbf��'N'�ľ��ȿ��Դﵽ19��������Decimal

                // F - ������
                case (sbyte)'F':
                    return typeof(System.Decimal);    // dbf��'F'�ľ��ȿ��Դﵽ19��������Decimal

                // D - ������
                case (sbyte)'D':
                    return typeof(System.DateTime);

                // T - ����ʱ����
                case (sbyte)'T':
                    return typeof(System.DateTime);

                // B - ˫������
                case (sbyte)'B':
                    return typeof(System.Double);

                // I - ����
                case (sbyte)'I':
                    return typeof(System.Int32);

                // L - �߼���
                case (sbyte)'L':
                    return typeof(System.Boolean);

                // M - ��ע�͡���ע�ͣ������ƣ�
                case (sbyte)'M':
                    return typeof(System.String);

                // G - ͨ����
                case (sbyte)'G':
                    return typeof(System.String);

                // P - ͼƬ��
                case (sbyte)'P':
                    return typeof(System.String);

                // ȱʡ�ַ�����
                default:
                    return typeof(System.String);

            }
        }
        /**/
        /// <summary>
        /// ��ȡdbf���ļ���Ӧ��DataSet
        /// </summary>
        /// <returns></returns>
        public System.Data.DataSet GetDataSet()
        {
            // ȷ���ļ��Ѿ���
            if (!this.IsFileOpened || (this.IsBOF && this.IsEOF))
                return null;

            // ������
            System.Data.DataSet ds = new System.Data.DataSet();
            System.Data.DataTable dt = new System.Data.DataTable(this._tableName);

            try
            {
                // ��ӱ����
                for (uint i = 0; i < this._fieldCount; i++)
                {
                    System.Data.DataColumn col = new System.Data.DataColumn();
                    string colText = string.Empty;

                    // ��ȡ�������б���
                    if (this.GetFieldName(i, ref colText))
                    {
                        col.ColumnName = colText;
                        col.Caption = colText;
                    }

                    // ����������
                    col.DataType = FieldTypeToColumnType(this._dbfFields[i].Type);


                    // �������Ϣ
                    dt.Columns.Add(col);
                }

                // ������еļ�¼��Ϣ
                this.MoveFirst();
                while (!this.IsEOF)
                {
                    // �����¼�¼��
                    System.Data.DataRow row = dt.NewRow();

                    // ѭ����ȡ�����ֶ���Ϣ����ӵ��µļ�¼����
                    for (uint i = 0; i < this._fieldCount; i++)
                    {
                        string temp = string.Empty;

                        // ��ȡ�ֶ�ֵ�ɹ������ӵ���¼����
                        if (this.GetFieldValue(i, ref temp))
                        {
                            // �����ȡ���ֶ�ֵΪ�գ�����DataTable���ֶ�ֵΪDBNull
                            //                            if (string.Empty != temp)
                            row[(int)i] = temp;
                            //                            else
                            //                                row[(int)i] = System.DBNull.Value;
                        }

                    }

                    // ��Ӽ�¼��
                    dt.Rows.Add(row);

                    // ���Ƽ�¼
                    this.MoveNext();

                    if (this.IsFileEnd)
                    {
                        break;
                    }                    
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            ds.Tables.Add(dt);
            return ds;
        }

        /**/
        /// <summary>
        /// ��ȡ��Ӧ������Ŵ����ֶ�����
        /// </summary>
        /// <param name="fieldIndex"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public bool GetFieldName(uint fieldIndex, ref string fieldName)
        {
            // ȷ���ļ��Ѿ���
            if (!this.IsFileOpened)
                return false;

            // �����߽���
            if (fieldIndex >= this._fieldCount)
            {
                fieldName = string.Empty;
                return false;
            }

            try
            {
                // ������
                fieldName = System.Text.Encoding.Default.GetString(this._dbfFields[fieldIndex].Name);
                //ȥ��ĩβ�Ŀ��ַ���־
                int i = fieldName.IndexOf('\0');
                if (i > 0)
                {
                    fieldName = fieldName.Substring(0, i);
                }
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /**/
        /// <summary>
        /// ��ȡ��Ӧ������Ŵ����ֶ��ı�ֵ
        /// </summary>
        /// <param name="fieldIndex"></param>
        /// <param name="fieldValue"></param>
        /// <returns></returns>
        public bool GetFieldValue(uint fieldIndex, ref string fieldValue)
        {
            // ��ȫ�Լ��
            if (!this.IsFileOpened || this.IsBOF || this.IsEOF || null == this._recordBuffer)
                return false;

            // �ֶ������������ֵ
            if (fieldIndex >= this._fieldCount)
            {
                fieldValue = string.Empty;
                return false;
            }

            try
            {
                // �Ӽ�¼�������л�ȡ��Ӧ�ֶε�byte[]

                //uint offset = this._dbfFields[fieldIndex].Offset;
                uint offset = 0;
                if (offset == 0)
                {
                    for (int i = 0; i < fieldIndex; i++)
                    {
                        offset += this._dbfFields[i].Length;
                    }
                }
                byte[] tmp = GetSubBytes(this._recordBuffer, offset, this._dbfFields[fieldIndex].Length);


                //
                // ��ʼbyte����ķ��������
                //
                if (((sbyte)'I') == this._dbfFields[fieldIndex].Type)
                {
                    // �����ֶεķ��������
                    int num1 = Byte2Int32(tmp);
                    fieldValue = num1.ToString();
                }
                else if (((sbyte)'B') == this._dbfFields[fieldIndex].Type)
                {
                    // ˫�������ֶεķ��������
                    double num1 = Byte2Double(tmp);
                    fieldValue = num1.ToString();
                }
                else if (((sbyte)'Y') == this._dbfFields[fieldIndex].Type)
                {
                    //
                    // �������ֶεķ��������
                    // �����ʹ洢��ʱ��Ӧ���ǽ��ֶ�ֵ�Ŵ�10000�������long�ʹ洢
                    // �����Ƚ�byte����ָ���long������ֵ��Ȼ����С10000����
                    //
                    long num1 = Byte2Int64(tmp);
                    fieldValue = (((decimal)num1) / 10000).ToString();
                }
                else if (((sbyte)'D') == this._dbfFields[fieldIndex].Type)
                {
                    //
                    // �������ֶεķ��������
                    //
                    DateTime date1 = Byte2Date(tmp);

                    fieldValue = date1.ToString();

                }
                else if (((sbyte)'T') == this._dbfFields[fieldIndex].Type)
                {
                    //
                    // ����ʱ�����ֶεķ��������
                    //
                    DateTime date1 = Byte2DateTime(tmp);

                    fieldValue = date1.ToString();

                }
                else
                {
                    // �����ֶ�ֵ���ַ��洢��ʽ���ƣ�ֱ�ӷ�������ַ����Ϳ���
                    fieldValue = System.Text.Encoding.Default.GetString(tmp);
                }

                // �����ֶ���ֵ����β�ո�
                fieldValue = fieldValue.Trim();

                // ������Ӷ���������ֵ����ͣ���һ�������ֶ�ֵ
                if (((sbyte)'N') == this._dbfFields[fieldIndex].Type ||    // N - ��ֵ��
                    ((sbyte)'F') == this._dbfFields[fieldIndex].Type)    // F - ������
                {
                    if (0 == fieldValue.Length)
                        // �ֶ�ֵΪ�գ�����Ϊ0
                        fieldValue = "0";
                    else if ("." == fieldValue)
                        // �ֶ�ֵΪ"."��Ҳ����Ϊ0
                        fieldValue = "0";
                    else
                    {
                        // ���ֶ�ֵ��ת��ΪDecimal����Ȼ����ת��Ϊ�ַ����ͣ��������ơ�.000��������
                        // �������ת����Ϊ0
                        try
                        {
                            fieldValue = System.Convert.ToDecimal(fieldValue).ToString();
                        }
                        catch (Exception)
                        {
                            fieldValue = "0";
                        }
                    }
                }
                // �߼����ֶ�
                else if (((sbyte)'L') == this._dbfFields[fieldIndex].Type)    // L - �߼���
                {
                    if ("T" != fieldValue)
                        fieldValue = "false";
                    else
                        fieldValue = "true";
                }
                // �������ֶ�
                else if (((sbyte)'D') == this._dbfFields[fieldIndex].Type ||    // D - ������
                    ((sbyte)'T') == this._dbfFields[fieldIndex].Type)    // T - ����ʱ����
                {
                    // ��ʱ�����κδ���
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /**/
        /// <summary>
        /// ��ȡbuf��������
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private static byte[] GetSubBytes(byte[] buf, uint startIndex, long length)
        {
            // �������
            if (null == buf)
            {
                throw new ArgumentNullException("buf");
            }
            if (startIndex >= buf.Length)
            {
                throw new ArgumentOutOfRangeException("startIndex");
            }
            if (0 == length)
            {
                throw new ArgumentOutOfRangeException("length", "����length�������0");
            }
            if (length > buf.Length - startIndex)
            {
                // ������ĳ��ȳ�����startIndex��bufĩβ�ĳ���ʱ������Ϊʣ�೤��
                length = buf.Length - startIndex;
            }

            byte[] target = new byte[length];

            // ��λ����
            for (uint i = 0; i < length; i++)
            {
                target[i] = buf[startIndex + i];
            }

            // ����buf��������
            return target;
        }

        /**/
        /// <summary>
        /// byte����洢����ֵת��Ϊint32����
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        private static int Byte2Int32(byte[] buf)
        {
            // �������
            if (null == buf)
            {
                // ����Ϊ��
                throw new ArgumentNullException("buf");
            }
            if (4 != buf.Length)
            {
                // �������buf�ĳ��Ȳ�Ϊ4���׳������쳣
                throw new ArgumentException("����Byte2Int32(byte[])�Ĳ��������ǳ���Ϊ4����Чbyte����", "buf");
            }

            // byte[] ����� int
            return (int)((((buf[0] & 0xff) | (buf[1] << 8)) | (buf[2] << 0x10)) | (buf[3] << 0x18));
        }

        /**/
        /// <summary>
        /// byte����洢����ֵת��Ϊint64����
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        private static long Byte2Int64(byte[] buf)
        {
            // �������
            if (null == buf)
            {
                // ����Ϊ��
                throw new ArgumentNullException("buf");
            }
            if (8 != buf.Length)
            {
                // �������buf�ĳ��Ȳ�Ϊ4���׳������쳣
                throw new ArgumentException("����Byte2Int64(byte[])�Ĳ��������ǳ���Ϊ8����Чbyte����", "buf");
            }

            // byte[] ����� long
            uint num1 = (uint)(((buf[0] | (buf[1] << 8)) | (buf[2] << 0x10)) | (buf[3] << 0x18));
            uint num2 = (uint)(((buf[4] | (buf[5] << 8)) | (buf[6] << 0x10)) | (buf[7] << 0x18));

            return (long)(((ulong)num2 << 0x20) | num1);
        }

        /**/
        /// <summary>
        /// byte����洢����ֵת��Ϊdouble����
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        private static double Byte2Double(byte[] buf)
        {
            // �������
            if (null == buf)
            {
                // ����Ϊ��
                throw new ArgumentNullException("buf");
            }
            if (8 != buf.Length)
            {
                // �������buf�ĳ��Ȳ�Ϊ8���׳������쳣
                throw new ArgumentException("����Byte2Double(byte[])�Ĳ��������ǳ���Ϊ8����Чbyte����", "buf");
            }

            double num1 = 0;
            unsafe
            {    // ��unsafe������ʹ��ָ��
                fixed (byte* numRef1 = buf)
                {
                    num1 = *((double*)numRef1);
                }
            }

            return num1;
        }

        /**/
        /// <summary>
        /// byte����洢����ֵת��Ϊֻ�������ڵ�DateTime����
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        private static DateTime Byte2Date(byte[] buf)
        {
            // �������
            if (null == buf)
            {
                // ����Ϊ��
                throw new ArgumentNullException("buf");
            }
            if (8 != buf.Length)
            {
                // �������buf�ĳ��Ȳ�Ϊ8���׳������쳣
                throw new ArgumentException("����Byte2DateTime(byte[])�Ĳ��������ǳ���Ϊ8����Чbyte����", "buf");
            }

            try
            {
                string str1 = System.Text.Encoding.Default.GetString(buf);
                str1 = str1.Trim();
                if (str1.Length < 8)
                {
                    return new DateTime();
                }
                int year = int.Parse(str1.Substring(0, 4));
                int month = int.Parse(str1.Substring(4, 2));
                int day = int.Parse(str1.Substring(6, 2));
                return new DateTime(year, month, day);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /**/
        /// <summary>
        /// byte����洢����ֵת��ΪDateTime����
        /// byte����Ϊ8λ��ǰ32λ�洢���ڵ������������32λ�洢ʱ����ܺ�����
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        private static DateTime Byte2DateTime(byte[] buf)
        {
            // �������
            if (null == buf)
            {
                // ����Ϊ��
                throw new ArgumentNullException("buf");
            }
            if (8 != buf.Length)
            {
                // �������buf�ĳ��Ȳ�Ϊ8���׳������쳣
                throw new ArgumentException("����Byte2DateTime(byte[])�Ĳ��������ǳ���Ϊ8����Чbyte����", "buf");
            }

            try
            {
                byte[] tmp = GetSubBytes(buf, 0, 4);
                tmp.Initialize();
                // ��ȡ����
                int days = Byte2Int32(tmp);

                // ��ȡ������
                tmp = GetSubBytes(buf, 4, 4);
                int milliSeconds = Byte2Int32(tmp);

                // ����С����ʱ��Ļ�������Ӹջ�ȡ�������ͺ��������õ������ֶ���ֵ
                DateTime dm1 = DateTime.MinValue;
                dm1 = dm1.AddDays(days - 1721426);
                dm1 = dm1.AddMilliseconds((double)milliSeconds);

                return dm1;
            }
            catch
            {
                return new DateTime();
            }
        }

        /**/
        /// <summary>
        /// ��ȡ��Ӧ�ֶε��ı�ֵ
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="fieldValue"></param>
        /// <returns></returns>
        public bool GetFieldValue(string fieldName, string fieldValue)
        {
            // ȷ��Ŀ���ļ��Ѿ���
            if (!this.IsFileOpened)
                return false;

            if (this.IsBOF || this.IsEOF)
                return false;

            if (null == this._recordBuffer || null == fieldName || 0 == fieldName.Length)
                return false;

            // ��ȡ�ֶ����Ƶ�����
            int fieldIndex = GetFieldIndex(fieldName);

            if (-1 == fieldIndex)
            {
                fieldValue = string.Empty;
                return false;
            }

            try
            {
                // ���ظ����ֶ�������ȡ���ֶ��ı�ֵ
                return GetFieldValue((uint)fieldIndex, ref fieldValue);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /**/
        /// <summary>
        /// ��ȡ��ǰ��¼���ı�ֵ
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public bool GetRecordValue(ref string record)
        {
            // ��ȫ�Լ��
            if (!this.IsFileOpened || this.IsBOF || this.IsEOF || null == this._recordBuffer)
                return false;

            try
            {
                // ������
                record = System.Text.Encoding.Default.GetString(this._recordBuffer);

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /**/
        /// <summary>
        /// ����¼ָ���ƶ�����һ����¼
        /// </summary>
        public void MoveFirst()
        {
            // ȷ��Ŀ���ļ��Ѿ���
            if (!this.IsFileOpened)
                return;

            if (this.IsBOF && this.IsEOF)
                return;

            // �������õ�ǰ��¼������
            this._recordIndex = 0;

            try
            {
                // ��ȡ��ǰ��¼��Ϣ
                ReadCurrentRecord();
            }
            catch (Exception e)
            {
                throw e;
            }

            return;
        }

        /**/
        /// <summary>
        /// ����¼ָ��ǰ��һ����¼
        /// </summary>
        public void MovePrevious()
        {
            // ȷ��Ŀ���ļ��Ѿ���
            if (!this.IsFileOpened)
                return;

            if (this.IsBOF)
                return;

            // �������õ�ǰ��¼������
            this._recordIndex -= 1;

            try
            {
                // ��ȡ��ǰ��¼��Ϣ
                ReadCurrentRecord();
            }
            catch (Exception e)
            {
                throw e;
            }

            return;
        }

        /**/
        /// <summary>
        /// ����¼ָ�����һ����¼
        /// </summary>
        public void MoveNext()
        {
            // ȷ��Ŀ���ļ��Ѿ���
            if (!this.IsFileOpened)
                return;

            if (this.IsEOF)
                return;

            // �������õ�ǰ��¼������
            this._recordIndex += 1;

            try
            {
                // ��ȡ��ǰ��¼��Ϣ
                ReadCurrentRecord();
            }
            catch (Exception e)
            {
                throw e;
            }

            return;
        }

        /**/
        /// <summary>
        /// ����¼ָ���ƶ������һ����¼
        /// </summary>
        public void MoveLast()
        {
            // ȷ��Ŀ���ļ��Ѿ���
            if (!this.IsFileOpened)
                return;

            if (this.IsBOF && this.IsEOF)
                return;

            // �������õ�ǰ��¼������
            this._recordIndex = (int)this._recordCount - 1;

            try
            {
                // ��ȡ��ǰ��¼��Ϣ
                ReadCurrentRecord();
            }
            catch (Exception e)
            {
                throw e;
            }

            return;
        }

        /**/
        /// <summary>
        /// �ر�dbf�ļ�
        /// </summary>
        public void Close()
        {
            this.Dispose(true);
        }

        /**/
        /// <summary>
        /// �����ֶ����ƻ�ȡ�ֶε�����ֵ
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        private int GetFieldIndex(string fieldName)
        {
            // ȷ���ļ��Ѿ���
            if (!this.IsFileOpened)
                return -1;

            // �ֶ�������Ч�Լ��
            if (null == fieldName || 0 == fieldName.Length)
                return -1;

            int index = -1;
            string dest;

            fieldName = fieldName.Trim();

            // ���������ֶ�������Ϣ��������fieldNameƥ�����Ŀ
            for (uint i = 0; i < this._fieldCount; i++)
            {
                dest = System.Text.Encoding.Default.GetString(this._dbfFields[i].Name);
                dest = dest.Trim();

                // ��鵱ǰ�ֶ�������ָ�����ֶ������Ƿ�ƥ��
                if (fieldName.Equals(dest))
                {
                    index = (int)i;
                    break;
                }
            }

            return index;
        }

        /**/
        /// <summary>
        /// ��dbf�ļ�
        /// </summary>
        /// <returns></returns>
        private bool OpenFile()
        {
            // ����ļ��Ѿ��򿪣����ȹر�Ȼ�����´�
            if (this.IsFileOpened)
            {
                this.Close();
            }

            // У���ļ���
            if (null == this._fileName || 0 == this._fileName.Length)
            {
                return false;
            }

            this._isFileOpened = false;

            try
            {
                // ��dbf�ļ�����ȡ�ļ�������
                this._fileStream = new FileStream(this._fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                //this._fileStream = File.Open(this._fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                // ʹ�û�ȡ���ļ�������������ƶ�ȡ������
                this._binaryReader = new BinaryReader(this._fileStream, System.Text.Encoding.Default);

                this._isFileOpened = true;
                this._tableName = System.IO.Path.GetFileNameWithoutExtension(this._fileName);
            }
            catch (Exception e)
            {
                throw e;
            }

            return this._isFileOpened;
        }

        /**/
        /// <summary>
        /// ��ȡ��ǰ��¼��Ϣ
        /// </summary>
        private void ReadCurrentRecord()
        {
            // ȷ��Ŀ���ļ��Ѿ���
            if (!this.IsFileOpened)
            {
                return;
            }

            if (this.IsBOF && this.IsEOF)
            {
                return;
            }

            try
            {
                this._fileStream.Seek(this._dbfHeader.HeaderLength + this._dbfHeader.RecordLength * this._recordIndex + 1, SeekOrigin.Begin);
                this._recordBuffer = this._binaryReader.ReadBytes(this._dbfHeader.RecordLength);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /**/
        /// <summary>
        /// ��dbf�ļ��ж�ȡ�����ֶ���Ϣ
        /// </summary>
        /// <returns></returns>
        private bool ReadFields()
        {
            // ȷ��Ŀ���ļ��Ѿ���
            if (!this.IsFileOpened)
                return false;

            // ��������ļ�ͷ������Ϣ
            if (null == this._dbfHeader)
                return false;

            // ���Թ����ֶ���Ϣ��������
            if (null == this._dbfFields)
                this._dbfFields = new DBFField[this._fieldCount];

            try
            {
                // ��λ�ֶ���Ϣ�ṹ�����
                this._fileStream.Seek(DBFHelper.DBFHeaderSize, SeekOrigin.Begin);

                // ��ȡ�����ֶ���Ϣ
                for (int i = 0; i < this._fieldCount; i++)
                {
                    this._dbfFields[i] = new DBFField();
                    this._dbfFields[i].Name = this._binaryReader.ReadBytes(11);
                    this._dbfFields[i].Type = this._binaryReader.ReadSByte();
                    this._dbfFields[i].Offset = this._binaryReader.ReadUInt32();
                    this._dbfFields[i].Length = this._binaryReader.ReadByte();
                    this._dbfFields[i].Precision = this._binaryReader.ReadByte();
                    this._dbfFields[i].Reserved = this._binaryReader.ReadBytes(2);
                    this._dbfFields[i].DbaseivID = this._binaryReader.ReadSByte();
                    this._dbfFields[i].Reserved2 = this._binaryReader.ReadBytes(10);
                    this._dbfFields[i].ProductionIndex = this._binaryReader.ReadSByte();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return true;
        }

        /**/
        /// <summary>
        /// ��dbf�ļ��ж�ȡ�ļ�ͷ��Ϣ
        /// </summary>
        /// <returns></returns>
        private bool ReadFileHeader()
        {
            // ȷ��Ŀ���ļ��Ѿ���
            if (!this.IsFileOpened)
                return false;

            // ���Թ����µ�dbf�ļ�ͷ����
            if (null == this._dbfHeader)
                this._dbfHeader = new DBFHelper();

            try
            {
                this._dbfHeader.Version = this._binaryReader.ReadSByte();//��1�ֽ�
                this._dbfHeader.LastModifyYear = this._binaryReader.ReadByte();//��2�ֽ�
                this._dbfHeader.LastModifyMonth = this._binaryReader.ReadByte();//��3�ֽ�
                this._dbfHeader.LastModifyDay = this._binaryReader.ReadByte();//��4�ֽ�
                this._dbfHeader.RecordCount = this._binaryReader.ReadUInt32();//��5-8�ֽ�
                this._dbfHeader.HeaderLength = this._binaryReader.ReadUInt16();//��9-10�ֽ�
                this._dbfHeader.RecordLength = this._binaryReader.ReadUInt16();//��11-12�ֽ�
                this._dbfHeader.Reserved = this._binaryReader.ReadBytes(16);//��13-14�ֽ�
                this._dbfHeader.TableFlag = this._binaryReader.ReadSByte();//��15�ֽ�
                this._dbfHeader.CodePageFlag = this._binaryReader.ReadSByte();//��16�ֽ�
                this._dbfHeader.Reserved2 = this._binaryReader.ReadBytes(2);////��17-18�ֽ�
            }
            catch (Exception e)
            {
                throw e;
            }

            // ���ü�¼��Ŀ
            this._recordCount = this._dbfHeader.RecordCount;
            uint fieldCount = (uint)((this._dbfHeader.HeaderLength - DBFHelper.DBFHeaderSize - 1) / DBFField.DBFFieldSize);
            this._fieldCount = 0;

            // ������Щdbf�ļ����ļ�ͷ����и������Σ�������Щ�ļ�û�У��ڴ�ʹ�ñ����������ֶ���Ŀ
            // ���ǲ���ÿһ���洢�ֶνṹ����ĵ�һ���ֽڵ�ֵ�������Ϊ0x0D����ʾ����һ���ֶ�
            // ����Ӵ˴���ʼ���ٴ����ֶ���Ϣ
            try
            {
                for (uint i = 0; i < fieldCount; i++)
                {
                    // ��λ��ÿ���ֶνṹ������ȡ��һ���ֽڵ�ֵ
                    this._fileStream.Seek(DBFHelper.DBFHeaderSize + i * DBFField.DBFFieldSize, SeekOrigin.Begin);
                    byte flag = this._binaryReader.ReadByte();

                    // �����ȡ���ı�־��Ϊ0x0D�����ʾ���ֶδ��ڣ�����Ӵ˴���ʼ������û���ֶ���Ϣ
                    if (0x0D != flag)
                        this._fieldCount++;
                    else
                        break;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return true;
        }


        #region properties
        /// <summary>
        /// ��ȡ��ǰ����������ļ���DataSet�еı���
        /// Ҳ���������ļ�ȥ����չ������ļ���
        /// </summary>

        public string TableName
        {
            get
            {
                return this._tableName;
            }
        }
        /**/
        /// <summary>
        /// ��ȡ��ǰ��¼�Ƿ�ɾ��
        /// </summary>
        public bool IsRecordDeleted
        {
            get
            {
                if (!this.IsFileOpened)
                    return false;

                if (this.IsBOF || this.IsEOF)
                    return false;

                // ֻ�м�¼����ĵ�һ���ֽڵ�ֵΪɾ����־��0x2A���ű�ʾ��ǰ��¼��ɾ����
                if (0x2A == this._recordBuffer[0])
                    return true;
                else
                    return false;
            }
        }

        /**/
        /// <summary>
        /// ��ȡ��¼����
        /// </summary>
        public uint RecordLength
        {
            get
            {
                if (!this.IsFileOpened)
                    return 0;

                return this._dbfHeader.RecordLength;
            }
        }

        /**/
        /// <summary>
        /// ��ȡ�ֶ���Ŀ
        /// </summary>
        public uint FieldCount
        {
            get
            {
                if (!this.IsFileOpened)
                    return 0;

                return this._fieldCount;
            }
        }

        /**/
        /// <summary>
        /// ��ȡ��¼��Ŀ
        /// </summary>
        public uint RecordCount
        {
            get
            {
                if (!this.IsFileOpened)
                    return 0;

                return this._recordCount;
            }
        }

        /**/
        /// <summary>
        /// ��ȡ�Ƿ��¼ָ���Ѿ��ƶ�����¼��ǰ��
        /// </summary>
        public bool IsBOF
        {
            get
            {
                return (-1 == this._recordIndex);
            }
        }

        /**/
        /// <summary>
        /// ��ȡ�Ƿ��¼ָ���Ѿ��ƶ�����¼�����
        /// </summary>
        public bool IsEOF
        {
            get
            {
                return ((uint)this._recordIndex == this._recordCount);
            }
        }

        public bool IsFileEnd
        {
            get
            {
                return this._recordBuffer.Length == 0;
            }
        }

        /**/
        /// <summary>
        /// ��ȡdbf�ļ��Ƿ��Ѿ�����
        /// </summary>
        private bool IsFileOpened
        {
            get
            {
                return this._isFileOpened;
            }
        }

        #endregion
        #region IDisposable ��Ա

        void System.IDisposable.Dispose()
        {
            // TODO:  ��� DBFFile.System.IDisposable.Dispose ʵ��
            this.Dispose(true);
        }

        #endregion
    }
}
