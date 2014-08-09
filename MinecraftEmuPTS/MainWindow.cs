using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MinecraftEmuPTS.Packets;
using MinecraftEmuPTS.Encription;
using MinecraftEmuPTS.GameInfo;
using MinecraftEmuPTS.NetHandler;

namespace MinecraftEmuPTS
{
    struct ConnectInfo
    {
        public String host;
        public String port;
        public bool FMLserver;
    }

    public struct ConsoleLine
    {
        public String text;
        public Color color;
        public String font;
    }

    public partial class MainWindow : Form
    {
        IPAddress ServerIP = IPAddress.Parse("144.76.9.205");
        int ServerPort = 24444;
        int ProtocolVersion = 78;

        public static String username = "NoliMultiply";
        public static volatile Queue<ConsoleLine> ConsoleStack = new Queue<ConsoleLine>();
        public static bool DoCrash = false;
        private static bool Reconnect = false;
        public static TcpClient Clientik;


        private static Thread thread = new Thread(new ParameterizedThreadStart(ConnectionThread));
        private static bool moovedir = false;
        private static int steps = 0;

        private void ThreadCallback(Object obj)
        {            
            thread = new Thread(new ParameterizedThreadStart(ConnectionThread));
            ConsoleLine line;
            line.text = "Connecting to server..";
            line.color = Color.DarkGreen;
            line.font = "";
            ConsoleStack.Enqueue(line);


            thread.IsBackground = true;
            ConnectInfo inf;

            ConnectionManager.player.name = username;

            inf.host = host.Text;
            inf.port = port.Text;
            inf.FMLserver = true;
            try
            {
                thread.Start(inf);
            }
            catch (SocketException ex)
            {
                Logger.WriteLog(ex);
                Console.WriteLine("Socket Exception: " + ex.ToString());
            }
            catch (IOException ex)
            {
                Logger.WriteLog(ex);
                Console.WriteLine("IO Exception: " + ex.ToString());
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            ModList.Init("MODLIST.txt");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ConnectionManager.InGame)
            {
                ConsoleLine line;
                line.text = "Client is already running!";
                line.color = Color.DarkGreen;
                line.font = "";
                ConsoleStack.Enqueue(line);
                return;
            }


            ThreadCallback(null);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AntiAfkEvent.Enabled = !AntiAfkEvent.Enabled;
            ConsoleLine line;
            if (AntiAfkEvent.Enabled)
            {
                line.text = "Anti-afk started!";
            }
            else
            {
                line.text = "Anti-afk stoped!";
            }
            line.color = Color.DarkGreen;
            line.font = "";
            ConsoleStack.Enqueue(line);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Win32.AllocConsole();
            ConnectionManager.player.name = username;
        }

        static void ConnectionThread(Object StateInfo)
        {
            new ConnectionManager(((ConnectInfo)StateInfo).host, ((ConnectInfo)StateInfo).port, ((ConnectInfo)StateInfo).FMLserver);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            username = textBox3.Text;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            richTextBox1.Text = ConnectionManager.player.ToString() + "\nHealth: " + (int)ConnectionManager.player.health + "\nHunger: " + (int)ConnectionManager.player.hunger;
            if (Clientik != null && Clientik.Connected==false)
            {
                if (Reconnect)
                {
                    //System.Threading.Timer timer = new System.Threading.Timer(ThreadCallback, null,10000, System.Threading.Timeout.Infinite);
                    //ConnectionManager.InGame = true;
                    ThreadCallback(null);
                }
            }

            /*if (ConnectionManager.player.getHeldItem() == null && thread.IsAlive)
            {
                if (ConnectionManager.player.HeldSlot == 8)
                {
                    ConnectionManager.Tasks.Enqueue("slot");
                    ConnectionManager.Tasks.Enqueue("0");
                }
                else
             
                    ConnectionManager.Tasks.Enqueue("slot");
                    ConnectionManager.Tasks.Enqueue((ConnectionManager.player.HeldSlot+1).ToString());
                }
            }*/

            if (ConsoleStack.Count > 0)
            {
                for (int i = 0; i < ConsoleStack.Count; i++)
                {
                    ConsoleLine line = ConsoleStack.Dequeue();
                    console.AppendText(Environment.NewLine);
                    console.AppendText(line.text);
                    console.Select(console.Text.Length - line.text.Length, line.text.Length);
                    console.SelectionColor = line.color;
                    if (line.font == "italic")
                        console.SelectionFont = new Font("Cambria", 12, FontStyle.Italic);
                    console.Select(console.Text.Length, console.Text.Length);
                    console.ScrollToCaret();
                }
            }
            lock (ConnectionManager.PlayerList)
            {
                PlayersNear.Items.Clear();
                foreach (EntityPlayer player in ConnectionManager.PlayerList.Values)
                {
                    PlayersNear.Items.Add(player);
                }
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (steps > 1)
            {
                AntiAfkMove.Enabled = false;
                steps = 0;
            }
            if (moovedir)
                ConnectionManager.player.x += 1;
            else
                ConnectionManager.player.x -= 1;
            steps++;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            moovedir = !moovedir;
            AntiAfkMove.Enabled = true;
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

                    ConnectionManager.Tasks.Enqueue(command);
                    ConnectionManager.Tasks.Enqueue(param);
                }
                else
                {
                    ConnectionManager.Tasks.Enqueue(p);
                }
                chat_message.Clear();
                return;

            }
            if (chat_message.Text != "")
            {
                ConnectionManager.Tasks.Enqueue("chat");
                ConnectionManager.Tasks.Enqueue(chat_message.Text);
                chat_message.Clear();
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            DoCrash = CrushFlag.Checked;
        }

        private void PingServer(Object obj)
        {
            TcpClient Client = new TcpClient();
            ConsoleLine line;
            line.color = Color.DarkGreen;
            line.font = "";
            line.text = "Pinging server..";
            ConsoleStack.Enqueue(line);
            try
            {
                Client.Connect(IPAddress.Parse(host.Text), Convert.ToInt32(port.Text));
                BinaryWriter bwr = new BinaryWriter(Client.GetStream());
                BinaryReader br = new BinaryReader(Client.GetStream());
                PacketServerPing packet = new PacketServerPing(78, host.Text, Convert.ToInt32(port.Text));
                //packet.Write();
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
                line.text = "Server protocol: " + pars[1] + "\nServer version: " + pars[2] + "\nServer name: " + pars[3] + "\nServer online: " + pars[4] + "/" + pars[5];
                ConsoleStack.Enqueue(line);
                Client.Close();
            }
            catch (SocketException ex)
            {
                Logger.WriteLog(ex);
                line.text = "Unable to connect to server!";
                ConsoleStack.Enqueue(line);
                Console.WriteLine(ex);
            }
            catch (IOException ex)
            {
                Logger.WriteLog(ex);
                line.text = "Error during processing data!";
                ConsoleStack.Enqueue(line);
                Console.WriteLine(ex);
            }
        }

        private void Ping_Click(object sender, EventArgs e)
        {
            System.Threading.Timer timer = new System.Threading.Timer(PingServer, null, 0, Timeout.Infinite);
        }

        private void ReconnectFlag_CheckedChanged(object sender, EventArgs e)
        {
            Reconnect = ReconnectFlag.Checked;
        }

    }
}
