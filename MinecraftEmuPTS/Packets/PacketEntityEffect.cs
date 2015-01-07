using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace MinecraftEmuPTS.NetHandler
{
    class PacketEntityEffect : Packet
    {
        public int entityId;
        public byte effectId;
        public byte effectAmplifier;
        public short duration;

        public PacketEntityEffect() { }

        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.entityId = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.effectId = DataInput.ReadByte();
            this.effectAmplifier = DataInput.ReadByte();
            this.duration = IPAddress.NetworkToHostOrder(DataInput.ReadInt16());
        }
    }
}
