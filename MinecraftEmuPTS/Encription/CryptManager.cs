using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Security.Cryptography;
using System.Net.Sockets;
using System.Net;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.X509.Extension;
using Org.BouncyCastle.X509.Store;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Encoders;


namespace MinecraftEmuPTS.Encription
{
    class CryptManager
    {
        
        static public void StopServer()
        {
            TcpClient Client = new TcpClient();
            Client.Connect(IPAddress.Parse("127.0.0.1"), 3128);
            NetworkStream tcpStream = Client.GetStream();
            byte command = 0;
            
            tcpStream.WriteByte(command);

            Client.Close();
        }
        static public PublicKey DecodePublicKey(byte[] data){
            /*TcpClient Client = new TcpClient();
            Client.Connect(IPAddress.Parse("127.0.0.1"), 3128);
            NetworkStream tcpStream = Client.GetStream();
            byte command = 1;

            

            tcpStream.WriteByte(command);
            tcpStream.Write(data, 0, data.Length);
            tcpStream.Close();
            Client.Close();*/
            PublicKey key;
            key.g
            var asymmetricKeyParameter = PublicKeyFactory.CreateKey(data);
            var rsaKeyParameters = (RsaKeyParameters)asymmetricKeyParameter;
            var cipher = CipherUtilities.GetCipher("RSA");
            cipher.Init(true, rsaKeyParameters);
            //cipher.g

        }
        static public PublicKey GetSharedKey(byte[] SendToken)
        {
            /*TcpClient Client = new TcpClient();
            Client.Connect(IPAddress.Parse("127.0.0.1"), 3128);
            NetworkStream tcpStream = Client.GetStream();
            byte command = 2;

           X509

            tcpStream.WriteByte(command);
            tcpStream.Write(SendToken, 0, SendToken.Length);
            int L;
            L = tcpStream.ReadByte();
            byte[] Secret = new byte[L];            
            tcpStream.Read(Secret, 0, Secret.Length);
            L = tcpStream.ReadByte();
            byte[] Token = new byte[L];
            tcpStream.Read(Token, 0, Token.Length);

            Client.Close();

            return new Packets.PacketSharedKey(Secret, Token);*/
            return null;
        }

        static public byte[] GetServerIdHash(String ServerId)
        {
            TcpClient Client = new TcpClient();
            Client.Connect(IPAddress.Parse("127.0.0.1"), 3128);
            NetworkStream tcpStream = Client.GetStream();
            byte command = 3;

            tcpStream.WriteByte(command);
            byte[] data = System.Text.Encoding.ASCII.GetBytes(ServerId);
            byte L = (byte)data.Length;
            tcpStream.WriteByte(L);
            tcpStream.Write(data, 0, L);
            L = (byte)tcpStream.ReadByte();
            data = new byte[L];
            tcpStream.Read(data, 0, L);

            Client.Close();

            return data;
        }

        public static byte[] EncryptPacket(Packet packet)
        {
            TcpClient Client = new TcpClient();
            Client.Connect(IPAddress.Parse("127.0.0.1"), 3128);
            NetworkStream tcpStream = Client.GetStream();
            byte command = 4;

            tcpStream.WriteByte(command);
            tcpStream.WriteByte((byte)packet.GetPacketSize());
            tcpStream.Write(packet.RawData, 0, packet.GetPacketSize());

            byte[] data = new byte[packet.GetPacketSize()];
            tcpStream.Read(data, 0, data.Length);

            return data;
        }

        public static Packet DecryptPacket(byte[] data, int length)
        {
            TcpClient Client = new TcpClient();
            Client.Connect(IPAddress.Parse("127.0.0.1"), 3128);
            NetworkStream tcpStream = Client.GetStream();
            byte command = 5;

            tcpStream.WriteByte(command);
            tcpStream.WriteByte((byte)length);
            tcpStream.Write(data, 0, length);

            return null;
        }
       
    }
}
