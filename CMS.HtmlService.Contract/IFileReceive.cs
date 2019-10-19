using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Drawing;

namespace CMS.HtmlService.Contract
{
    [ServiceContract]
    public interface IFileReceive
    {
        [OperationContract]
        bool CreateFileImage(byte[] image, string readPath);
        //[OperationContract]
        //bool CreateFileImageII(Bitmap image, string readPath);
        [OperationContract]
        bool DeleteFile(string readPath);
        [OperationContract]
        bool DeleteFiles(string readPaths);
    }
}
