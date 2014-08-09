using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
namespace MinecraftEmuPTS.Packets
{
    class PacketBlockChange : Packet
    {
        /** Block X position. */
        public int xPosition;

        /** Block Y position. */
        public int yPosition;

        /** Block Z position. */
        public int zPosition;

        /** The new block type for the block. */
        public int type;

        /** Metadata of the block. */
        public int metadata;

        public PacketBlockChange() { }
        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.xPosition = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.yPosition = DataInput.ReadByte();
            this.zPosition = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.type = IPAddress.NetworkToHostOrder(DataInput.ReadInt16());
            this.metadata = DataInput.ReadByte();
        }
    }
}
