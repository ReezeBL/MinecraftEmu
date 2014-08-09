using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
namespace MinecraftEmuPTS.Packets
{
    class PacketMultiBlockChange : Packet
    {
        /** Chunk X position. */
        public int xPosition;

        /** Chunk Z position. */
        public int zPosition;

        /** The metadata for each block changed. */
        public byte[] metadataArray;

        /** The size of the arrays. */
        public int size;

        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.xPosition = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.zPosition = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.size = IPAddress.NetworkToHostOrder(DataInput.ReadInt16()) &65535;
            int i = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());

            if (i > 0)
            {
                this.metadataArray = new byte[i];
                DataInput.Read(this.metadataArray,0,i);
            }
        }
    }
}
