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
        public byte[] ChunkData;
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
        }

        public void LoadChunkData(System.IO.BinaryReader DataInput)
        {
            ChunkData = new byte[DataLength];
            DataInput.Read(ChunkData, 0, DataLength);
            for (int i = 0; i < ChunkNumber; ++i)
            {
                DataInput.ReadInt32();
                DataInput.ReadInt32();
                DataInput.ReadInt16();
                DataInput.ReadInt16();
            }
        }
    }
}
