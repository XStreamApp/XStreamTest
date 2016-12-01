using DeviceInformation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace GlobalFunctions
{
    public static class BinSerializer
    {
        static MemoryStream str= new MemoryStream();
        static BinaryFormatter formatter = new BinaryFormatter();

        public static byte[] Serialize(ThisDevice thisDevice)
        {
            byte[] binString = { };
            try
            {
                formatter.Serialize(str, thisDevice);
                binString = str.ToArray();
                foreach (var item in binString)
                {
                    Console.Write(item);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return binString;
        }

        public static ThisDevice Deserialize(Byte[] buffer)
        {
            ThisDevice thisDevice = new ThisDevice();
            try
            {
                str = new MemoryStream(buffer);
                thisDevice = (ThisDevice)formatter.Deserialize(str);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return thisDevice;
        }
    }
}
