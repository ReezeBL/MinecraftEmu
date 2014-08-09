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
using MinecraftEmuPTS.GameInfo;


namespace MinecraftEmuPTS
{
    class Packet
    {
        public static Dictionary<int, Type> PacketMap = new Dictionary<int,Type>();
        virtual protected void readData(BinaryReader DataInput) { }
        virtual protected void writeData(BinaryWriter DataOutput) { }
        //virtual public void processPacket(INetHandler handle) { }

        public byte[] RawData;
        public int PacketID;
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

        public static String readString(BinaryReader DataInput, int MLength)
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
                for (int j = 0; j < length; j++)
                {
                    stringbuilder.Append((char)IPAddress.NetworkToHostOrder(DataInput.ReadInt16()));
                }
                return stringbuilder.ToString();
            }
        }

        public static void writeString(String text, BinaryWriter DataOutput)
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
            else if (length == 0)
            {
                return null;
            }
            else
            {
                byte[] abyte = new byte[length];
                DataInput.Read(abyte, 0, length);
                return abyte;
            }
        }

        protected ItemStack readItemStack(BinaryReader DataInput)
        {
            ItemStack item = new ItemStack();
            short meta = IPAddress.NetworkToHostOrder(DataInput.ReadInt16());
            if (meta >= 0)
            {
                item.StackSize = DataInput.ReadByte();
                item.ItemDamage = DataInput.ReadInt16();
                item.NBTdata = readNBTTag(DataInput);
            }
            else
            {
                return null;
            }
            item.ItemId = meta;
            return item;
            
        }

        protected void writeItemStack(ItemStack item, BinaryWriter DataOutput)
        {
            if (item == null)
            {
                DataOutput.Write(IPAddress.HostToNetworkOrder((short)-1));
            }
            else
            {
                DataOutput.Write(IPAddress.HostToNetworkOrder((short)item.ItemId));
                DataOutput.Write((byte)item.StackSize);
                DataOutput.Write(IPAddress.HostToNetworkOrder((short)item.ItemDamage));
                writeNBTTag(item.NBTdata, DataOutput);
            }
        }

        protected byte[] readNBTTag(BinaryReader DataInput)
        {
            short len = IPAddress.NetworkToHostOrder(DataInput.ReadInt16());
            if (len < 0) return null;         
            byte[] data = DataInput.ReadBytes(len);
            return data;
        }

        protected void writeNBTTag(byte[] NBTTag, BinaryWriter DataOutput)
        {
            if (NBTTag == null)
            {
                DataOutput.Write(IPAddress.HostToNetworkOrder((short)-1));
            }
            else
            {
                DataOutput.Write(IPAddress.HostToNetworkOrder((short)NBTTag.Length));
                DataOutput.Write(NBTTag);
            }
        }

        protected void readWatchableObjects(BinaryReader DataInput)
        {
            for (byte b0 = DataInput.ReadByte(); b0 != 127; b0 = DataInput.ReadByte())
            {
                int i = (b0 & 224) >> 5;
                int j = b0 & 31;
                switch (i)
                {
                    case 0:
                        DataInput.ReadByte();
                        break;
                    case 1:
                        DataInput.ReadInt16();
                        break;
                    case 2:
                        DataInput.ReadInt32();
                        break;
                    case 3:
                        DataInput.ReadInt32();
                        break;
                    case 4:
                        readString(DataInput, 64);
                        break;
                    case 5:
                        readItemStack(DataInput);
                        break;
                    case 6:
                        DataInput.ReadInt32();
                        DataInput.ReadInt32();
                        DataInput.ReadInt32();
                        break;
                }
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

        public void Init()
        {
            PacketMap.Add(0, typeof(PacketKeepAlive));
            PacketMap.Add(1, typeof(PacketLogin));         
            PacketMap.Add(3, typeof(PacketChat));
            PacketMap.Add(4, typeof(PacketUpdateTime));
            PacketMap.Add(5, typeof(PacketPlayerInventory));
            PacketMap.Add(6, typeof(PacketSpawnPosition));
            PacketMap.Add(10, typeof(PacketFlying));
            PacketMap.Add(11, typeof(PacketLogin));          
            PacketMap.Add(14, typeof(PacketPlayerLookMove));
            PacketMap.Add(1, typeof(PacketLogin));
            PacketMap.Add(1, typeof(PacketLogin));
            PacketMap.Add(1, typeof(PacketLogin));
            PacketMap.Add(1, typeof(PacketLogin));
            PacketMap.Add(1, typeof(PacketLogin));
            PacketMap.Add(1, typeof(PacketLogin));
            PacketMap.Add(1, typeof(PacketLogin));
            PacketMap.Add(1, typeof(PacketLogin));
            PacketMap.Add(1, typeof(PacketLogin));
            PacketMap.Add(1, typeof(PacketLogin));
            PacketMap.Add(1, typeof(PacketLogin));
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
