using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace MinecraftEmuPTS.Packets
{
    class PacketAttachEntity : Packet
    {
        public int attachState;
        public int ridingEntityId;
        public int vehicleEntityId;

        public PacketAttachEntity() { }

        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.ridingEntityId = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.vehicleEntityId = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.attachState = DataInput.ReadByte();
        }
    }
}
