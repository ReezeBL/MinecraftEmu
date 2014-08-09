using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
namespace MinecraftEmuPTS.Packets
{
    class PacketUpdateSign : Packet
    {
        public int xPosition;
        public int yPosition;
        public int zPosition;
        public String[] signLines;

        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.xPosition = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.yPosition = IPAddress.NetworkToHostOrder(DataInput.ReadInt16());
            this.zPosition = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.signLines = new String[4];

            for (int i = 0; i < 4; ++i)
            {
                this.signLines[i] = readString(DataInput, 15);
            }
        }
    }
}
