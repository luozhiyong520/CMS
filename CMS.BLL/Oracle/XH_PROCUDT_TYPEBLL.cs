using System.Text;
using CMS.Model;
using CMS.Model.Oracle;
using ServiceStack.OrmLite;
using CMS.DAL;
using ServiceStack.DataAnnotations;
using System.Data.OracleClient;
using Common;
using System.Collections.Generic;
using System;


namespace CMS.BLL.Oracle
{
    public class XH_PROCUDT_TYPEBLL
    {
        //添加新品种
        public void AddActualsGoods(string code, string name)
        {
            OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory(SqlConnectFactory.CODE, OracleDialect.Provider);
            XH_PROCUDT_TYPE model = new XH_PROCUDT_TYPE();
            model.FCODE = code;
            model.FNAME = name;
            using (var db = dbFactory.OpenDbConnection())
            {
                try
                {
                    db.Insert<XH_PROCUDT_TYPE>(model);
                }
                catch (Exception ex)
                {
                    return;
                }
            }
        }

        //查询品种
        public List<XH_PROCUDT_TYPE> SelectGoods(string code, string name)
        {
            OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory(SqlConnectFactory.CODE, OracleDialect.Provider);
            List<XH_PROCUDT_TYPE> seachGoods = new List<XH_PROCUDT_TYPE>();

            if (code == "" && name == "")
            {
                using (var db = dbFactory.OpenDbConnection())
                {
                    seachGoods = db.Query<XH_PROCUDT_TYPE>("select * from XH_PROCUDT_TYPE");
                }
            }
            else
            {
                using (var db = dbFactory.OpenDbConnection())
                {
                    seachGoods = db.Query<XH_PROCUDT_TYPE>("select * from XH_PROCUDT_TYPE where FCODE=:FCODE or FNAME=:FNAME", new { FCODE = code, FNAME = name });
                }
            }
            return seachGoods;
        }

        //删除品种
        public void DeleteGoodsType(string code)
        {
            OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory(SqlConnectFactory.CODE, OracleDialect.Provider);
            string delGoodType;
            using (var db = dbFactory.OpenDbConnection())
            {
                delGoodType = db.Query<XH_PROCUDT_TYPE>("delete from XH_PROCUDT_TYPE where FCODE=:FCODE", new { FCODE = code }).ToString();
            }
        }
    }
}
