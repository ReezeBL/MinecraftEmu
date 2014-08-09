using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
namespace MinecraftEmuPTS.Packets
{
    class PacketEntityStatus : Packet
    {
        public int entityId;

        /** 2 for hurt, 3 for dead */
        public byte entityStatus;
        public PacketEntityStatus() { }

        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.entityId = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.entityStatus = DataInput.ReadByte();
        }
    }
}
