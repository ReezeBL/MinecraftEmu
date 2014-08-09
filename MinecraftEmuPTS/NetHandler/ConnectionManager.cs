using System;
using System.Collections;
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
using System.Diagnostics;
using MinecraftEmuPTS.Packets;
using MinecraftEmuPTS.Encription;
using MinecraftEmuPTS;
using MinecraftEmuPTS.GameInfo;
using MinecraftEmuPTS.NetHandler;
using Newtonsoft.Json.Linq;

namespace MinecraftEmuPTS
{
    class ConnectionManager
    {
        #region Variables
        private TcpClient Client;
        //private NetworkStream Channel;
        private BinaryReader input;
        private BinaryWriter output;
        private IPAddress ServerIP;
        private int ServerPort;
        private string AuthToken;

        public volatile static EntityPlayer player = new EntityPlayer();
        public static int nChunkPakets = 0;
        public volatile static Queue<String> Tasks = new Queue<string>();
        public volatile static Dictionary<int, EntityPlayer> PlayerList = new Dictionary<int, EntityPlayer>();

        private static List<Packet> Stack = new List<Packet>();

        private Thread threadout;
        private Thread threadin;
        private Thread threadupdate;
  
        Queue<Packet> PacketWrite;
        Queue<Packet> PacketRead;

        public static bool InGame = false;
        public bool Connected;

        bool FML;
        #endregion
        #region ThredStarting
        private void StartOut(Object obj)
        {
            new WriteThread((ConnectionManager)obj);
        }

        private void StartIn(Object obj)
        {
            new ReadThread((ConnectionManager)obj);
        }

