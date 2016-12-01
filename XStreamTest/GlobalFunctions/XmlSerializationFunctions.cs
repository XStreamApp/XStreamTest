//using DeviceInformation;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Xml.Serialization;

//namespace GlobalFunctions
//{
//    public static class SerializationFunctions
//    {
//        public static string Serialize(ThisDevice thisDevice)
//        {
//            StreamWriter stWriter = null;
//            XmlSerializer xmlSerializer;
//            string buffer;
//            try
//            {
//                xmlSerializer = new XmlSerializer(typeof(ThisDevice));
//                MemoryStream memStream = new MemoryStream();
//                stWriter = new StreamWriter(memStream);
//                System.Xml.Serialization.XmlSerializerNamespaces xs = new XmlSerializerNamespaces();
//                xs.Add("", "");
//                xmlSerializer.Serialize(stWriter, thisDevice, xs);
//                buffer = Encoding.ASCII.GetString(memStream.GetBuffer());
//            }
//            catch (Exception Ex)
//            {
//                throw Ex;
//            }
//            finally
//            {
//                if (stWriter != null)
//                    stWriter.Close();
//            }
//            return buffer;

//        }
//        public static ThisDevice DeSerialize(string xmlString)
//        {
//            ThisDevice thisDevice = new ThisDevice();
//            XmlSerializer xmlSerializer;
//            MemoryStream memStream = null;
//            try
//            {
//                xmlSerializer = new XmlSerializer(thisDevice.GetType());
//                byte[] bytes = new byte[xmlString.Length];
//                Encoding.ASCII.GetBytes(xmlString, 0, xmlString.Length, bytes, 0);
//                memStream = new MemoryStream(bytes);
//                object objectFromXml = xmlSerializer.Deserialize(memStream);
//                ThisDevice tempObject = (ThisDevice)objectFromXml;
//                thisDevice.aliasName = tempObject.aliasName;
//                thisDevice.data = tempObject.data;
//                thisDevice.directory = tempObject.directory;
//                thisDevice.flag = tempObject.flag;
//                thisDevice.address = tempObject.address;
//                thisDevice.password = tempObject.password;
//                thisDevice.status = tempObject.status;
//            }
//            catch (Exception Ex)
//            {
//                Console.WriteLine(Ex.Message);
//            }
//            finally
//            {
//                if (memStream != null)
//                    memStream.Close();
//            }
//            return thisDevice;
//        }
//    }
//}
