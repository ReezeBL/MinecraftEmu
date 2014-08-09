using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
namespace MinecraftEmuPTS.Packets
{
    class PacketEntityPainting : Packet
    {
        public int entityId;
        public int xPosition;
        public int yPosition;
        public int zPosition;
        public int direction;
        public String title;

        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.entityId = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.title = readString(DataInput, 14);
            this.xPosition = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.yPosition = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.zPosition = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.direction = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
        }
    }
}
