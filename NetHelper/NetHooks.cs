using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace NetHelper
{
    public enum ClientCommands // Server to client commands
    {
        Empty = 10, 
        GoodLoginAndPass, 
        BadLoginAndPass, 
        GoodRegistration, 
        OccupiedLogin, 
        GetBroadcastMessage, 
        GetPrivateMessage, 
        BadPrivateMessageDestLogin, // Informs that private message can't be delivered (user is offline)
        GetAnnouncement // Gets message destined for all clients (on new user logon)
    }
    public enum ServerCommands // Client to server commands
    {
        Empty = 10, // No command
        Login, 
        Register, 
        DoBroadcastMessage, 
        DoPrivateMessage 
    }

    public static class NetHooks
    {
        public const char ExtraSeparator = '\u2514'; // Message extra parameters separator

        public static readonly string[] EmptyExtra = { "" };




        /// <summary>
        /// Checks if char is allowed in the message
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static bool IsAllowedChar(char ch)
        {
            int i = ch;
            return (i >= 48 && i <= 57) || (i >= 65 && i <= 90) || (i >= 97 && i <= 122) || (i >= 1040 && i <= 1103);
        }
        /// <summary>
        /// Checks if specified port is occupied on current computer
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public static bool CheckPortAvailable(int port)
        {
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();
            foreach (TcpConnectionInformation tcpi in tcpConnInfoArray)
            {
                if (tcpi.LocalEndPoint.Port == port)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Returns IP address of a computer in VLAN or null if there is no such network 
        /// </summary>
        /// <returns></returns>
        public static IPAddress GetVLANIP()
        {
            IPAddress IP = null;
            foreach (IPAddress ip in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (ip.ToString().Contains('.') && ip.ToString().Substring(0, 2) == "25") // (25 for Hamachi VLAN as a first byte)
                    IP = ip;
            }
            return IP;
        }
        /// <summary>
        /// Encodes string as a byte sequence and sends it with a network stream
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="message"></param>
        public static void WriteToStream(NetworkStream stream, string message)
        {
            byte[] data = Encoding.Unicode.GetBytes(message);
            stream.Write(data, 0, data.Length);

        }
        /// <summary>
        /// Gets bytes from the net stream and decodes it as a string
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string ReadFromStream(NetworkStream stream)
        {
            byte[] data = new byte[64]; // Data buffer
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (stream.DataAvailable);
            return builder.ToString();
        }
    }
}
