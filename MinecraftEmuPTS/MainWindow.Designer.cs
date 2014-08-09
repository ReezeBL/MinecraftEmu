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
            this.test = new System.Windows.Forms.Button();
            this.host = new System.Windows.Forms.TextBox();
            this.port = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.UpdateTick = new System.Windows.Forms.Timer(this.components);
            this.console = new System.Windows.Forms.RichTextBox();
            this.AntiAfkEvent = new System.Windows.Forms.Timer(this.components);
            this.AntiAfkMove = new System.Windows.Forms.Timer(this.components);
            this.chat_message = new System.Windows.Forms.TextBox();
            this.chat_send = new System.Windows.Forms.Button();
            this.PlayersNear = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.CrushFlag = new System.Windows.Forms.CheckBox();
            this.ReconnectFlag = new System.Windows.Forms.CheckBox();
            this.Ping = new System.Windows.Forms.Button();
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
            // test
            // 
            this.test.Location = new System.Drawing.Point(371, 73);
            this.test.Name = "test";
            this.test.Size = new System.Drawing.Size(122, 28);
            this.test.TabIndex = 2;
            this.test.Text = "Antiafk";
            this.test.UseVisualStyleBackColor = true;
            this.test.Click += new System.EventHandler(this.button2_Click);
            // 
            // host
            // 
            this.host.Location = new System.Drawing.Point(30, 81);
            this.host.Name = "host";
            this.host.Size = new System.Drawing.Size(132, 20);
            this.host.TabIndex = 3;
            this.host.Text = "144.76.9.205";
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
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // UpdateTick
            // 
            this.UpdateTick.Enabled = true;
            this.UpdateTick.Interval = 500;
            this.UpdateTick.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // console
            // 
            this.console.BackColor = System.Drawing.SystemColors.Control;
            this.console.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.console.Location = new System.Drawing.Point(30, 106);
            this.console.Name = "console";
            this.console.ReadOnly = true;
            this.console.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.console.Size = new System.Drawing.Size(529, 227);
            this.console.TabIndex = 0;
            this.console.Text = "";
            // 
            // AntiAfkEvent
            // 
            this.AntiAfkEvent.Interval = 50000;
            this.AntiAfkEvent.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // AntiAfkMove
            // 
            this.AntiAfkMove.Interval = 1000;
            this.AntiAfkMove.Tick += new System.EventHandler(this.timer3_Tick);
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
            // CrushFlag
            // 
            this.CrushFlag.AutoSize = true;
            this.CrushFlag.Location = new System.Drawing.Point(371, 23);
            this.CrushFlag.Name = "CrushFlag";
            this.CrushFlag.Size = new System.Drawing.Size(104, 17);
            this.CrushFlag.TabIndex = 12;
            this.CrushFlag.Text = "Crush Master =P";
            this.CrushFlag.UseVisualStyleBackColor = true;
            this.CrushFlag.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // ReconnectFlag
            // 
            this.ReconnectFlag.AutoSize = true;
            this.ReconnectFlag.Location = new System.Drawing.Point(371, 53);
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
            // MainWindow
            // 
            this.AcceptButton = this.chat_send;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 371);
            this.Controls.Add(this.Ping);
            this.Controls.Add(this.ReconnectFlag);
            this.Controls.Add(this.CrushFlag);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PlayersNear);
            this.Controls.Add(this.chat_send);
            this.Controls.Add(this.chat_message);
            this.Controls.Add(this.console);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.port);
            this.Controls.Add(this.host);
            this.Controls.Add(this.test);
            this.Controls.Add(this.connect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "MainWindow";
            this.Text = "Minecraft Emulator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button connect;
        private System.Windows.Forms.Button test;
        private System.Windows.Forms.TextBox host;
        private System.Windows.Forms.TextBox port;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Timer UpdateTick;
        private System.Windows.Forms.RichTextBox console;
        private System.Windows.Forms.Timer AntiAfkEvent;
        private System.Windows.Forms.Timer AntiAfkMove;
        private System.Windows.Forms.TextBox chat_message;
        private System.Windows.Forms.Button chat_send;
        private System.Windows.Forms.ListBox PlayersNear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.CheckBox CrushFlag;
        private System.Windows.Forms.CheckBox ReconnectFlag;
        private System.Windows.Forms.Button Ping;
    }
}

