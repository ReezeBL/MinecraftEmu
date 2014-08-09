using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace MinecraftEmuPTS.NetHandler
{
    class PacketEntityHeadRotation : Packet
    {
        public int entityId;
        public byte headRotationYaw;

        public PacketEntityHeadRotation() { }

        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.entityId = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.headRotationYaw = DataInput.ReadByte();
        }
    }
}
