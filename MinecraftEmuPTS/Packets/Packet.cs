using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using MinecraftEmuPTS.NetHandler;
using MinecraftEmuPTS.Packets;


namespace MinecraftEmuPTS
{
    class Packet
    {
        virtual protected void readData(BinaryReader DataInput) { }
        virtual protected void writeData(BinaryWriter DataOutput) { }
        //virtual public void processPacket(INetHandler handle) { }

        public byte[] RawData;
        protected int PacketID;
        protected int PacketSize;
        private MemoryStream Data;

        public void Write()
        {
            BinaryWriter bw = new BinaryWriter(Data);
            bw.Write((byte)GetPacketID());
            writeData(bw);
            PacketSize = (int)Data.Position;
        }

        public void Read()
        {

            Data.Close();
            Data = new MemoryStream(RawData);

            BinaryReader br = new BinaryReader(Data);
            PacketID = br.ReadByte();
            readData(br);
            PacketSize = (int)Data.Position;
        }

        public void Write(BinaryWriter DataWrite)
        {
            DataWrite.Write((byte)GetPacketID());
            writeData(DataWrite);
        }

        public void Read(BinaryReader DataRead)
        {
            //this.PacketID = DataRead.ReadByte();
            readData(DataRead);
        }

        protected static String readString(BinaryReader DataInput, int MLength)
        {
            short length = IPAddress.NetworkToHostOrder(DataInput.ReadInt16());

            if (length > MLength)
            {
                throw new IOException("Received string length longer than maximum allowed (" + length + " > " + MLength + ")");
            }
            else if (length < 0)
            {
                throw new IOException("Received string length is less than zero! Weird string!");
            }
            else
            {
                StringBuilder stringbuilder = new StringBuilder();
                for (int j = 0; j < length; ++j)
                {
                    stringbuilder.Append((char)IPAddress.NetworkToHostOrder(DataInput.ReadInt16()));
                }
                return stringbuilder.ToString();
            }
        }

        protected static void writeString(String text, BinaryWriter DataOutput)
        {
            if (text.Length > 32767)
            {
                throw new IOException("String too big");
            }
            else
            {
                DataOutput.Write(IPAddress.HostToNetworkOrder((short)text.Length));
                for (int i = 0; i < text.Length; i++)
                {
                    DataOutput.Write(IPAddress.HostToNetworkOrder((short)text[i]));
                }
            }
        }

        protected static byte[] readBytesFromStream(BinaryReader DataInput)
        {
            short length = IPAddress.NetworkToHostOrder(DataInput.ReadInt16());

            if (length < 0)
            {
                throw new IOException("Key was smaller than nothing!  Weird key!");
            }
            else
            {
                byte[] abyte = new byte[length];
                DataInput.Read(abyte, 0, length);
                return abyte;
            }
        }

        private static Packet GetNewPacket(int ID)
        {
            switch (ID){
                case 0:
                    return new PacketKeepAlive();
                case 1:
                    return new PacketLogin();
                default:
                    return new Packet();
            }
        }

        protected static void writeByteArray(BinaryWriter DataOutput, byte[] ArrayOfByte)
        {
            DataOutput.Write(IPAddress.HostToNetworkOrder((short)ArrayOfByte.Length));
            DataOutput.Write(ArrayOfByte);
        }
        public void Init(Packet packet)
        {
            this.RawData = packet.RawData;
            Read();
        }
        public Packet()
        {           
            PacketID = -1;
            PacketSize = 0;
            RawData = new byte[4096];
            Data = new MemoryStream(RawData);
        }

        public int GetPacketID()
        {
            return PacketID;
        }

        public int GetPacketSize()
        {
            return PacketSize;
        }
    }
}
