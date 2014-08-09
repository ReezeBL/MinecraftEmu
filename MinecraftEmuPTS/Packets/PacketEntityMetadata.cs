using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace MinecraftEmuPTS.Packets
{
    class PacketEntityMetadata : Packet
    {
        public int entityId;
        public PacketEntityMetadata()
        {

        }
        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.entityId = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            readWatchableObjects(DataInput);
        }
    }
}
