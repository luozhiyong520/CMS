using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.Model;
using CMS.Model.Oracle;
using ServiceStack.OrmLite;
using CMS.DAL;
using ServiceStack.DataAnnotations;
using System.Data.OracleClient;
using Common;


namespace CMS.BLL.Oracle
{
    public class XH_GOODS_TYPEBLL
    {
        OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory(SqlConnectFactory.BOCE, OracleDialect.Provider);

        //添加品种
        public void AddGoodsType(string code, string type, string name)
        {
            string insertGoodType;
            using (var db = dbFactory.OpenDbConnection())
            {
                insertGoodType = db.Query<XH_GOODS_TYPE>("insert into XH_GOODS_TYPE (FCODE,FTYPE) VALUES( '" + code + "', '" + type + "')").ToString();
            }

            string goodsTypeCode;
            using (var db = dbFactory.OpenDbConnection())
            {
                goodsTypeCode = db.Query<XH_PROCUDT_TYPE>("select FCODE from XH_PROCUDT_TYPE ").ToString();
            }
            //判断品种是否存在，如果不存在则添加新品种
            if (goodsTypeCode == "")
            {
                using (var db = dbFactory.OpenDbConnection())
                {
                    string insertGoodsType = db.Query<XH_PROCUDT_TYPE>("insert into XH_GOODS_TYPE (FCODE,FNAME) VALUES( '" + code + "', '" + name + "')").ToString();
                }
            }
        }

        //查询品种
        public List<XH_GOODS_TYPE> SelectGoodsType(string code, string type, string name)
        {
            List<XH_GOODS_TYPE> seachGoodType = new List<XH_GOODS_TYPE>();
            using (var db = dbFactory.OpenDbConnection())
            {
                seachGoodType = db.Query<XH_GOODS_TYPE>("select * from XH_GOODS_TYPE as xgt,XH_PROCUDT_TYPE as xpt where xgt.FCODE=xpt.FCODE");
            }
            return seachGoodType;
        }

        //删除品种
        public void DeleteGoodsType(string code)
        {
            string delGoodType;
            using (var db = dbFactory.OpenDbConnection())
            {
                delGoodType = db.Query<XH_GOODS_TYPE>("delete from XH_GOODS_TYPE where code=:FCODE", new { FCODE = code }).ToString();
            }
        }
    }
}
