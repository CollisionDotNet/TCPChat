using System.Collections.Generic;
using System.Windows.Forms;
namespace Chat
{
    partial class Interface
    {
        enum Page
        {
            NoPage,
            ServerConnectionPage,
            LoginPage,
            RegistrationPage,
            ChatPage
        }
        Control[] ServerConnectionControls;
        Control[] LoginControls;
        Control[] RegistrationControls;
        Control[] ChatControls;
        Page currentPage = Page.NoPage;
        #region MustHave
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
        #region Код, автоматически созданный конструктором форм Windows
        private void InitializeComponent()
        {
            
            this.ConnectButton = new System.Windows.Forms.Button();
            this.RegistrationButton = new System.Windows.Forms.Button();
            this.IPInput = new System.Windows.Forms.TextBox();
            this.PortInput = new System.Windows.Forms.TextBox();
            this.ChatWindow = new System.Windows.Forms.TextBox();
            this.NewMessageInput = new System.Windows.Forms.TextBox();
            this.IPInputLabel = new System.Windows.Forms.Label();
            this.PortInputLabel = new System.Windows.Forms.Label();
            this.ChatLabel = new System.Windows.Forms.Label();
            this.NewMessageLabel = new System.Windows.Forms.Label();
            this.SendMessageButton = new System.Windows.Forms.Button();
            this.LoginLabel = new System.Windows.Forms.Label();
            this.LoginInput = new System.Windows.Forms.TextBox();
            this.LoginButton = new System.Windows.Forms.Button();
            this.HelloLabel = new System.Windows.Forms.Label();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.PasswordInput = new System.Windows.Forms.TextBox();
            this.NoAccountLabel = new System.Windows.Forms.Label();
            this.OpenRegistrationLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            #region Server Connection Page Controls
            // 
            // IPInput
            // 
            this.IPInput.Location = new System.Drawing.Point(364, 119);
            this.IPInput.Name = "IPInput";
            this.IPInput.Size = new System.Drawing.Size(100, 20);
            this.IPInput.TabIndex = 1;         
            // 
            // IPInputLabel
            // 
            this.IPInputLabel.AutoSize = true;
            this.IPInputLabel.Location = new System.Drawing.Point(361, 103);
            this.IPInputLabel.Name = "IPInputLabel";
            this.IPInputLabel.Size = new System.Drawing.Size(110, 13);
            this.IPInputLabel.TabIndex = 5;
            this.IPInputLabel.Text = "Enter server IP:";             
            // PortInput
            // 
            this.PortInput.Location = new System.Drawing.Point(364, 162);
            this.PortInput.Name = "PortInput";
            this.PortInput.Size = new System.Drawing.Size(100, 20);
            this.PortInput.TabIndex = 2;
            // 
            // PortInputLabel
            // 
            this.PortInputLabel.AutoSize = true;
            this.PortInputLabel.Location = new System.Drawing.Point(361, 146);
            this.PortInputLabel.Name = "PortInputLabel";
            this.PortInputLabel.Size = new System.Drawing.Size(123, 13);
            this.PortInputLabel.TabIndex = 6;
            this.PortInputLabel.Text = "Enter server port:";
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(364, 188);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(100, 23);
            this.ConnectButton.TabIndex = 0;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);

            ServerConnectionControls = new Control[] { IPInput, IPInputLabel, PortInput, PortInputLabel, ConnectButton };

            #endregion
            #region Login Page Controls
            // 
            // LoginLabel
            // 
            this.LoginLabel.Location = new System.Drawing.Point(350, 123);
            this.LoginLabel.Name = "LoginLabel";
            this.LoginLabel.Size = new System.Drawing.Size(84, 13);
            this.LoginLabel.TabIndex = 11;
            this.LoginLabel.Text = "Enter login:";
            // 
            // LoginInput
            // 
            this.LoginInput.Location = new System.Drawing.Point(344, 139);
            this.LoginInput.Name = "LoginInput";
            this.LoginInput.Size = new System.Drawing.Size(100, 20);
            this.LoginInput.TabIndex = 10;
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Location = new System.Drawing.Point(350, 162);
            this.PasswordLabel.Name = "Password Label";
            this.PasswordLabel.Size = new System.Drawing.Size(91, 13);
            this.PasswordLabel.TabIndex = 15;
            this.PasswordLabel.Text = "Enter password:";
            // 
            // PasswordInput
            // 
            this.PasswordInput.Location = new System.Drawing.Point(344, 178);
            this.PasswordInput.Name = "PasswordInput";
            this.PasswordInput.Size = new System.Drawing.Size(100, 20);
            this.PasswordInput.TabIndex = 14;
            // 
            // LoginButton
            // 
            this.LoginButton.Location = new System.Drawing.Point(344, 201);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(100, 23);
            this.LoginButton.TabIndex = 12;
            this.LoginButton.Text = "Login";
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // NoAccountLabel
            // 
            this.NoAccountLabel.AutoSize = true;
            this.NoAccountLabel.Location = new System.Drawing.Point(324, 231);
            this.NoAccountLabel.Name = "No Account Label";
            this.NoAccountLabel.Size = new System.Drawing.Size(81, 13);
            this.NoAccountLabel.TabIndex = 16;
            this.NoAccountLabel.Text = "No account?";
            // 
            // OpenRegistrationLabel
            // 
            this.OpenRegistrationLabel.AutoSize = true;
            this.OpenRegistrationLabel.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.OpenRegistrationLabel.Location = new System.Drawing.Point(411, 231);
            this.OpenRegistrationLabel.Name = "Open Registration Label";
            this.OpenRegistrationLabel.Size = new System.Drawing.Size(72, 13);
            this.OpenRegistrationLabel.TabIndex = 17;
            this.OpenRegistrationLabel.Text = "Registraion";
            this.OpenRegistrationLabel.Click += new System.EventHandler(this.OpenRegistrationLabel_Click);

