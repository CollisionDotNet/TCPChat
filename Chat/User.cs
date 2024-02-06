using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetHelper;

namespace Chat
{
    // Represents chat client
    public class User
    {
        public Interface UI; // UI object binded to current user
        public TcpClient client; // user's TCP client
        public string Login; 
        public string Password;
        public User(Interface ui)
        {
            UI = ui;
        }
        public void Process()
        {
            NetworkStream stream = client.GetStream(); // user's network stream
            ClientCommands curCommand = ClientCommands.Empty; // current server-to-client command
            string curMessage; // bare string server-to-client message  
            string[] extraParams;
            while (true) // Receiving server-to-client messages
            {
                curMessage = NetHooks.ReadFromStream(stream); // Decoding new string message from stream
                if (curMessage != string.Empty) // Then server has sended the message
                {
                    ReceiveCommand(stream, out curCommand, ref curMessage, out extraParams); // Extracting command and it's params out of a received string
                    RunCommand(stream, curCommand, curMessage, extraParams); // Then executing it
                    curMessage = string.Empty; 
                    curCommand = ClientCommands.Empty; 
                    extraParams = NetHooks.EmptyExtra; 
                }
            }
        }
        /// <summary>
        /// Executes command on client side
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="command"></param>
        /// <param name="message"></param>
        /// <param name="extra"></param>
        public void RunCommand(NetworkStream stream, ClientCommands command, string message, string[] extra)
        {
            switch(command)
            {
                case ClientCommands.Empty:
                    break;
                case ClientCommands.GoodLoginAndPass:
                    Login = extra[0];
                    Password = extra[1];
                    UI.OpenChatPage();
                    break;
                case ClientCommands.BadLoginAndPass:
                    MessageBox.Show("Wrong login or password!");
                    break;
                case ClientCommands.GoodRegistration:
                    UI.OpenLoginPage();
                    break;
                case ClientCommands.OccupiedLogin:
                    MessageBox.Show("This login is occupied");
                    break;
                case ClientCommands.GetAnnouncement:
                    UI.ChatWindow.AppendText("\n" + message);
                    break;
                case ClientCommands.GetBroadcastMessage:
                    UI.ChatWindow.AppendText("\n" + extra[0] + ": " + message);
                    break;
                case ClientCommands.GetPrivateMessage:
                    UI.ChatWindow.AppendText("\n" + extra[0] + " whispers: " + message);
                    break;
                case ClientCommands.BadPrivateMessageDestLogin:
                    UI.ChatWindow.AppendText("\nUser " + extra[0] + " is not found!");
                    break;
            }
        }
        /// <summary>
        /// Sends message from client to server
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="command"></param>
        /// <param name="message"></param>
        /// <param name="ExtraList"></param>
        public void SendCommand(NetworkStream stream, ServerCommands command, string message, string[] ExtraList)
        {
            string extra = string.Join(NetHooks.ExtraSeparator.ToString(), ExtraList);
            int extraLength = extra.Length;
            string LengthStr = extra.Length < 10 ? "0" + extraLength.ToString() : extraLength.ToString();
            message = message.Insert(0, extra);
            message = message.Insert(0, LengthStr);
            message = message.Insert(0, ((int)command).ToString());
            NetHooks.WriteToStream(stream, message);
        }
        /// <summary>
        /// Receives command from server with extra parameters
        /// </summary>
        /// <param name="command"></param>
        /// <param name="message"></param>
        /// <param name="ExtraList"></param>
        public void ReceiveCommand(NetworkStream stream, out ClientCommands command, ref string message,  out string[] ExtraList)
        {

            command = (ClientCommands)int.Parse(message.Substring(0, 2));
            int ExtraLength = int.Parse(message.Substring(2, 2));
            string extra = message.Substring(4, ExtraLength);
            ExtraList = extra.Split(NetHooks.ExtraSeparator);
            message = message.Substring(4 + ExtraLength);

        }
    }
}
