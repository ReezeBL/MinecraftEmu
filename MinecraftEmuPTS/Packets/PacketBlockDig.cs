using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace MinecraftEmuPTS.Packets
{
    class PacketBlockDig : Packet
    {
        /** Block X position. */
        public int xPosition;

        /** Block Y position. */
        public int yPosition;

        /** Block Z position. */
        public int zPosition;

        /** Punched face of the block. */
        public int face;

        /** Status of the digging (started, ongoing, broken). */
        public int status;

        public PacketBlockDig(int par1, int par2, int par3, int par4, int par5)
        {
            this.PacketID = 14;
            this.status = par1;
            this.xPosition = par2;
            this.yPosition = par3;
            this.zPosition = par4;
            this.face = par5;
        }

        protected override void writeData(System.IO.BinaryWriter DataOutput)
        {

            DataOutput.Write((byte)this.status);
            DataOutput.Write(IPAddress.HostToNetworkOrder(this.xPosition));
            DataOutput.Write((byte)this.yPosition);
            DataOutput.Write(IPAddress.HostToNetworkOrder(this.zPosition));
            DataOutput.Write((byte)this.face);

        }
    }
}
