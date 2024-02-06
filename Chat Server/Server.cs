using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using NetHelper;

namespace ChatServer
{
    // Represents server that processes all clients requests
    public class Server
    {
        private static ushort port;
        private static IPAddress IP;
        private static TcpListener listener; // Listener opened on a specified port to catch requests
        internal List<ServerUser> activeUsers = new List<ServerUser>(); // List of users connected to the server
        internal List<ServerUser> registeredUsers = new List<ServerUser>(); // List of users registered on the server
        static void Main(string[] args)
        {
            GetIPAndPort(ref IP, ref port);
            listener = new TcpListener(IP, port);
            listener.Start();
            Console.WriteLine("Server is started successfully. IP: {0}, port: {1}!", IP, port);
            Console.WriteLine("Waiting for connections...");                      
            Server server = new Server();
            Thread listening = new Thread(new ThreadStart(server.Listen));
            listening.Start();
        }
        static void GetIPAndPort(ref IPAddress IP, ref ushort port)

        {
            Console.WriteLine("Searching VLANs...");
            IP = NetHooks.GetVLANIP();
            if (IP == null)
            {
                Console.WriteLine("No VLANs found. Start Hamachi!");
                Console.ReadKey();
                Environment.Exit(0);
            }
            else
                Console.WriteLine("VLAN connection is found! Server IP: " + IP.ToString());

            Console.Write("Enter server's port: ");
            while (true)
            {
                if (!ushort.TryParse(Console.ReadLine(), out port) || port < 1024 || port > 65535)
                    Console.WriteLine("Wrong port number! Port must be an integer in range from 1024 to 65535.");
                else if (!NetHooks.CheckPortAvailable(port))
                    Console.WriteLine("This port is occupied with another app! Use another port!");
                else
                    break;
            }
        }
        public void Listen() // Listens for incoming connections in a separate thread
        {
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient(); // Accepts new client connection
                if (client != null) 
                {
                    Console.WriteLine("New connection!");
                    ServerUser user = new ServerUser(client, this); // Initializing new user object and its thread
                    Thread userThread = new Thread(new ThreadStart(user.Process));
                    userThread.Start();
                }
            }
        }
        public void Report(string s)
        {
            Console.WriteLine(s);
        }
        public void LoginUser(ServerUser user, string login, string password) // User's authorization
        {
            Report(login + " " + password);
            foreach(ServerUser check in registeredUsers)
            {
                if (check.Login == login && check.Password == password) 
                {
                    user.Login = login; 
                    user.Password = password;
                    activeUsers.Add(user); 
                    user.SendCommand(user.client.GetStream(), ClientCommands.GoodLoginAndPass, string.Empty, new string[] { login, password } ); // Sends message of successful login to client
                    Announcement(login + " joins chat!");
                    return;
                }
                user.SendCommand(user.client.GetStream(), ClientCommands.BadLoginAndPass, string.Empty, NetHooks.EmptyExtra); // Sends message of wrong login or password to client
            }
        }
        public void Announcement(string message) // Sends message from server to all connected clients
        {
            foreach(ServerUser user in activeUsers)
            {
                user.SendCommand(user.client.GetStream(), ClientCommands.GetAnnouncement, message, NetHooks.EmptyExtra); 
            }
        }
        public void Register(ServerUser user, string login, string password) // User's registration
        {
            foreach(ServerUser check in registeredUsers)
            {
                if(check.Login == login)
                {
                    user.SendCommand(user.client.GetStream(), ClientCommands.OccupiedLogin, string.Empty, NetHooks.EmptyExtra); // Sends message of an occupied login to client
                    return;
                }
            }
            user.Login = login;
            user.Password = password;
            user.SendCommand(user.client.GetStream(), ClientCommands.GoodRegistration, string.Empty, NetHooks.EmptyExtra ); // Sends message of successful registration to client
            registeredUsers.Add(user);
            // TO DO: JSON
        }
        public void BroadcastMessage(string message, string senderLogin) // Sends message from specified user to all other connected clients
        {
            foreach (ServerUser user in activeUsers)
            {
                if (user.Login != senderLogin)
                {
                    user.SendCommand(user.client.GetStream(), ClientCommands.GetBroadcastMessage, message, new string[] { senderLogin } ); 
                }
            }
        }

        public void PrivateMessage(ServerUser senderUser, string message, string senderLogin, string destinationLogin) // Sends message from specified user to another one
        {
            foreach (ServerUser user in activeUsers)
            {
                if (user.Login == destinationLogin)
                {
                    user.SendCommand(user.client.GetStream(), ClientCommands.GetPrivateMessage, message, new string[] { senderLogin } ); // Sends message from specified user to another one
                    return;
                }
            }
            senderUser.SendCommand(senderUser.client.GetStream(), ClientCommands.BadPrivateMessageDestLogin, string.Empty, new string[] { destinationLogin } ); // Sends message of wrong private message destination login to its sender
        }

    }
}
