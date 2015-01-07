using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
namespace MinecraftEmuPTS.Packets
{
    class PacketDestroyEntity : Packet
    {
        public int[] entityId;
        public PacketDestroyEntity() { }
        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.entityId = new int[DataInput.ReadByte()];

            for (int i = 0; i < this.entityId.Length; ++i)
            {
                this.entityId[i] = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            }
        }

        public override void processPacket(NetHandler.PacketHandler handle)
        {
            handle.HandlePacketDestroyEntity(this);
        }
    }
}
