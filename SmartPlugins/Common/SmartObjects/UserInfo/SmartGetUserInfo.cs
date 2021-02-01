using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;
using System.Management;

namespace SmartObjects.UserInfo
{
    public struct SmartGetUserInfo
    {       
        public string UserIP { get => ""; }
        public string UserMAC { get => ""; }
        public string HddSerialNumber { get => GetHDDSerial(); }
        public string UserID { get; }

        public SmartGetUserInfo(string Id)
        {
            UserID = Id;
            //HddSerialNumber = GetHDDSerial();
            //UserIP = GetUserIP();
        }

        private string GetHDDSerial()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");

            foreach (ManagementObject wmi_HD in searcher.Get())
            {
                if (wmi_HD["SerialNumber"] != null)
                {                    
                    return wmi_HD["SerialNumber"].ToString();
                }
            }
            return string.Empty;
        }

        private string GetUserIP()
        {
            String host = System.Net.Dns.GetHostName();           
            System.Net.IPAddress ip = System.Net.Dns.GetHostEntry(host).AddressList[0];
            return ip.ToString();
        }
    }
}
