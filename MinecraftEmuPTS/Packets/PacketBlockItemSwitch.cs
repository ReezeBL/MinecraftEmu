using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace MinecraftEmuPTS.Packets
{
    class PacketBlockItemSwitch : Packet
    {
        public int id;

        public PacketBlockItemSwitch()
        {
            this.PacketID = 16;
        }

        public PacketBlockItemSwitch(int par1)
        {
            this.PacketID = 16;
            this.id = par1;
        }

        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.id = IPAddress.NetworkToHostOrder(DataInput.ReadInt16());
        }

        protected override void writeData(System.IO.BinaryWriter DataOutput)
        {
            DataOutput.Write((IPAddress.HostToNetworkOrder((Int16)this.id)));
        }

        public override void processPacket(NetHandler.PacketHandler handle)
        {
            handle.HandlePacketBlockItemSwitch(this);
        }
    }
}
