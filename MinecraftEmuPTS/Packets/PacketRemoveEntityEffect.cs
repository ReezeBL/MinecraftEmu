using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace MinecraftEmuPTS.NetHandler
{
    class PacketRemoveEntityEffect : Packet
    {
        /** The ID of the entity which an effect is being removed from. */
        public int entityId;

        /** The ID of the effect which is being removed from an entity. */
        public byte effectId;

        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.entityId = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.effectId = DataInput.ReadByte();
        }
    }
}
