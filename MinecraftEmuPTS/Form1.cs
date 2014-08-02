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

namespace MinecraftEmuPTS
{
    struct ConnectInfo
    {
        public String host;
        public String port;
        public bool FMLserver;
    }
    public partial class Form1 : Form
    {
        IPAddress ServerIP = IPAddress.Parse("144.76.9.205");
        int ServerPort = 24444;
        int ProtocolVersion = 78;

        public Form1()
        {
            InitializeComponent();
        }
  
        private void button1_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ParameterizedThreadStart(ConnectionThread));
            ConnectInfo inf;

            inf.host = textBox1.Text;
            inf.port = textBox2.Text;
            inf.FMLserver = false;

            try
            {
                thread.Start(inf);
            }
            catch (SocketException ex)
            {
                Console.WriteLine("Socket Exception: " + ex.ToString());
            }
            catch (IOException ex)
            {
                Console.WriteLine("IO Exception: " + ex.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Win32.AllocConsole();
        }

        static void ConnectionThread(Object StateInfo)
        {
            new ConnectionManager(((ConnectInfo)StateInfo).host,((ConnectInfo)StateInfo).port,((ConnectInfo)StateInfo).FMLserver);
        }
    }
}
