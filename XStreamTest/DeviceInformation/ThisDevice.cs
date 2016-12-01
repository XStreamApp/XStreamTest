using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace DeviceInformation
{
    [Serializable]
    public class ThisDevice : ISerializable
    {
        public string data = "";
        public string aliasName, status, directory;
        public string password;
        public string address;
        public int flag = 0;
        public IPEndPoint ipEp;


        public ThisDevice()
        {
        }

        public ThisDevice(SerializationInfo info, StreamingContext context)
        {
            data = info.GetString("data");
            aliasName = info.GetString("aliasName");
            status = info.GetString("status");
            directory = info.GetString("directory");
            password = info.GetString("password");
            ipEp = (IPEndPoint)info.GetValue("ipEp", typeof(IPEndPoint));
            flag = info.GetInt32("flag");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("data", data);
            info.AddValue("aliasName", aliasName);
            info.AddValue("status", status);
            info.AddValue("directory", directory);
            info.AddValue("password", password);
            info.AddValue("ipEp", ipEp);
            info.AddValue("flag", flag);
        }
    }
}
