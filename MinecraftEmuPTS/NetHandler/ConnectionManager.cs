using MinecraftEmuPTS.Encription;
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

namespace MinecraftEmuPTS
{
    class ConnectionManager
    {
        #region Variables

        private TcpClient Client;      
        private BinaryReader input;
        private BinaryWriter output;
       
        public  EntityPlayer player;
        public  Queue<String> Tasks;
        public  Dictionary<int, EntityPlayer> PlayerList;

        #region Events
        public  delegate void ClientDisconnectDelegate();
        public  event ClientDisconnectDelegate onClientDisconnect;
        public delegate void ClientConnectDelegate();
        public event ClientConnectDelegate onClientConnect;
        public delegate void PlayerDeadDelegate();
        public event PlayerDeadDelegate onPlayerDeath;
        public delegate void NewPlayerDelegate(EntityPlayer a);
        public event NewPlayerDelegate onNewPlayer;
        public delegate void PlayerRemoveDelegate(EntityPlayer[] a);
        public event PlayerRemoveDelegate onPlayerRemove;

        public void RaiseConnectEvent()
        {
            onClientConnect();
        }
        public void RaiseDisconnectEvent()
        {
            onClientDisconnect();
        }
        public void RaisePlayerDeathEvent()
        {
            onPlayerDeath();
        }
        public void RaiseNewPlayerEvent(EntityPlayer a)
        {
            onNewPlayer(a);
        }
        public void RaisePlayerRemoveEvent(EntityPlayer[] a)
        {
            onPlayerRemove(a);
        }

        #endregion        
       
        private Timer threadin;
        private Timer threadupdate;
  
        Queue<Packet> PacketWrite;
        Queue<Packet> PacketRead;

        public PlayerControl pControl;

        public bool InGame;
        public bool Connected;
        public bool FML;
        public bool HandleChatMessage = true;

        #endregion
        #region ThredStarting        
        private void StartIn(Object obj)
        {
            new ReadThread((ConnectionManager)obj);
        }

        private void StartUpdate(Object obj)
        {
            new UpdateThread((ConnectionManager)obj);
        }
        #endregion

        public ConnectionManager()
        {
            pControl = new PlayerControl(this);
            this.Connected = false;
            this.InGame = false;           

            Tasks = new Queue<string>();
            PlayerList = new Dictionary<int, EntityPlayer>();

            player = new EntityPlayer();
            player.name = MainWindow.username;

            PacketWrite = new Queue<Packet>();
            PacketRead = new Queue<Packet>();
        }

        public void Connect(String host, String port, bool FMLserver)
        {
            this.Connected = false;
            this.InGame = false;
            this.FML = FMLserver;

            Tasks = new Queue<string>();
            PlayerList= new Dictionary<int,EntityPlayer>();            

            player = new EntityPlayer();
            player.name = MainWindow.username;

            PacketWrite = new Queue<Packet>();
            PacketRead = new Queue<Packet>();
            try
            {
                IPAddress ServerIP = IPAddress.Parse(host);
                int ServerPort = Convert.ToInt32(port);
                IPEndPoint Dest = new IPEndPoint(ServerIP, ServerPort);
                Client = new TcpClient();
                Client.Connect(ServerIP, ServerPort);
                if (Client.Connected == false)
                {
                    CustomLib.putsc("Server is unavailible!\n", Color.Aquamarine);
                    this.Dispose();
                    return;
                }

                NetworkStream Channel = Client.GetStream();
                input = new BinaryReader(Channel);
                output = new BinaryWriter(new BufferedStream(Channel, 5120));
                this.Connected = true;

                Thread.Sleep(100);

                threadin = new Timer(StartIn, this, 300, Timeout.Infinite);
                threadupdate = new Timer(StartUpdate, this, 300, Timeout.Infinite);

                AddToSendingQueue(new PacketHandshake(78, MainWindow.username, ServerIP.ToString(), ServerPort));
                Console.WriteLine("Sending handshake...");
                MainLoop();

                CustomLib.putsc("Connection closed..\n", Color.Aquamarine);               
                this.Dispose();                
            }
            catch (ThreadAbortException ex)
            {

            }
            catch (SocketException ex)
            {
                Console.WriteLine("Socket Exception: " + ex.ToString());
                CustomLib.putsc("Server is unavailible!\n",Color.Aquamarine);
                this.Dispose();                
            }
            catch (IOException ex)
            {
                Console.WriteLine("IO Exception: " + ex.ToString());
                this.Dispose();                
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                Console.WriteLine("Exception: " + ex.ToString());
                this.Dispose();              
            }
        }

        private void Dispose()
        {           
            PlayerList.Clear();
            PacketWrite.Clear();
            PacketRead.Clear();
            player = new EntityPlayer();

            this.InGame = false;
            this.Connected = false;

            Thread.Sleep(100);

            threadin.Dispose();
            threadupdate.Dispose();

            if(Client.Connected)
                Client.GetStream().Dispose();
            Client.Close();

            Thread.Sleep(1400);          
            this.RaiseDisconnectEvent();
        }
                       
        public void SendPacket()
        {
            while(PacketWrite.Count > 0)
            {
                Packet packet = PacketWrite.Dequeue();
                if (packet == null) return;
                packet.Write(output);
                output.Flush();
                if (packet.PacketID == 252)
                {
                    Console.WriteLine("Output stream encrypted");
                    Stream stream = CryptManager.encryptStream(Client.GetStream());
                    output = new BinaryWriter(new BufferedStream(stream, 5120));
                }               
            }
        }
        public Packet GetDividedPacket()
        {
            int ID = input.ReadByte();
            try
            {
                Packet packet = (Packet)Activator.CreateInstance(Packet.PacketMap[ID]);

                packet.Read(input);
                packet.PacketID = ID;
                if (ID == 252)
                {
                    Console.WriteLine("Server authorized!");
                    Stream stream = CryptManager.encryptStream(Client.GetStream());
                    input = new BinaryReader(stream);
                    Console.WriteLine("Input stream encrypted");
                }                
                return packet;
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine("Unknown PacketID: " + ID);
                throw new Exception("Unknown PacketID " + ID);
            }
        }    
        public void AddToSendingQueue(Packet packet)
        {
            PacketWrite.Enqueue(packet);
        }
        public void PutPacket(Packet packet)
        {
            PacketRead.Enqueue(packet);
        }              
        private void MainLoop()
        {
            PacketHandler handler = new PacketHandler(this);
            while (this.Connected)
            {
                while(this.PacketRead.Count > 0)
                {
                    Packet packet = this.PacketRead.Dequeue();
                    if(packet != null)
                        packet.processPacket(handler);
                }                
                Thread.Sleep(10);
            }
            Console.WriteLine("Connection stopped...");
        }             
    }
}