            LoginControls = new Control[] { LoginLabel, LoginInput, PasswordLabel, PasswordInput, LoginButton, NoAccountLabel, OpenRegistrationLabel };
            #endregion
            #region Registration Page Controls
            // 
            // RegistrationButton
            // 
            this.RegistrationButton.Location = new System.Drawing.Point(344, 201);
            this.RegistrationButton.Name = "RegistrationButton";
            this.RegistrationButton.Size = new System.Drawing.Size(100, 23);
            this.RegistrationButton.TabIndex = 12;
            this.RegistrationButton.Text = "Register";
            this.RegistrationButton.UseVisualStyleBackColor = true;
            this.RegistrationButton.Click += new System.EventHandler(this.RegistrationButton_Click);
            RegistrationControls = new Control[] { LoginLabel, LoginInput, PasswordLabel, PasswordInput, RegistrationButton }; 
            #endregion
            #region Chat Page Controls
            // 
            // ChatLabel
            // 
            this.ChatLabel.AutoSize = true;
            this.ChatLabel.Location = new System.Drawing.Point(139, 222);
            this.ChatLabel.Name = "Chat Label";
            this.ChatLabel.Size = new System.Drawing.Size(29, 13);
            this.ChatLabel.TabIndex = 7;
            this.ChatLabel.Text = "Chat:";
            // 
            // ChatWindow
            // 
            this.ChatWindow.Location = new System.Drawing.Point(41, 238);
            this.ChatWindow.Multiline = true;
            this.ChatWindow.Name = "Chat Window";
            this.ChatWindow.Size = new System.Drawing.Size(225, 117);
            this.ChatWindow.TabIndex = 3;
            // 
            // NewMessageLabel
            // 
            this.NewMessageLabel.AutoSize = true;
            this.NewMessageLabel.Location = new System.Drawing.Point(501, 222);
            this.NewMessageLabel.Name = "New Message Label";
            this.NewMessageLabel.Size = new System.Drawing.Size(68, 13);
            this.NewMessageLabel.TabIndex = 8;
            this.NewMessageLabel.Text = "Message:";
            // 
            // NewMessageInput
            // 
            this.NewMessageInput.Location = new System.Drawing.Point(411, 238);
            this.NewMessageInput.Name = "New Message Input";
            this.NewMessageInput.Size = new System.Drawing.Size(246, 20);
            this.NewMessageInput.TabIndex = 4;
            // 
            // SendMessageButton
            // 
            this.SendMessageButton.Location = new System.Drawing.Point(663, 238);
            this.SendMessageButton.Name = "Send Message Button";
            this.SendMessageButton.Size = new System.Drawing.Size(100, 23);
            this.SendMessageButton.TabIndex = 9;
            this.SendMessageButton.Text = "Send";
            this.SendMessageButton.UseVisualStyleBackColor = true;
            this.SendMessageButton.Click += new System.EventHandler(this.SendMessageButton_Click);

            ChatControls = new Control[] { ChatLabel, ChatWindow, NewMessageLabel, NewMessageInput, SendMessageButton };
            #endregion
            // 
            // HelloLabel
            // 
            this.HelloLabel.AutoSize = true;
            this.HelloLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.HelloLabel.Location = new System.Drawing.Point(231, 41);
            this.HelloLabel.Name = "Hello Label";
            this.HelloLabel.Size = new System.Drawing.Size(351, 31);
            this.HelloLabel.TabIndex = 13;
            this.HelloLabel.Text = "Welcome to chat!";            
           
            // 
            // Interface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "Interface";
            this.Text = "Chat";
            this.SizeChanged += Interface_SizeChanged;
            Controls.AddRange(ServerConnectionControls);
            Controls.Add(RegistrationButton);
            Controls.AddRange(LoginControls);
            Controls.AddRange(ChatControls);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void Interface_SizeChanged(object sender, System.EventArgs e)
        {
             
        }

        #endregion

        internal Button ConnectButton;
        internal Button RegistrationButton;
        internal TextBox IPInput;
        internal TextBox PortInput;
        internal TextBox ChatWindow;
        internal TextBox NewMessageInput;
        internal Label IPInputLabel;
        internal Label PortInputLabel;
        internal Label ChatLabel;
        internal Label NewMessageLabel;
        internal Button SendMessageButton;
        internal Label LoginLabel;
        internal TextBox LoginInput;
        internal Button LoginButton;
        internal Label HelloLabel;
        internal Label PasswordLabel;
        internal TextBox PasswordInput;
        internal Label NoAccountLabel;
        internal Label OpenRegistrationLabel;
    }
}

