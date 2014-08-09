using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace MinecraftEmuPTS.Packets
{
    class PacketChunkData : Packet
    {
        public short ChunkNumber;
        public int DataLength;
        public static byte[] ChunkData = new byte[0];


        public PacketChunkData(Packet packet)
        {
            this.PacketID = 51;
            this.RawData = packet.RawData;
            this.Read();
        }

        public PacketChunkData()
        {
            this.PacketID = 56;
        }

        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.PacketID = 56;
            this.ChunkNumber = IPAddress.NetworkToHostOrder(DataInput.ReadInt16());
            this.DataLength = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            bool flag_1 = DataInput.ReadBoolean();

            if (DataLength != 0)
            {
                if (ChunkData.Length < this.DataLength)
                    ChunkData = new byte[this.DataLength];
                DataInput.Read(ChunkData, 0, this.DataLength);
            }
            for (int i = 0; i < this.ChunkNumber; i++)
            {
                DataInput.ReadInt32();
                DataInput.ReadInt32();
                DataInput.ReadInt16();
                DataInput.ReadInt16();
            }
        }

        public void LoadChunkData(System.IO.BinaryReader DataInput)
        {
            
        }
    }
}
