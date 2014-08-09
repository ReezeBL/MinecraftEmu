using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
namespace MinecraftEmuPTS.Packets
{
    class PacketCollect : Packet
    {
        /** The entity on the ground that was picked up. */
        public int collectedEntityId;

        /** The entity that picked up the one from the ground. */
        public int collectorEntityId;

        public PacketCollect() { }

        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.collectedEntityId = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.collectorEntityId = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
        }
    }
}
