using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using CMS.Model.Oracle;

namespace CMS.HtmlService.Contract
{
    [ServiceContract]
    public interface ISsoOutWindow
    {
        [OperationContract(IsOneWay=true)]
        void SsoPush(int planId, string version);

        [OperationContract(IsOneWay = true)]
        void SsoPushOne(int ssoResultId);

        [OperationContract]
        int UpDateStatus(int id, int status);

        //炒股大赛
        [OperationContract(IsOneWay = true)]
        void SsoPushStockContest(StockContestData scd);

    }
}
