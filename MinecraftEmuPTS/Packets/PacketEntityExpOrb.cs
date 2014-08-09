using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace MinecraftEmuPTS.NetHandler
{
    class PacketEntityExpOrb : Packet
    {
        /** Entity ID for the XP Orb */
        public int entityId;
        public int posX;
        public int posY;
        public int posZ;

        /** The Orbs Experience points value. */
        public int xpValue;

        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.entityId = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.posX = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.posY = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.posZ = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.xpValue = IPAddress.NetworkToHostOrder(DataInput.ReadInt16());
        }
    }
}