        private void StartUpdate(Object obj)
        {
            new UpdateThread((ConnectionManager)obj);
        }
        #endregion
        public ConnectionManager(String host, String port, bool FMLserver)
        {
            this.Connected = false;
            //this.InGame = true;
            this.FML = FMLserver;

            Tasks.Clear();
            PlayerList.Clear();
            player = new EntityPlayer();
            player.name = MainWindow.username;

            PacketWrite = new Queue<Packet>();
            PacketRead = new Queue<Packet>();



            Console.WriteLine("Connecting to server..");
            try
            {

                ServerIP = IPAddress.Parse(host);
                ServerPort = Convert.ToInt32(port);
                IPEndPoint Dest = new IPEndPoint(ServerIP, ServerPort);
                Client = new TcpClient();
                Client.Connect(ServerIP, ServerPort);
                Console.WriteLine("Connection succseed!");
                NetworkStream Channel = Client.GetStream();

                input = new BinaryReader(Channel);
                output = new BinaryWriter(new BufferedStream(Channel, 5120));

                MainWindow.Clientik = Client;
                try
                {
                    threadout = new Thread((ParameterizedThreadStart)StartOut);
                    threadout.IsBackground = true;
                    threadout.Start(this);

                    threadin = new Thread((ParameterizedThreadStart)StartIn);
                    threadin.IsBackground = true;
                    threadin.Start(this);

                    threadupdate = new Thread((ParameterizedThreadStart)StartUpdate);
                    threadupdate.IsBackground = true;
                    threadupdate.Start(this);

                    InitializeLoginSession();

                    PlayerList.Clear();
                    PacketWrite.Clear();
                    PacketRead.Clear();
                    player = new EntityPlayer();

                    InGame = false;

                    Thread.CurrentThread.Abort();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    InGame = false;
                    Logger.WriteLog(ex);
                    Thread.CurrentThread.Abort();
                }
            }
            catch (SocketException ex)
            {
                Logger.WriteLog(ex);
                Console.WriteLine("Socket Exception: " + ex.ToString());
                InGame = false;
            }
            catch (IOException ex)
            {
                Logger.WriteLog(ex);
                Console.WriteLine("IO Exception: " + ex.ToString());
                InGame = false;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                Console.WriteLine("Exception: " + ex.ToString());
                InGame = false;
            }
        }
        public void SendPacket()
        {
            if (PacketWrite.Count > 0)
            {
                Packet packet = PacketWrite.Dequeue();
                if (packet == null) return;
                packet.Write(output);
                output.Flush();
                if (packet.PacketID == 252)
                {
                    Stream stream = CryptManager.encryptStream(Client.GetStream());
                    output = new BinaryWriter(new BufferedStream(stream, 5120));
                }
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
        public Packet GetDividedPacket()
        {
            int ID = input.ReadByte();

            if (ID == 0)
            {
                PacketKeepAlive packet = new PacketKeepAlive();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }

            if (ID == 1)
            {
                PacketLogin packet;
                if (!this.FML)
                    packet = new PacketLogin(true);
                else
                    packet = new PacketLogin(false);
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }
            if (ID == 3)
            {
                PacketChat packet = new PacketChat();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }
            if (ID == 4)
            {
                PacketUpdateTime packet = new PacketUpdateTime();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }
            if (ID == 5)
            {
                PacketPlayerInventory packet = new PacketPlayerInventory();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }
            if (ID == 6)
            {

                PacketSpawnPosition packet = new PacketSpawnPosition();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }
            if (ID == 8)
            {
                PacketUpdateHealth packet = new PacketUpdateHealth();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }
            if (ID == 9)
            {
                PacketRespawn packet = new PacketRespawn();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }
            if (ID == 10)
            {
                PacketFlying packet = new PacketFlying();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;

            }
            if (ID == 13)
            {
                PacketPlayerLookMove packet = new PacketPlayerLookMove();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }

            if (ID == 16)
            {
                PacketBlockItemSwitch packet = new PacketBlockItemSwitch();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }
            if (ID == 18)
            {
                PacketAnimation packet = new PacketAnimation();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }

            if (ID == 20)
            {
                PacketNamedEntitySpawn packet = new PacketNamedEntitySpawn();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }
            if (ID == 22)
            {
                PacketCollect packet = new PacketCollect();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }
           
            if (ID == 23)
            {
                PacketVehicleSpawn packet = new PacketVehicleSpawn();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }

            if (ID == 24)
            {
                PacketMobSpawn packet = new PacketMobSpawn();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }
            if (ID == 25)
            {
                PacketEntityExpOrb packet = new PacketEntityExpOrb();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }
            if (ID == 26)
            {
                PacketEntityExpOrb packet = new PacketEntityExpOrb();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }
            if (ID == 28)
            {
                PacketEntityVelocity packet = new PacketEntityVelocity();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }

            if (ID == 29)
            {
                PacketDestroyEntity packet = new PacketDestroyEntity();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }

            if (ID == 31)
            {
                PacketRelEntityMove packet = new PacketRelEntityMove();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }
            if (ID == 32)
            {
                PacketEntityLook packet = new PacketEntityLook();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }

            if (ID == 33)
            {
                PacketRelEntityMoveLook packet = new PacketRelEntityMoveLook();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }
            if (ID == 34)
            {
                PacketEntityTeleport packet = new PacketEntityTeleport();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }
            if (ID == 35)
            {
                PacketEntityHeadRotation packet = new PacketEntityHeadRotation();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }
            if (ID == 38)
            {
                PacketEntityStatus packet = new PacketEntityStatus();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }

            if (ID == 39)
            {
                PacketAttachEntity packet = new PacketAttachEntity();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }

            if (ID == 40)
            {
                PacketEntityMetadata packet = new PacketEntityMetadata();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }
            if (ID == 43)
            {
                PacketExperience packet = new PacketExperience();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }
            if (ID == 44)
            {
                PacketUpdateAttributes packet = new PacketUpdateAttributes();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }
            if (ID == 51)
            {
                PacketMapChunk packet = new PacketMapChunk();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }
            if (ID == 52)
            {
                PacketMultiBlockChange packet = new PacketMultiBlockChange();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }

            if (ID == 53)
            {
                PacketBlockChange packet = new PacketBlockChange();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }
            if (ID == 54)
            {
                PacketPlayNoteBlock packet = new PacketPlayNoteBlock();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }
            if (ID == 55)
            {
                PacketBlockDestroy packet = new PacketBlockDestroy();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }
            if (ID == 56)
            {
                PacketChunkData packet = new PacketChunkData();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }

            if (ID == 61)
            {
                PacketDoorChange packet = new PacketDoorChange();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }

            if (ID == 62)
            {
                PacketLevelSound packet = new PacketLevelSound();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }

            if (ID == 70)
            {
                PacketGameEvent packet = new PacketGameEvent();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }
            if (ID == 101)
            {
                PacketCloseWindow packet = new PacketCloseWindow();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }
            if (ID == 103)
            {
                PacketSetSlot packet = new PacketSetSlot();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }

            if (ID == 104)
            {
                PacketWindowItems packet = new PacketWindowItems();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }

            if (ID == 130)
            {
                PacketUpdateSign packet = new PacketUpdateSign();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }

            if (ID == 131)
            {
                PacketMapData packet = new PacketMapData();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }

            if (ID == 132)
            {
                PacketTileEntityData packet = new PacketTileEntityData();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;

            }
            if (ID == 200)
            {
                PacketStatistic packet = new PacketStatistic();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }
            if (ID == 201)
            {
                PacketPlayerInfo packet = new PacketPlayerInfo();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }
            if (ID == 202)
            {
                PacketPlayerAbilities packet = new PacketPlayerAbilities();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }

            if (ID == 209)
            {
                PacketSetPlayerTeam packet = new PacketSetPlayerTeam();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }

            if (ID == 250)
            {
                PacketCustomPayload packet = new PacketCustomPayload();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }
            if (ID == 252)
            {
                PacketSharedKey packet = new PacketSharedKey();
                packet.Read(input);
                Console.WriteLine("Server authorized!");
                {
                    Stream stream = CryptManager.encryptStream(Client.GetStream());
                    input = new BinaryReader(stream);
                }
                packet.PacketID = ID;
                return packet;
            }

            if (ID == 253)
            {
                PacketServerAuthData packet = new PacketServerAuthData();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }
            if (ID == 255)
            {
                PacketDisconnect packet = new PacketDisconnect();
                packet.Read(input);
                packet.PacketID = ID;
                return packet;
            }
            Console.WriteLine("Unknown PacketID: " + ID);
            throw new Exception("Unknow Packet");
        }

        private int GetPacket()
        {
            int ID = input.ReadByte();
            return ID;
        }

        private bool AuthMe(PacketServerAuthData packet)
        {
            if ("-".Equals(packet.getServerID()))
            {
                if (this.FML)
                {
                    AddToSendingQueue(FakeFML());
                }
                AddToSendingQueue(new PacketClientCommand(0));
                return true;
            }

            Console.WriteLine("ServerID: " + packet.getServerID());
            CryptManager.DecodePublicKey(packet.GetPublicKey());

            byte[] SharedKey = CryptManager.CreateSecretKey();
            byte[] SharedSecret = CryptManager.EncryptData(SharedKey);
            byte[] VerifyToken = CryptManager.EncryptData(packet.GetVerifyToken());


            String ServerHash = CryptManager.GetServerIdHash(packet.getServerID());
            Console.WriteLine("Server hash: " + ServerHash);
            AuthToken = GetAuthToken();
            String AuthResponse = POSTurl(AuthToken + ServerHash);
            if (AuthResponse.Equals("OK"))
            {
                AddToSendingQueue(new PacketSharedKey(SharedSecret, VerifyToken));
                Thread.Sleep(100);
                if (this.FML)
                {
                    AddToSendingQueue(FakeFML());
                }
                AddToSendingQueue(new PacketClientCommand(0));
                return true;
            }
            return false;
        }

        private void InitializeLoginSession()
        {
            try
            {
                AddToSendingQueue(new PacketHandshake(78, MainWindow.username, ServerIP.ToString(), ServerPort));
                Console.WriteLine("Sending handshake...");
                MainLoop();
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
        private void MainLoop()
        {
            while (HandlePacket())
            {
                if (this.Connected)
                {
                    if (!threadin.IsAlive || !threadout.IsAlive || !threadupdate.IsAlive)
                    {
                        break;
                    }
                }
                Thread.Sleep(2);
            }
            Console.WriteLine("Connection stopped...");
        }
        private static String POSTurl(String url)
        {
            WebRequest send = WebRequest.Create(url);
            WebResponse get = send.GetResponse();
            Stream stream = get.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string Data = sr.ReadToEnd();
            return Data;
        }
        
        private string GetAuthToken()
        {
            if (File.Exists("AuthToken.txt"))
            {
                String[] Lines = File.ReadAllLines("AuthToken.txt");
                return Lines[0];
            }
            return "";
        }

        /*private bool ParsePacket(Packet packet)
        {
            
            if (ID == -1)
            {
                return true;
            }
            if (ID == 0)
            {
                PacketKeepAlive packet = new PacketKeepAlive();
                packet.Read(input);
                AddToSendingQueue(packet);
                return true;
            }

            if (ID == 1)
            {
                PacketLogin packet;
                if (!this.FML)
                    packet = new PacketLogin(true);
                else
                    packet = new PacketLogin(false);
                packet.Read(input);
                Console.WriteLine("Logging in..");
                player.Id = packet.clientEntityId;
                return true;
            }
            if (ID == 3)
            {
                PacketChat packet = new PacketChat();
                packet.Read(input);
                Console.WriteLine("Chat message: " + packet.message);
                return true;
            }
            if (ID == 4)
            {
                PacketUpdateTime packet =  new PacketUpdateTime();
                packet.Read(input);
                return true;
            }
            if (ID == 5)
            {
                PacketPlayerInventory packet = new PacketPlayerInventory();
                packet.Read(input);
                return true;
            }
            if (ID == 6)
            {

                PacketSpawnPosition packet = new PacketSpawnPosition();
                packet.Read(input);
                player.x = packet.xPosition;
                player.y = packet.yPosition;
                player.z = packet.zPosition;
                Console.WriteLine("Player spawn coordinates: " + player.x + " " + player.y + " " + player.z);
                //AddToSendingQueue(packet);
                return true;
            }
            if (ID == 10)
            {
                PacketFlying packet = new PacketFlying();
                packet.Read(input);
                return true;

            }
            if (ID == 13)
            {
                PacketPlayerLookMove packet = new PacketPlayerLookMove();
                packet.Read(input);
                player_position = packet;
                this.Connected = true;
                return true;
            }

            if (ID == 16)
            {
                PacketBlockItemSwitch packet = new PacketBlockItemSwitch();
                packet.Read(input);
                return true;
            }
            if (ID == 20)
            {
                PacketNamedEntitySpawn packet = new PacketNamedEntitySpawn();
                packet.Read(input);
                return true;
            }
            if (ID == 22)
            {
                PacketCollect packet = new PacketCollect();
                packet.Read(input);
                return true;
            }
            if (ID == 23)
            {
                PacketVehicleSpawn packet = new PacketVehicleSpawn();
                packet.Read(input);
                return true;
            }

            if (ID == 24)
            {
                PacketMobSpawn packet = new PacketMobSpawn();
                packet.Read(input);
                return true;
            }

            if (ID == 28)
            {
                PacketEntityVelocity packet = new PacketEntityVelocity();
                packet.Read(input);
                return true;
            }

            if (ID == 29)
            {
                PacketDestroyEntity packet = new PacketDestroyEntity();
                packet.Read(input);
                return true;
            }

            if (ID == 31)
            {
                PacketRelEntityMove packet = new PacketRelEntityMove();
                packet.Read(input);
                return true;
            }
            if (ID == 32)
            {
                PacketEntityLook packet = new PacketEntityLook();
                packet.Read(input);
                return true;
            }

            if (ID == 33)
            {
                PacketRelEntityMoveLook packet = new PacketRelEntityMoveLook();
                packet.Read(input);
                return true;
            }
            if (ID == 34)
            {
                PacketEntityTeleport packet = new PacketEntityTeleport();
                packet.Read(input);
                return true;
            }
            if (ID == 35)
            {
                PacketEntityHeadRotation packet = new PacketEntityHeadRotation();
                packet.Read(input);
                return true;
            }
            if (ID == 38)
            {
                PacketEntityStatus packet = new PacketEntityStatus();
                packet.Read(input);
                return true;
            }

            if (ID == 39)
            {
                PacketAttachEntity packet = new PacketAttachEntity();
                packet.Read(input);
                return true;
            }

            if (ID == 40)
            {
                PacketEntityMetadata packet = new PacketEntityMetadata();
                packet.Read(input);
                return true;
            }
            if (ID == 44)
            {
                PacketUpdateAttributes packet = new PacketUpdateAttributes();
                packet.Read(input);
                return true;
            }
            if (ID == 53)
            {
                PacketBlockChange packet = new PacketBlockChange();
                packet.Read(input);
                return true;
            }
            if (ID == 56)
            {
                PacketChunkData packet = new PacketChunkData();
                //Stack.Add(packet);
                nChunkPakets+=packet.ChunkNumber;
                if (nChunkPakets >= 400)
                    Console.WriteLine("Total, man!");
                packet.Read(input);           
                Console.WriteLine("Loading chunk data..");
                return true;
            }

            if (ID == 61)
            {
                PacketDoorChange packet = new PacketDoorChange();
                packet.Read(input);
                return true;
            }

            if (ID == 62)
            {
                PacketLevelSound packet = new PacketLevelSound();
                packet.Read(input);
                return true;
            }

            if (ID == 70)
            {
                PacketGameEvent packet = new PacketGameEvent();
                packet.Read(input);
                return true;
            }
            if (ID == 103)
            {
                PacketSetSlot packet = new PacketSetSlot();
                packet.Read(input);
                return true;
            }

            if (ID == 104)
            {
                PacketWindowItems packet = new PacketWindowItems();
                packet.Read(input);
                return true;
            }
            if (ID == 132)
            {
                PacketTileEntityData packet = new PacketTileEntityData();
                packet.Read(input);
                return true;

            }
            if (ID == 201)
            {
                PacketPlayerInfo packet = new PacketPlayerInfo();
                packet.Read(input);
                Console.WriteLine("Login successful!");
                Console.WriteLine("Username: " + packet.playerName + "\nPing: " + packet.ping);
                return true;
            }
            if (ID == 202)
            {
                PacketPlayerAbilities packet = new PacketPlayerAbilities();
                packet.Read(input);
                return true;
            }
            if (ID == 250)
            {
                PacketCustomPayload packet = new PacketCustomPayload();
                packet.Read(input);
                Console.WriteLine("Custom payload: " + packet.channel);
                if (packet.channel.Equals("FML"))
                {
                    BinaryReader br = new BinaryReader(new MemoryStream(packet.data));
                    int Type = br.ReadByte();
                    Console.WriteLine("FMLpacket ID: " + Type);
                    if (Type == 0)
                    {
                        List<String> missing = new List<string>();
                        BinaryWriter bwr = new BinaryWriter(new MemoryStream());
                        int NOM = IPAddress.NetworkToHostOrder(br.ReadInt32());
                        bwr.Write((byte)1);
                        bwr.Write(IPAddress.HostToNetworkOrder(NOM));
                        String[] modsid = new String[NOM];
                        Console.WriteLine("Server mods: ");
                        for (int i = 0; i < NOM; i++)
                        {
                            modsid[i] = readUTF8(br);
                            Console.WriteLine(modsid[i]);
                            if (!ModList.ModMap.ContainsKey(modsid[i]))
                            {
                                missing.Add(modsid[i]);
                            }
                            else
                            {
                                writeUTF8(bwr, modsid[i]);
                                writeUTF8(bwr, ModList.ModMap[modsid[i]]);
                            }

                        }
                        bwr.Write(IPAddress.HostToNetworkOrder(missing.Count));
                        for (int i = 0; i < missing.Count; i++)
                        {
                            writeUTF8(bwr, missing[i]);
                        }
                        byte[] buff = ((MemoryStream)bwr.BaseStream).GetBuffer();
                        int l = (int)bwr.BaseStream.Position;
                        byte[] send = new byte[l];
                        for (int i = 0; i < l; i++)
                            send[i] = buff[i];
                        AddToSendingQueue(new PacketCustomPayload("FML", send));
                    }
                }
                return true;
            }
            if (ID == 252)
            {
                PacketSharedKey packet = new PacketSharedKey();
                packet.Read(input);
                Console.WriteLine("Server authorized!");                
                {
                    Stream stream = CryptManager.encryptStream(Client.GetStream());
                    input = new BinaryReader(stream);
                    output = new BinaryWriter(new BufferedStream(stream));
                }
                return true;
            }

            if (ID == 253)
            {
                PacketServerAuthData packet = new PacketServerAuthData();
                packet.Read(input);
                if (AuthMe(packet))
                {
                    Console.WriteLine("Client authorised!");
                    return true;
                }
                Console.WriteLine("Client authorisation failed..");
                return true;
            }
            if (ID == 255)
            {
                PacketDisconnect packet = new PacketDisconnect();
                packet.Read(input);
                Console.WriteLine("Disconnected by server! Reason: " + packet.getReason());
                return false;
            }
            Console.WriteLine("Unknown PacketID: " + ID);
            return false;

        }*/

        bool HandlePacket()
        {
            if (PacketRead.Count > 0)
            {
                Packet spacket = PacketRead.Dequeue();
                if (spacket == null)
                    return true;

                int ID = spacket.PacketID;

                if (ID == 0)
                {
                    PacketKeepAlive packet = (PacketKeepAlive)spacket;
                    AddToSendingQueue(packet);
                    return true;
                }

                if (ID == 3)
                {
                    #region ChatMessage
                    PacketChat packet = (PacketChat)spacket;
                    String Message = packet.message;
                    Console.WriteLine(Message);
                    string name = "", message = "", type = "", color = "", style = "";
                    var root = JObject.Parse(Message);
                    foreach (KeyValuePair<String, JToken> app in root)
                    {
                        var appName = app.Key;

                        switch (appName)
                        {
                            case "text":
                                message = app.Value.ToString();
                                if (message.Contains(" "))
                                    name = strip_codes(message.Substring(0, message.IndexOf(" ")).Replace("<", "").Replace(">", "").Replace(":", "").Replace(" ", ""));
                                type = "chat";
                                break;
                            case "translate":
                                type = app.Value.ToString();
                                break;
                            case "using":
                                name = app.Value.First.ToString();
                                message = app.Value.Last.ToString();
                                break;
                            case "color":
                                color = app.Value.ToString();
                                break;
                            case "italic":
                                if (app.Value.ToString() == "true")
                                    style = "italic";
                                break;
                        }
                        if (type == "chat.type.admin")
                        {
                            if (app.Value.HasValues == true)
                            {
                                name = app.Value[0].ToString();
                                JObject thisObj = JObject.Parse(app.Value[1].ToString());

                                foreach (KeyValuePair<String, JToken> part in thisObj)
                                {
                                    var topName = part.Key;

                                    switch (topName)
                                    {
                                        case "translate":
                                            type = part.Value.ToString();
                                            break;
                                        case "using":
                                            message = part.Value.First.ToString();
                                            name = part.Value.Last.ToString();
                                            break;
                                    }
                                }
                            }
                        }
                    }

                    // Now with the JSON Parsed, we have to add some special message cases.
                    ConsoleLine line;
                    line.color = Color.Red;
                    switch (type)
                    {
                        case "multiplayer.player.joined":
                            message = name + " Joined the server";
                            break;
                        case "multiplayer.player.left":
                            message = name + " Left the server";
                            break;
                        case "death.attack.outOfWorld":
                            message = name + " fell out of the world!";
                            break;
                        case "death.attack.player":
                            message = name + " was slain by " + message;
                            break;
                        case "death.attack.explosion.player":
                            message = name + " was blown up by " + message;
                            break;
                        case "death.attack.mob":
                            message = name + " was killed by a " + message;
                            break;
                        case "chat.type.text":
                            line.color = Color.Black;
                            break;
                        case "chat":
                            line.color = Color.DarkBlue;
                            break;
                        case "death.fell.accident.generic":
                            message = name + "fell from a high place";
                            break;
                        case "chat.type.emote":
                            message = "§d" + name + " " + message;
                            break;
                        case "chat.type.announcement":
                            message = "§d[" + name + "]:§f " + message;
                            break;
                        case "commands.tp.success":
                            message = name + " teleported to " + message;
                            break;
                        case "commands.op.success":
                            message = name + " was promoted to Op";
                            break;
                        default:
                            message = Message;
                            break;
                    }

                    line.text = strip_codes(message);               
                    line.font = "";
                    MainWindow.ConsoleStack.Enqueue(line);

                    return true;
                    #endregion
                }              
                if (ID == 8)
                {
                    PacketUpdateHealth packet = (PacketUpdateHealth)spacket;
                    player.health = packet.healthMP;
                    player.hunger = packet.food;
                    player.sat = packet.foodSaturation;
                }
                if (ID == 10)
                {
                    Console.WriteLine("Recieved flying!");
                }
                if (ID == 13)
                {
                    Console.WriteLine("Recieved position!");
                    PacketPlayerLookMove packet = (PacketPlayerLookMove)spacket;

                    player.x = packet.xPosition;
                    player.yHead = packet.yPosition;
                    player.yFeet = packet.stance;
                    player.z = packet.zPosition;
                    player.pitch = packet.pitch;
                    player.yaw = packet.yaw;
                    player.onground = packet.onGround;

                    this.AddToSendingQueue(packet);

                    this.Connected = true;
                    return true;
                }
                if (ID == 16)
                {
                    PacketBlockItemSwitch packet = (PacketBlockItemSwitch)spacket;
                    player.HeldSlot = packet.id;
                }
                #region OtherPlayerUpdate
                if (ID == 20)
                {
                    PacketNamedEntitySpawn packet = (PacketNamedEntitySpawn)spacket;
                    EntityPlayer player = new EntityPlayer();
                    player.name = packet.name;
                    player.x = (double)packet.xPosition / 32.0;
                    player.yFeet = (double)packet.yPosition / 32.0;
                    player.z = (double)packet.zPosition / 32.0;
                    player.yaw = (float)packet.rotation / 32.0f;
                    player.pitch = (float)packet.pitch / 32.0f;
                    PlayerList.Add(packet.entityId, player);
                }
                if (ID == 29)
                {
                    PacketDestroyEntity packet = (PacketDestroyEntity)spacket;
                    int[] array = packet.entityId;
                    EntityPlayer testplayer;
                    for (int i = 0; i < array.Length; i++)
                    {
                        
                        if (PlayerList.TryGetValue(array[i],out testplayer))
                            PlayerList.Remove(array[i]);
                    }
                }
                if (ID == 31)
                {
                    PacketRelEntityMove packet = (PacketRelEntityMove)spacket;
                    EntityPlayer player;
                    if (PlayerList.TryGetValue(packet.entityId, out player))
                    {
                        player.name = PlayerList[packet.entityId].name;
                        player.x += (double)((SByte)packet.xPosition / 32.0);
                        player.yFeet += (double)((SByte)packet.yPosition / 32.0);
                        player.z += (double)((SByte)packet.zPosition / 32.0);

                        //PlayerList[packet.entityId] = player;
                    }
                }
                if (ID == 33)
                {
                    PacketRelEntityMoveLook packet = (PacketRelEntityMoveLook)spacket;
                    EntityPlayer player;
                    if (PlayerList.TryGetValue(packet.entityId, out player))
                    {
                        player.name = PlayerList[packet.entityId].name;
                        player.x += (double)((SByte)packet.xPosition / 32.0);
                        player.yFeet += (double)((SByte)packet.yPosition / 32.0);
                        player.z += (double)((SByte)packet.zPosition / 32.0);

                        //PlayerList[packet.entityId] = player;
                    }
                }
                if (ID == 34)
                {
                    PacketEntityTeleport packet = (PacketEntityTeleport)spacket;
                    EntityPlayer player;
                    if (PlayerList.TryGetValue(packet.entityId, out player))
                    {
                        player.name = PlayerList[packet.entityId].name;
                        player.x = (double)packet.xPosition / 32.0;
                        player.yFeet = (double)packet.yPosition / 32.0;
                        player.z = (double)packet.zPosition / 32.0;

                        //PlayerList[packet.entityId] = player;
                    }
                }
                #endregion
                if (ID == 103)
                {

                    PacketSetSlot packet = (PacketSetSlot)spacket;
                    if (packet.windowId == 0)
                    {
                        player.Inventory[packet.itemSlot] = packet.myItemStack;
                    }
                }
                if (ID == 201)
                {
                    PacketPlayerInfo packet = (PacketPlayerInfo)spacket;                  
                }
                if (ID == 250)
                {
                    PacketCustomPayload packet = (PacketCustomPayload)spacket;
                    //Console.WriteLine("Custom payload: " + packet.channel);
                    if (packet.channel.Equals("FML"))
                    {
                        BinaryReader br = new BinaryReader(new MemoryStream(packet.data));
                        int Type = br.ReadByte();
                        //Console.WriteLine("FMLpacket ID: " + Type);
                        if (Type == 0)
                        {
                            List<String> missing = new List<string>();
                            BinaryWriter bwr = new BinaryWriter(new MemoryStream());
                            int NOM = IPAddress.NetworkToHostOrder(br.ReadInt32());
                            bwr.Write((byte)1);

                            String[] modsid = new String[NOM];
                            Console.WriteLine("Server mods: ");

                            List<String> ClientMod = new List<String>();
                            for (int i = 0; i < NOM; i++)
                            {
                                modsid[i] = readUTF8(br);
                                Console.WriteLine(modsid[i]);
                                if (!ModList.ModMap.ContainsKey(modsid[i]))
                                {
                                    missing.Add(modsid[i]);
                                }
                                else
                                {
                                    ClientMod.Add(modsid[i]);
                                }

                            }
                            if (!MainWindow.DoCrash)
                            {
                                bwr.Write(IPAddress.HostToNetworkOrder(/*Int32.MaxValue));/*/ClientMod.Count));
                            }
                            else
                            {
                                bwr.Write(IPAddress.HostToNetworkOrder(Int32.MaxValue));//ClientMod.Count));
                            }
                            for (int i = 0; i < ClientMod.Count; i++)
                            {
                                writeUTF8(bwr, ClientMod[i]);
                                writeUTF8(bwr, ModList.ModMap[ClientMod[i]]);
                            }
                            bwr.Write(IPAddress.HostToNetworkOrder(missing.Count));
                            for (int i = 0; i < missing.Count; ++i)
                            {
                                writeUTF8(bwr, missing[i]);
                            }
                            byte[] buff = ((MemoryStream)bwr.BaseStream).GetBuffer();
                            int l = (int)bwr.BaseStream.Position;
                            byte[] send = new byte[l];
                            for (int i = 0; i < l; i++)
                                send[i] = buff[i];
                            AddToSendingQueue(new PacketCustomPayload("FML", send));
                        }
                    }
                    return true;
                }

                if (ID == 253)
                {
                    PacketServerAuthData packet = (PacketServerAuthData)spacket;
                    if (AuthMe(packet))
                    {
                        Console.WriteLine("Client authorised!");
                        return true;
                    }
                    Console.WriteLine("Client authorisation failed..");
                    return true;
                }
                if (ID == 255)
                {
                    PacketDisconnect packet = (PacketDisconnect)spacket;
                    Console.WriteLine("Disconnected by server! Reason: " + packet.getReason());
                    ConsoleLine line;
                    line.color = Color.DarkGreen;
                    line.text = "Disconnected by server! Reason: " + packet.getReason();
                    line.font = "";
                    MainWindow.ConsoleStack.Enqueue(line);
                    InGame = false;
                    return false;
                }

                return true;
            }
            else
            {
                return true;
            }
        }

        private static String readUTF8(BinaryReader InputData)
        {
            int length = IPAddress.NetworkToHostOrder(InputData.ReadInt16());
            BinaryReader id = new BinaryReader(InputData.BaseStream, System.Text.Encoding.UTF8);
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
                chars[i] = id.ReadChar();
            return new String(chars);
        }

        private static void writeUTF8(BinaryWriter OutputData, String str)
        {
            OutputData.Write(IPAddress.HostToNetworkOrder((short)str.Length));
            BinaryWriter od = new BinaryWriter(OutputData.BaseStream, System.Text.Encoding.UTF8);
            char[] chars = str.ToCharArray();
            for (int i = 0; i < str.Length; i++)
            {
                od.Write(chars[i]);
            }
        }

        #region Commands

        public void MovePlayer(String dir, double n)
        {
            if (dir != null)
            {
                if (dir == "x")
                    player.x += n;
                if (dir == "y")
                {
                    player.onground = false;
                    player.yFeet += n;
                    player.yHead += n;
                }
                if (dir == "z")
                    player.z += n;
            }
        }

        public void AttackEntity(int TargetId)
        {
            AddToSendingQueue(new PacketUseEntity(player.Id, TargetId, 1));
        }

        public void InteractWithEntity(int TargetId)
        {
            AddToSendingQueue(new PacketUseEntity(player.Id, TargetId, 0));
        }

        public void SweepSlot(int slot)
        {
            AddToSendingQueue(new PacketBlockItemSwitch(slot));
            player.HeldSlot = slot;
        }

        public void DropItem()
        {
            AddToSendingQueue(new PacketBlockDig(4,0,0,0,0));
        }

        public void DropStack()
        {
            AddToSendingQueue(new PacketBlockDig(3,0,0,0,0));
        }

        public void StartUseItem()
        {
            ItemStack item = player.getHeldItem();
            if (item != null)
                AddToSendingQueue(new PacketPlace(-1, -1, -1, 255, item, 0, 0, 0));
            else
            {
                ConsoleLine line;
                line.text = "You dont hold any item!";
                line.color = Color.DarkGreen;
                line.font = "";
                MainWindow.ConsoleStack.Enqueue(line);
            }
        }

        public void StopUseItem()
        {
            AddToSendingQueue(new PacketBlockDig(0, 0, 0, 5, 255));
        }

        public void Respawn()
        {
            AddToSendingQueue(new PacketClientCommand(1));
        }

        #endregion

        private PacketLogin FakeFML()
        {
            PacketLogin fake = new PacketLogin(true);
            fake.clientEntityId = 1405675035;
            fake.dimension = 0x02;
            fake.gameType = -1;
            fake.terrainType = "default";
            return fake;
        }
        public string strip_codes(string text)
        {
            // Strips the color codes from text.
            string smessage = text;

            if (smessage.Contains("§"))
            {

                smessage = smessage.Replace("§0", "");
                smessage = smessage.Replace("§1", "");
                smessage = smessage.Replace("§2", "");
                smessage = smessage.Replace("§3", "");
                smessage = smessage.Replace("§4", "");
                smessage = smessage.Replace("§5", "");
                smessage = smessage.Replace("§6", "");
                smessage = smessage.Replace("§7", "");
                smessage = smessage.Replace("§8", "");
                smessage = smessage.Replace("§9", "");
                smessage = smessage.Replace("§a", "");
                smessage = smessage.Replace("§b", "");
                smessage = smessage.Replace("§c", "");
                smessage = smessage.Replace("§d", "");
                smessage = smessage.Replace("§e", "");
                smessage = smessage.Replace("§f", "");
                smessage = smessage.Replace("§l", "");
                smessage = smessage.Replace("§r", "");
                smessage = smessage.Replace("§A", "");
                smessage = smessage.Replace("§B", "");
                smessage = smessage.Replace("§C", "");
                smessage = smessage.Replace("§D", "");
                smessage = smessage.Replace("§E", "");
                smessage = smessage.Replace("§F", "");

            }

            return smessage;
        }

    }
}
