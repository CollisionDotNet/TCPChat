using NetHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    // Represents user connected on a server
    public class ServerUser
    {
        public TcpClient client; // user's TCP client
        public string Login; 
        public string Password; 
        public Server server; // Server, to which the user is connected

        public ServerUser(TcpClient client, Server server) 
        {
            this.server = server;
            this.client = client;
        }
        public void Process() // Processes all client-to-server requests
        {
            NetworkStream stream = client.GetStream(); // user's network stream
            ServerCommands curCommand = ServerCommands.Empty; // current client-to-server command
            string curMessage; // bare string client-to-server message
            string[] extraParams;
            while (true) // Receiving client-to-server messages
            {
                curMessage = NetHooks.ReadFromStream(stream); // Decoding new string message from stream
                if (curMessage != string.Empty) // Then client has sended the message
                {
                    server.Report(curMessage);
                    ReceiveCommand(out curCommand, ref curMessage, out extraParams); // Extracting command and it's params out of a received string
                    RunCommand(curCommand, curMessage, extraParams); // Then executing it
                    curMessage = string.Empty; 
                    curCommand = ServerCommands.Empty; 
                    extraParams = NetHooks.EmptyExtra; 
                }
            }

        }
        /// <summary>
        /// Sends message from server to client
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="command"></param>
        /// <param name="message"></param>
        /// <param name="ExtraList"></param>
        public void SendCommand(NetworkStream stream, ClientCommands command, string message, string[] ExtraList)
        {
            string extra = string.Join(NetHooks.ExtraSeparator.ToString(), ExtraList); // String with all extra parameters
            int extraLength = extra.Length; 
            string LengthStr = extra.Length < 10 ? "0" + extraLength.ToString() : extraLength.ToString(); 
            message = message.Insert(0, extra); // Finally goes extras string
            message = message.Insert(0, LengthStr); // Secondly goes its length
            message = message.Insert(0, ((int)command).ToString()); // Firstly goes command number
            NetHooks.WriteToStream(stream, message); // Sending message to client

        }
        /// <summary>
        /// Receives command from client with extra parameters
        /// </summary>
        /// <param name="command"></param>
        /// <param name="message"></param>
        /// <param name="ExtraList"></param>
        public void ReceiveCommand(out ServerCommands command, ref string message, out string[] ExtraList)
        {
            command = (ServerCommands)int.Parse(message.Substring(0, 2)); // Scrapping command number out of message
            int ExtraLength = int.Parse(message.Substring(2, 2)); // Getting extra params length
            string extra = message.Substring(4, ExtraLength); // Getting extra params
            ExtraList = extra.Split(NetHooks.ExtraSeparator); // Splitting extra params as an array
            message = message.Substring(4 + ExtraLength); // Getting pure message
        }
        /// <summary>
        /// Executes command on the server side
        /// </summary>
        /// <param name="command"></param>
        /// <param name="message"></param>
        /// <param name="extra"></param>
        private void RunCommand(ServerCommands command, string message, string[] extra)
        {
            switch (command)
            {
                case ServerCommands.Empty:
                    break;             
                case ServerCommands.Login:
                    server.LoginUser(this, extra[0], extra[1]);
                    break;
                case ServerCommands.Register:
                    server.Register(this, extra[0], extra[1]);
                    break;
                case ServerCommands.DoBroadcastMessage:
                    server.BroadcastMessage(message, extra[0]);
                    break;
                case ServerCommands.DoPrivateMessage:
                    server.PrivateMessage(this, message, extra[0], extra[1]);
                    break;

            }
        }

    }
}

