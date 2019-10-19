using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMS.DAL;

namespace CMS.BLL
{
   public partial class ChannelBLL
    {
       public string InsertChannel(string parentID, string channeName, string url, string typeID, string channelEnName, string createUser, bool status)
       {  
           return dal.InsertChannel(parentID, channeName, url, typeID, channelEnName, createUser, status);
       }


       public string UpdateChannel(string channelID, string parentID, string channeName, string url, string typeID, string channelEnName, string updateUser, bool status)
       {
           return dal.UpdateChannel(channelID, parentID, channeName, url, typeID, channelEnName, updateUser, status);
       }
    }

}