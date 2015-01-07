using MinecraftEmuPTS.GameInfo;
using MinecraftEmuPTS.NetHandler;
using MinecraftEmuPTS.Packets;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace MinecraftEmuPTS
{
    struct ConnectInfo
    {
        public String host;
        public String port;
        public bool FMLserver;
    }

    struct ConsoleLine
    {
        public String text;
        public Color color;
        public String font;
    }

    partial class MainWindow : Form
    {
        IPAddress ServerIP = IPAddress.Parse("144.76.9.205");
        int ServerPort = 24444;
        int ProtocolVersion = 78;

        public static String username = "NoliMultiply";
        public static volatile Queue<ConsoleLine> ConsoleStack = new Queue<ConsoleLine>();
        public static bool AutoFeed = false;
        private static bool Reconnect = false;

        private delegate void ConsoleWriteFormated_(ConsoleLine line);
        private delegate void TimerCallback_(Object obj);

        public ConnectionManager cManager = new ConnectionManager();
        public TimeSpan time;
        
        private System.Threading.Timer onlineTimer;       
        private System.Threading.Timer connectTimer;
        private System.Threading.Timer afkTimer;
        private Int32 dir = 1;

        public MainWindow()
        {
            InitializeComponent();            
        }       
        void cManager_onPlayerDeath()
        {
            cManager.pControl.Respawn();
        }
        void ConnectionManager_onClientDisconnect()
        {
            if (onlineTimer != null)
                onlineTimer.Dispose();
            if (ReconnectFlag.Checked)
            {
                if (connectTimer != null)
                    connectTimer.Dispose();

                ConnectInfo inf;
                username = textBox3.Text;
                cManager.player.name = username;
                inf.host = host.Text;
                inf.port = port.Text;
                inf.FMLserver = true;
                BotSettings.Default.HostIP = inf.host;
                BotSettings.Default.Port = inf.port;
                BotSettings.Default.Username = username;
                connectTimer = new System.Threading.Timer(ConnectionThread, inf, 3000, System.Threading.Timeout.Infinite);

            }
        }
        void Client_onClientConnect()
        {
            onlineTimer = new System.Threading.Timer(TimerCallback, null, 100, 1000);
        }      
        private void button1_Click(object sender, EventArgs e)
        {
            if (cManager.Connected)
            {               
                CustomLib.putsc("Client is already running!\n", Color.Aquamarine);
                return;
            }
            InitConnecting();

        }        
        private void MainWindow_Load(object sender, EventArgs e)
        {
            ModList.Init("MODLIST.txt");
            Packet.Init();
            CustomLib.form = this;
            time = BotSettings.Default.timer;            
            cManager.player.name = username;

            cManager.onClientDisconnect += ConnectionManager_onClientDisconnect;
            cManager.onClientConnect += Client_onClientConnect;
            cManager.onPlayerDeath += cManager_onPlayerDeath;            

            console.MaxLength = Int32.MaxValue;
            textBox3.Text = BotSettings.Default.Username;
            host.Text = BotSettings.Default.HostIP;
            port.Text = BotSettings.Default.Port;
            time = BotSettings.Default.timer;
            OnlineTimer.Text = BotSettings.Default.timer.ToString();
        }              
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            if (cManager.Connected)
            {
                richTextBox1.Text = cManager.player.ToString() + "\nHealth: " + (int)cManager.player.health + "\nHunger: " + (int)cManager.player.hunger;
                PlayersNear.Items.Clear();
                lock (cManager.PlayerList.Values)
                {
                    foreach (EntityPlayer p in cManager.PlayerList.Values)
                    {
                        PlayersNear.Items.Add(p);
                    }
                }
            }
        }
        public void ConsoleWriteFormated(ConsoleLine line)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ConsoleWriteFormated_(ConsoleWriteFormated), line);
            }
            else
            {
                console.AppendText(line.text);
                console.Select(console.Text.Length - line.text.Length, line.text.Length);
                console.SelectionColor = line.color;
                if (line.font == "italic")
                    console.SelectionFont = new Font("Cambria", 12, FontStyle.Italic);
                console.Select(console.Text.Length, console.Text.Length);
                console.ScrollToCaret();                
            }    
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (chat_message.Text.StartsWith("-"))
            {
                String p = chat_message.Text.Substring(1);
                if (p.IndexOf(' ') != -1)
                {
                    String command = p.Substring(0, p.IndexOf(' '));
                    String param = p.Substring(p.IndexOf(' ') + 1);

                    cManager.Tasks.Enqueue(command);
                    cManager.Tasks.Enqueue(param);
                }
                else
                {
                    if (p == "cls")
                    {
                        console.Clear();
                    }
                    else
                    {
                        cManager.Tasks.Enqueue(p);
                    }
                }
                chat_message.Clear();
                return;

            }
            if (chat_message.Text != "")
            {
                cManager.Tasks.Enqueue("chat");
                cManager.Tasks.Enqueue(chat_message.Text);
                chat_message.Clear();
            }

        }      
        private void PingServer(Object obj)
        {
            TcpClient Client = new TcpClient();          
            CustomLib.putsc("Pinging server..\n", Color.Aquamarine);
            try
            {
                Client.Connect(IPAddress.Parse(host.Text), Convert.ToInt32(port.Text));
                BinaryWriter bwr = new BinaryWriter(Client.GetStream());
                BinaryReader br = new BinaryReader(Client.GetStream());
                PacketServerPing packet = new PacketServerPing(78, host.Text, Convert.ToInt32(port.Text));                
                packet.Write(bwr);
                PacketDisconnect packet2 = new PacketDisconnect();
                if (br.ReadByte() != 255)
                {
                    throw new IOException("Bad message");
                }
                packet2.Read(br);
                String message = packet2.getReason();
                message = message.Substring(2);
                String[] pars = message.Split("\0".ToCharArray());
                CustomLib.putsc("Server protocol: " + pars[1] + "\nServer version: " + pars[2] + "\nServer name: " + pars[3] + "\nServer online: " + pars[4] + "/" + pars[5] + "\n", Color.Aquamarine);
                Client.Close();                
            }
            catch (SocketException ex)
            {
                CustomLib.putsc("Unable to connect to server!\n", Color.Aquamarine);
                Console.WriteLine(ex);
            }
            catch (IOException ex)
            {
                CustomLib.putsc("Error during processing data!\n", Color.Aquamarine);
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                Console.WriteLine(ex);
            }
        }
        private void Ping_Click(object sender, EventArgs e)
        {           
            System.Threading.Timer timer = new System.Threading.Timer(PingServer, null, 0, Timeout.Infinite);
            Thread.Sleep(100);
            timer.Dispose();
        }
        private void ReconnectFlag_CheckedChanged(object sender, EventArgs e)
        {
            Reconnect = ReconnectFlag.Checked;
        }
        private void AntiafkFlag_CheckedChanged(object sender, EventArgs e)
        {
            bool f = AntiafkFlag.Checked;
            if (f)
            {
                afkTimer = new System.Threading.Timer(AfkCallback, null, 100, 60000);
            }
            else
                afkTimer.Dispose();
        }
        private void AutofeedFlag_CheckedChanged(object sender, EventArgs e)
        {
            AutoFeed = AutofeedFlag.Checked;
            if (AutoFeed)
            {
                cManager.Tasks.Enqueue("feed");
                cManager.Tasks.Enqueue("on");
            }
            else
            {
                cManager.Tasks.Enqueue("feed");
                cManager.Tasks.Enqueue("off");
            }
        }
        private void TimerCallback(Object obj)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new TimerCallback_(TimerCallback), obj);
            }
            else
            {
                time = time.Add(new TimeSpan(0, 0, 1));
                OnlineTimer.Text = time.ToString();
                BotSettings.Default.timer = time;
            }
        }
        private void AfkCallback(Object obj)
        {
            cManager.pControl.MovePlayer("x", dir);
            dir = -dir;          
        }
        private void ConnectionThread(Object StateInfo)
        {           
            CustomLib.putsc("Connecting to server..\n", Color.Aquamarine);
            cManager.Connect(((ConnectInfo)StateInfo).host, ((ConnectInfo)StateInfo).port, ((ConnectInfo)StateInfo).FMLserver);                      
        }
        private void InitConnecting()
        {
            if (cManager.Connected)
            {
                CustomLib.putsc("Client is already running!\n", Color.Aquamarine);
                return;
            }           
            ConnectInfo inf;

            username = textBox3.Text;
            cManager.player.name = username;

            inf.host = host.Text;
            inf.port = port.Text;
            inf.FMLserver = true;

            BotSettings.Default.HostIP = inf.host;
            BotSettings.Default.Port = inf.port;
            BotSettings.Default.Username = username;

            if (connectTimer != null)
                connectTimer.Dispose();

            connectTimer = new System.Threading.Timer(ConnectionThread, inf, 100, System.Threading.Timeout.Infinite);
        }
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            BotSettings.Default.Save();
            if (connectTimer != null)
                connectTimer.Dispose();
            if (afkTimer != null)
                afkTimer.Dispose();
            if (onlineTimer != null)
                onlineTimer.Dispose();
        }

        private void clearTime_Click(object sender, EventArgs e)
        {
            time = new TimeSpan();
            OnlineTimer.Text = time.ToString();
            BotSettings.Default.timer = time;
        }

        private void disableChat_CheckedChanged(object sender, EventArgs e)
        {
            cManager.HandleChatMessage = !disableChat.Checked;
        }
    }
}
