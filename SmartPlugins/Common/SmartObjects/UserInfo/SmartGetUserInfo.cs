using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekla.Structures.Model;
using System.Management;
using System.Net.NetworkInformation;

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

        public string GetMACAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (var adapter in nics)
            {
                if (sMacAddress != null)
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                    return sMacAddress;
                }
            }
            return string.Empty;
        }
    }
}
