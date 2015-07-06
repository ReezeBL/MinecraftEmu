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
        virtual public void processPacket(PacketHandler handle) {
            //Console.WriteLine("Unhandled packet " + PacketID);
        }

        public int PacketID;           

        public void Write(BinaryWriter DataWrite)
        {
            DataWrite.Write((byte)GetPacketID());
            writeData(DataWrite);            
        }

        public void Read(BinaryReader DataRead)
        {
            
            readData(DataRead);         
        }
        #region DataReadingWriting
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
      

        protected static void writeByteArray(BinaryWriter DataOutput, byte[] ArrayOfByte)
        {
            DataOutput.Write(IPAddress.HostToNetworkOrder((short)ArrayOfByte.Length));
            DataOutput.Write(ArrayOfByte);
        }        
        public Packet()
        {           
            PacketID = -1;          
        }
        #endregion
        public static void Init()
        {
            PacketMap.Add(0, typeof(PacketKeepAlive));
            PacketMap.Add(1, typeof(PacketLogin));         
            PacketMap.Add(3, typeof(PacketChat));
            PacketMap.Add(4, typeof(PacketUpdateTime));
            PacketMap.Add(5, typeof(PacketPlayerInventory));
            PacketMap.Add(6, typeof(PacketSpawnPosition));
            PacketMap.Add(8, typeof(PacketUpdateHealth));
            PacketMap.Add(9, typeof(PacketRespawn));
            PacketMap.Add(10, typeof(PacketFlying));
            PacketMap.Add(13, typeof(PacketPlayerLookMove));          
            PacketMap.Add(16, typeof(PacketBlockItemSwitch));
            PacketMap.Add(18, typeof(PacketAnimation));
            PacketMap.Add(20, typeof(PacketNamedEntitySpawn));
            PacketMap.Add(22, typeof(PacketCollect));
            PacketMap.Add(23, typeof(PacketVehicleSpawn));
            PacketMap.Add(24, typeof(PacketMobSpawn));
            PacketMap.Add(25, typeof(PacketEntityExpOrb));
            PacketMap.Add(26, typeof(PacketEntityExpOrb));
            PacketMap.Add(28, typeof(PacketEntityVelocity));
            PacketMap.Add(29, typeof(PacketDestroyEntity));
            PacketMap.Add(31, typeof(PacketRelEntityMove));
            PacketMap.Add(32, typeof(PacketEntityLook));
            PacketMap.Add(33, typeof(PacketRelEntityMoveLook));
            PacketMap.Add(34, typeof(PacketEntityTeleport));
            PacketMap.Add(35, typeof(PacketEntityHeadRotation));
            PacketMap.Add(38, typeof(PacketEntityStatus));
            PacketMap.Add(39, typeof(PacketAttachEntity));
            PacketMap.Add(40, typeof(PacketEntityMetadata));
            PacketMap.Add(41, typeof(PacketEntityEffect));
            PacketMap.Add(42, typeof(PacketRemoveEntityEffect));
            PacketMap.Add(43, typeof(PacketExperience));
            PacketMap.Add(44, typeof(PacketUpdateAttributes));
            PacketMap.Add(51, typeof(PacketMapChunk));
            PacketMap.Add(52, typeof(PacketMultiBlockChange));
            PacketMap.Add(53, typeof(PacketBlockChange));
            PacketMap.Add(54, typeof(PacketPlayNoteBlock));
            PacketMap.Add(55, typeof(PacketBlockDestroy));
            PacketMap.Add(56, typeof(PacketChunkData));
            PacketMap.Add(60, typeof(PacketExplosion));
            PacketMap.Add(61, typeof(PacketDoorChange));
            PacketMap.Add(62, typeof(PacketLevelSound));
            PacketMap.Add(70, typeof(PacketGameEvent));
            PacketMap.Add(71, typeof(PacketWeather));
            PacketMap.Add(101, typeof(PacketCloseWindow));
            PacketMap.Add(103, typeof(PacketSetSlot));
            PacketMap.Add(104, typeof(PacketWindowItems));
            PacketMap.Add(130, typeof(PacketUpdateSign));
            PacketMap.Add(131, typeof(PacketMapData));
            PacketMap.Add(132, typeof(PacketTileEntityData));
            PacketMap.Add(200, typeof(PacketStatistic));
            PacketMap.Add(201, typeof(PacketPlayerInfo));
            PacketMap.Add(202, typeof(PacketPlayerAbilities));
            PacketMap.Add(209, typeof(PacketSetPlayerTeam));
            PacketMap.Add(250, typeof(PacketCustomPayload));
            PacketMap.Add(252, typeof(PacketSharedKey));
            PacketMap.Add(253, typeof(PacketServerAuthData));
            PacketMap.Add(255, typeof(PacketDisconnect));          
        }

        public int GetPacketID()
        {
            return PacketID;
        }      
    }
}