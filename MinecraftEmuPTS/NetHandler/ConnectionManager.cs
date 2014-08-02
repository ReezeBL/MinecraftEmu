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
using System.Diagnostics;
using MinecraftEmuPTS.Packets;
using MinecraftEmuPTS.Encription;
using MinecraftEmuPTS;
using MinecraftEmuPTS.GameInfo;
using MinecraftEmuPTS.NetHandler;

namespace MinecraftEmuPTS
{
    class ConnectionManager
    {
        private TcpClient Client;
        //private NetworkStream Channel;
        private BinaryReader input;
        private BinaryWriter output;
        private IPAddress ServerIP;
        private int ServerPort;
        private string AuthToken;

        private EntityPlayer player;

        bool Encription;
        bool FML;

        public ConnectionManager(String host, String port, bool FMLserver)
        {
            this.Encription = false;
            this.FML = FMLserver;
            player = new EntityPlayer();


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
                output = new BinaryWriter(Channel);

                InitializeLoginSession();
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
        private void SendPacket(Packet packet)
        {
            if (!this.Encription)
            {
                packet.Write(output);
                //output.Write(packet.RawData, 0, packet.GetPacketSize());
            }
            else
            {

            }
        }
        private int GetPacket()
        {
            if (!this.Encription)
            {
                int ID = input.ReadByte();
                return ID;
            }
            else
            {
                return 0;
            }
        }

        private bool AuthMe(PacketServerAuthData packet)
        {
            if ("-".Equals(packet.getServerID()))
                return true;

            Console.WriteLine("ServerID: " + packet.getServerID());
            CryptManager.SendCryptedKey(packet.GetPublicKey());
            SendPacket(CryptManager.GetSharedKey(packet.GetVerifyToken()));
            ParsePacket(GetPacket());
            Encription = true;

            String ServerHash = System.Text.Encoding.UTF8.GetString(CryptManager.GetServerIdHash(packet.getServerID()));
            AuthToken = GetAuthToken();
            String AuthResponse = POSTurl(AuthToken + ServerHash);
            if (AuthResponse.Equals("OK"))
                return true;
            return false;
        }

        private void InitializeLoginSession()
        {
            try
            {
                SendPacket(new PacketHandshake(78, "Siamant", ServerIP.ToString(), ServerPort));
                Console.WriteLine("Sending handshake...");/*
                if (!AuthMe(new PacketServerAuthData(GetPacket())))
                {
                    Console.WriteLine("Client authorization failed!");
                    return;
                }
                Console.WriteLine("Client authorized!");

                SendPacket(FakeFML());
                SendPacket(new PacketClientCommand(0));*/
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
            while (ParsePacket(GetPacket())) ;
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

        private bool ParsePacket(Packet packet)
        {
            if (packet.GetPacketID() == 0)
            {
                SendPacket(packet);
                return true;
            }

            if (packet.GetPacketID() == 1)
            {
                PacketLogin parsed_packet = new PacketLogin(packet);
                Console.WriteLine("Logging in..");
                return true;
            }

            if (packet.GetPacketID() == 56)
            {
                PacketChunkData parsed_packet = new PacketChunkData(packet);
                Console.WriteLine("Loading chunk data..");
                parsed_packet.LoadChunkData(input);
                return true;
            }

            if (packet.GetPacketID() == 201)
            {
                PacketPlayerInfo parsed_packet = new PacketPlayerInfo(packet);
                Console.WriteLine("Login successful!");
                Console.WriteLine("Username: " + parsed_packet.playerName + "\nPing: " + parsed_packet.ping);
                return true;
            }
            if (packet.GetPacketID() == 252)
            {
                PacketSharedKey parsed_packet = new PacketSharedKey(packet);
                Console.WriteLine("Server authorized!");
                return true;
            }

            if (packet.GetPacketID() == 253)
            {
                PacketServerAuthData parsed_packet = new PacketServerAuthData(packet);
                if (AuthMe(parsed_packet))
                {
                    Console.WriteLine("Client authorised!");
                    if (this.FML)
                    {
                        SendPacket(FakeFML());
                    }
                    SendPacket(new PacketClientCommand(0));

                    return true;
                }
                Console.WriteLine("Client authorisation failed..");
                return true;
            }
            if (packet.GetPacketID() == 255)
            {
                PacketDisconnect parsed_packet = new PacketDisconnect(packet);
                Console.WriteLine("Disconnected by server! Reason: " + parsed_packet.getReason());
                return false;
            }
            Console.WriteLine("Unknown PacketID: " + packet.GetPacketID());
            return true;

        }

        private bool ParsePacket(int ID)
        {
            if (ID == 0)
            {
                PacketKeepAlive packet = new PacketKeepAlive();
                packet.Read(input);
                SendPacket(packet);
                return true;
            }

            if (ID == 1)
            {
                PacketLogin packet;
                if(!this.FML)
                    packet = new PacketLogin(true);
                else
                    packet = new PacketLogin(false);
                packet.Read(input);
                Console.WriteLine("Logging in..");
                player.Id = packet.clientEntityId;
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
                //SendPacket(packet);
                return true;
            }
            if (ID == 56)
            {
                PacketChunkData packet = new PacketChunkData();
                packet.Read(input);
                Console.WriteLine("Loading chunk data..");
                packet.LoadChunkData(input);
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
                return true;
            }
            if (ID == 252)
            {
                PacketSharedKey packet = new PacketSharedKey();
                packet.Read(input);
                Console.WriteLine("Server authorized!");
                return true;
            }

            if (ID == 253)
            {
                PacketServerAuthData packet = new PacketServerAuthData();
                packet.Read(input);
                if (AuthMe(packet))
                {
                    Console.WriteLine("Client authorised!");
                    if (this.FML)
                    {
                        SendPacket(FakeFML());
                    }
                    SendPacket(new PacketClientCommand(0));

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
            SendPacket(new PacketDisconnect("sex is good"));
            return false;

        }

        private PacketLogin FakeFML()
        {
            PacketLogin fake = new PacketLogin();
            fake.clientEntityId = 1405675035;
            fake.dimension = 0x02;
            fake.gameType = -1;
            fake.terrainType = "default";
            return fake;
        }
    }
}
