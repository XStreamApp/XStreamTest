using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace DeviceInformation
{
    public static class ConnectedDevices
    {
        public static ThisDevice thisDeviceInfo;
        
        static List<ThisDevice> listClient = new List<ThisDevice>();
        static int updateFlag = 0, cnt = 0;
        
        public static void GetCompiled(IPAddress addr)
        {
            thisDeviceInfo = new ThisDevice();
            thisDeviceInfo.ipEp = new IPEndPoint(addr, 8810);
            thisDeviceInfo.aliasName = "Ras";
            thisDeviceInfo.address = addr.ToString();
            thisDeviceInfo.password = "1234";
        }

        public static void ManipClientObject(ThisDevice thisObject)
        {
            if (thisObject.data == "REMOVE_THIS_DEVICE")
            {
                UpdateFlag = 1;
                ListClient.Remove(thisObject);
            }
            else
            {
                foreach (var item in ListClient)
                {
                    if (item.address.Equals(thisObject.address))
                    {
                        if (!item.data.Equals(thisObject.data))
                        {
                            item.data = thisObject.data;
                        }
                    }
                }
                if (thisObject.flag == 1)
                {
                    UpdateFlag = 1;
                    ListClient.Add(thisObject);
                    Cnt++;
                    thisObject.flag = 1;
                }
            }
        }

        public static int Cnt
        {
            get
            {
                return cnt;
            }

            set
            {
                cnt = value;
            }
        }

        public static List<ThisDevice> ListClient
        {
            get
            {
                return listClient;
            }

            set
            {
                listClient = value;
            }
        }

        public static int UpdateFlag
        {
            get
            {
                return updateFlag;
            }

            set
            {
                updateFlag = value;
            }
        }
    }
}
