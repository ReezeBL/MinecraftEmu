using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace MinecraftEmuPTS.Packets
{
    class PacketEntityVelocity : Packet
    {
        public int entityId;
        public int motionX;
        public int motionY;
        public int motionZ;

        public PacketEntityVelocity() { }

        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.entityId = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.motionX = IPAddress.NetworkToHostOrder(DataInput.ReadInt16());
            this.motionY = IPAddress.NetworkToHostOrder(DataInput.ReadInt16());
            this.motionZ = IPAddress.NetworkToHostOrder(DataInput.ReadInt16());
        }
    }
}
