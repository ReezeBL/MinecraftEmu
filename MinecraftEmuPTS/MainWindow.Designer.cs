namespace MinecraftEmuPTS
{
    partial class MainWindow
    {
        /// <summary>
        /// Требуется переменная конструктора.
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.connect = new System.Windows.Forms.Button();
            this.host = new System.Windows.Forms.TextBox();
            this.port = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.UpdateTick = new System.Windows.Forms.Timer(this.components);
            this.console = new System.Windows.Forms.RichTextBox();
            this.chat_message = new System.Windows.Forms.TextBox();
            this.chat_send = new System.Windows.Forms.Button();
            this.PlayersNear = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.ReconnectFlag = new System.Windows.Forms.CheckBox();
            this.Ping = new System.Windows.Forms.Button();
            this.AntiafkFlag = new System.Windows.Forms.CheckBox();
            this.AutofeedFlag = new System.Windows.Forms.CheckBox();
            this.OnlineTimer = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.timer_online = new System.Windows.Forms.Timer(this.components);
            this.disableChat = new System.Windows.Forms.CheckBox();
            this.clearTime = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // connect
            // 
            this.connect.Location = new System.Drawing.Point(29, 46);
            this.connect.Name = "connect";
            this.connect.Size = new System.Drawing.Size(133, 29);
            this.connect.TabIndex = 0;
            this.connect.Text = "Connect";
            this.connect.UseVisualStyleBackColor = true;
            this.connect.Click += new System.EventHandler(this.button1_Click);
            // 
            // host
            // 
            this.host.Location = new System.Drawing.Point(30, 81);
            this.host.Name = "host";
            this.host.Size = new System.Drawing.Size(132, 20);
            this.host.TabIndex = 3;
            this.host.Text = "5.196.77.107";
            // 
            // port
            // 
            this.port.Location = new System.Drawing.Point(206, 80);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(121, 20);
            this.port.TabIndex = 4;
            this.port.Text = "24444";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(30, 20);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(132, 20);
            this.textBox3.TabIndex = 5;
            this.textBox3.Text = "NoliMultiply";
            // 
            // UpdateTick
            // 
            this.UpdateTick.Enabled = true;
            this.UpdateTick.Interval = 500;
            this.UpdateTick.Tick += new System.EventHandler(this.UpdateTimer_Tick);
            // 
            // console
            // 
            this.console.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.console.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.console.ForeColor = System.Drawing.SystemColors.WindowText;
            this.console.Location = new System.Drawing.Point(30, 106);
            this.console.Name = "console";
            this.console.ReadOnly = true;
            this.console.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.console.Size = new System.Drawing.Size(529, 227);
            this.console.TabIndex = 0;
            this.console.Text = "";
            // 
            // chat_message
            // 
            this.chat_message.Location = new System.Drawing.Point(29, 339);
            this.chat_message.Name = "chat_message";
            this.chat_message.Size = new System.Drawing.Size(422, 20);
            this.chat_message.TabIndex = 7;
            // 
            // chat_send
            // 
            this.chat_send.Location = new System.Drawing.Point(463, 339);
            this.chat_send.Name = "chat_send";
            this.chat_send.Size = new System.Drawing.Size(85, 20);
            this.chat_send.TabIndex = 8;
            this.chat_send.Text = "Send";
            this.chat_send.UseVisualStyleBackColor = true;
            this.chat_send.Click += new System.EventHandler(this.button3_Click);
            // 
            // PlayersNear
            // 
            this.PlayersNear.FormattingEnabled = true;
            this.PlayersNear.Location = new System.Drawing.Point(583, 105);
            this.PlayersNear.Name = "PlayersNear";
            this.PlayersNear.Size = new System.Drawing.Size(195, 160);
            this.PlayersNear.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(580, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Players Nearby:";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(587, 21);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(190, 53);
            this.richTextBox1.TabIndex = 11;
            this.richTextBox1.Text = "";
            // 
            // ReconnectFlag
            // 
            this.ReconnectFlag.AutoSize = true;
            this.ReconnectFlag.Location = new System.Drawing.Point(380, 12);
            this.ReconnectFlag.Name = "ReconnectFlag";
            this.ReconnectFlag.Size = new System.Drawing.Size(99, 17);
            this.ReconnectFlag.TabIndex = 13;
            this.ReconnectFlag.Text = "Auto-reconnect";
            this.ReconnectFlag.UseVisualStyleBackColor = true;
            this.ReconnectFlag.CheckedChanged += new System.EventHandler(this.ReconnectFlag_CheckedChanged);
            // 
            // Ping
            // 
            this.Ping.Location = new System.Drawing.Point(202, 45);
            this.Ping.Name = "Ping";
            this.Ping.Size = new System.Drawing.Size(125, 29);
            this.Ping.TabIndex = 14;
            this.Ping.Text = "Ping Server";
            this.Ping.UseVisualStyleBackColor = true;
            this.Ping.Click += new System.EventHandler(this.Ping_Click);
            // 
            // AntiafkFlag
            // 
            this.AntiafkFlag.AutoSize = true;
            this.AntiafkFlag.Location = new System.Drawing.Point(380, 35);
            this.AntiafkFlag.Name = "AntiafkFlag";
            this.AntiafkFlag.Size = new System.Drawing.Size(62, 17);
            this.AntiafkFlag.TabIndex = 15;
            this.AntiafkFlag.Text = "Anti-afk";
            this.AntiafkFlag.UseVisualStyleBackColor = true;
            this.AntiafkFlag.CheckedChanged += new System.EventHandler(this.AntiafkFlag_CheckedChanged);
            // 
            // AutofeedFlag
            // 
            this.AutofeedFlag.AutoSize = true;
            this.AutofeedFlag.Location = new System.Drawing.Point(380, 58);
            this.AutofeedFlag.Name = "AutofeedFlag";
            this.AutofeedFlag.Size = new System.Drawing.Size(72, 17);
            this.AutofeedFlag.TabIndex = 16;
            this.AutofeedFlag.Text = "Auto-feed";
            this.AutofeedFlag.UseVisualStyleBackColor = true;
            this.AutofeedFlag.CheckedChanged += new System.EventHandler(this.AutofeedFlag_CheckedChanged);
            // 
            // OnlineTimer
            // 
            this.OnlineTimer.Location = new System.Drawing.Point(583, 294);
            this.OnlineTimer.Name = "OnlineTimer";
            this.OnlineTimer.ReadOnly = true;
            this.OnlineTimer.Size = new System.Drawing.Size(100, 20);
            this.OnlineTimer.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(584, 278);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Online timer:";
            // 
            // timer_online
            // 
            this.timer_online.Interval = 1000;
            // 
            // disableChat
            // 
            this.disableChat.AutoSize = true;
            this.disableChat.Location = new System.Drawing.Point(380, 80);
            this.disableChat.Name = "disableChat";
            this.disableChat.Size = new System.Drawing.Size(85, 17);
            this.disableChat.TabIndex = 19;
            this.disableChat.Text = "Disable chat";
            this.disableChat.UseVisualStyleBackColor = true;
            this.disableChat.CheckedChanged += new System.EventHandler(this.disableChat_CheckedChanged);
            // 
            // clearTime
            // 
            this.clearTime.Location = new System.Drawing.Point(583, 320);
            this.clearTime.Name = "clearTime";
            this.clearTime.Size = new System.Drawing.Size(100, 20);
            this.clearTime.TabIndex = 20;
            this.clearTime.Text = "Clear time";
            this.clearTime.UseVisualStyleBackColor = true;
            this.clearTime.Click += new System.EventHandler(this.clearTime_Click);
            // 
            // MainWindow
            // 
            this.AcceptButton = this.chat_send;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 371);
            this.Controls.Add(this.clearTime);
            this.Controls.Add(this.disableChat);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.OnlineTimer);
            this.Controls.Add(this.AutofeedFlag);
            this.Controls.Add(this.AntiafkFlag);
            this.Controls.Add(this.Ping);
            this.Controls.Add(this.ReconnectFlag);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PlayersNear);
            this.Controls.Add(this.chat_send);
            this.Controls.Add(this.chat_message);
            this.Controls.Add(this.console);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.port);
            this.Controls.Add(this.host);
            this.Controls.Add(this.connect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "MainWindow";
            this.Text = "Minecraft Emulator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button connect;
        private System.Windows.Forms.TextBox host;
        private System.Windows.Forms.TextBox port;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Timer UpdateTick;
        private System.Windows.Forms.RichTextBox console;
        private System.Windows.Forms.TextBox chat_message;
        private System.Windows.Forms.Button chat_send;
        private System.Windows.Forms.ListBox PlayersNear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.CheckBox ReconnectFlag;
        private System.Windows.Forms.Button Ping;
        private System.Windows.Forms.CheckBox AntiafkFlag;
        private System.Windows.Forms.CheckBox AutofeedFlag;
        private System.Windows.Forms.TextBox OnlineTimer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timer_online;
        private System.Windows.Forms.CheckBox disableChat;
        private System.Windows.Forms.Button clearTime;
    }
}

