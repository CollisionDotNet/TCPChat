using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using NetHelper;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat
{
    public partial class Interface : Form
    {
        IPAddress ServerIP; // IP сервера
        short port; // Порт сервера
        User user; // Создание пустого объекта игрока
        public Interface()
        {
            InitializeComponent();
            Load += Interface_Load;
        }

        private void Interface_Load(object sender, EventArgs e)
        {
            user = new User(this);
            OpenServerConnectionPage();
           
        }

       

        private void LoginButton_Click(object sender, EventArgs e) // Обработка нажатия на кнопку входа
        {
            string login = LoginInput.Text;
            string password = PasswordInput.Text;
            bool success = true;
            foreach(char ch in login)
            {
                if (!NetHooks.ApplyableChar(ch))
                {
                    // Сообщение о плохом логине
                    success = false;
                }
            }
            foreach (char ch in password)
            {
                if (!NetHooks.ApplyableChar(ch))
                {
                    // Сообщение о плохом пароле
                    success = false;
                }
            }
            if (password.Length < 6)
                success = false;
            if (success)
                user.SendCommand(user.client.GetStream(), ServerCommands.Login, string.Empty, new string[] { login, password });

        }
        private void RegistrationButton_Click(object sender, EventArgs e) // Обработка нажатия на кнопку входа
        {
           
            string login = LoginInput.Text;
            string password = PasswordInput.Text;
            bool success = true;
            foreach (char ch in login)
            {
                if (!NetHooks.ApplyableChar(ch))
                {
                    MessageBox.Show("1");
                    success = false;
                    break;
                }
            }
            foreach (char ch in password)
            {
                if (!NetHooks.ApplyableChar(ch))
                {
                    MessageBox.Show("2");
                    success = false;
                }
            }
            if (password.Length < 6)
            {
                MessageBox.Show("3");
                success = false;
            }
            if (success)
            {
                MessageBox.Show("Отослали!");
                user.SendCommand(user.client.GetStream(), ServerCommands.Register, string.Empty, new string[] { login, password });
            }

        }

        private void ConnectButton_Click(object sender, EventArgs e) // Обработка нажатия на кнопку подключения к серверу
        {
            ServerIP = IPAddress.Parse(IPInput.Text); // Получаем IP сервера для подключения из textBox-а
            port = short.Parse(PortInput.Text); // Получаем порт сервера для подключения из textBox-а

            TcpClient client = new TcpClient(); // Создаём объект клиента
            user.client = client; // Установка клиента игрока

            client.Connect(ServerIP, port); // Подключаемся к серверу по IP и порту
            Thread Update = new Thread(new ThreadStart(user.Process)); // Создаём и запускаем отдельный поток обновления чата
            Update.Start();

            OpenLoginPage();

        }

        private void SendMessageButton_Click(object sender, EventArgs e) // Обработка нажатия на кнопку отправки сообщения
        {
            string message = NewMessageInput.Text;
            if (message.StartsWith("-> "))
            {               
                string destLogin = "";

                message = message.Substring(2);
                message = message.Trim(' ');
                string[] arr = message.Split(' ');
                if (arr.Length > 1)
                {
                    destLogin = arr[0];
                    message = message.Substring(destLogin.Length + 1);
                    user.SendCommand(user.client.GetStream(), ServerCommands.DoPrivateMessage, message, new string[] { user.Login, destLogin });
                }
            }
            else
            {
                user.SendCommand(user.client.GetStream(), ServerCommands.DoBroadcastMessage, message, new string[] { user.Login });
            }
            NewMessageInput.Text = "";
        }

        private void OpenRegistrationLabel_Click(object sender, EventArgs e)
        {
            OpenRegistrationPage();
        }
        public void OpenServerConnectionPage() // Метод открытия страницы подключения к серверу
        {
            this.currentPage = Page.ServerConnectionPage;
            foreach(Control control in Controls)
            {
                control.Visible = false;
                control.Enabled = false;
            }
            foreach (Control control in ServerConnectionControls)
            {
                control.Visible = true;
                control.Enabled = true;
            }
        }
        public void OpenRegistrationPage() // Метод открытия страницы регистрации пользователя
        {
            this.currentPage = Page.RegistrationPage;
            foreach (Control control in Controls)
            {
                control.Visible = false;
                control.Enabled = false;
            }
            foreach (Control control in RegistrationControls)
            {
                control.Visible = true;
                control.Enabled = true;
            }
        }
        public void OpenLoginPage() // Метод открытия страницы входа
        {
            this.currentPage = Page.LoginPage;
            foreach (Control control in Controls)
            {
                control.Visible = false;
                control.Enabled = false;
            }
            foreach (Control control in LoginControls)
            {
                control.Visible = true;
                control.Enabled = true;
            }
        }
        public void OpenChatPage()
        {
            this.currentPage = Page.ChatPage;
            foreach (Control control in Controls)
            {
                control.Visible = false;
                control.Enabled = false;
            }
            foreach (Control control in ChatControls)
            {
                control.Visible = true;
                control.Enabled = true;
            }
        }
        public void TurnOffControls(Control[] controls)
        {
            foreach (Control control in controls)
            {
                control.Visible = false;
                control.Enabled = false;
            }
        }
        public void TurnOnControls(Control[] controls)
        {
            foreach (Control control in controls)
            {
                control.Visible = true;
                control.Enabled = true;
            }
        }
    }

}
