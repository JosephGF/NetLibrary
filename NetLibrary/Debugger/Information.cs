using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetLibrary.Debugger
{
    public class Information
    {
        private DateTime _dateError = DateTime.Now;
        public string UserName { get { return SystemInformation.UserName; } }
        public string UserDomainName { get { return SystemInformation.UserDomainName; } }
        public string Culture { get { return Application.CurrentCulture.DisplayName; } }
        public string MonitorCount { get { return SystemInformation.MonitorCount.ToString(); } }
        public string Network { get { return SystemInformation.Network.ToString(); } }
        public string BatteryLifePercent { get { return SystemInformation.PowerStatus.BatteryLifePercent.ToString(); } }
        public string UserInteractive { get { return SystemInformation.UserInteractive.ToString(); } }
        public string ExecutablePath { get { return Application.ExecutablePath; } }
        public string IpClient
        {
            get
            {
                IPHostEntry host;
                string localIP = "?";
                host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        localIP = ip.ToString();
                    }
                }
                return localIP;
            }
        }

        public string ApplicationName { get { return Application.ProductName + " Ver. " + Application.ProductVersion; } }
        public string OSProcess { get { return Environment.Is64BitOperatingSystem ? "x64" : "x32"; } }
        public string AppProcess { get { return Environment.Is64BitProcess ? "x64" : "x32"; } }
        public string MachineName { get { return Environment.MachineName; } }
        public string OSVersion { get { return Environment.OSVersion.VersionString; } }

        public string ApplicationStartTime { get { return Process.GetCurrentProcess().StartTime.ToString("dd/MM/yyyy hh:mm:ss"); } }
        public string ApplicationExceptionTime { get { return _dateError.ToString("dd/MM/yyyy hh:mm:ss"); } }

        public override string ToString()
        {
            StringBuilder strInfo = new StringBuilder();
            foreach (PropertyInfo pi in typeof(Information).GetProperties())
            {
                strInfo.Append(" | ");
                strInfo.Append(pi.Name);
                strInfo.Append(" -> ");
                strInfo.AppendLine(pi.GetValue(this) as String);
            }

            return strInfo.ToString();
        }
    }

}
