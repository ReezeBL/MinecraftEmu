using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinecraftEmuPTS.Packets
{
    class PacketKeepAlive : Packet
    {
        public int randomId;

        public PacketKeepAlive() {
            this.PacketID = 0;
        }

        public PacketKeepAlive(int par1)
        {
            this.PacketID = 0;
            this.randomId = par1;
        }

        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.randomId = DataInput.ReadInt32();
        }
        protected override void writeData(System.IO.BinaryWriter DataOutput)
        {
            DataOutput.Write(this.randomId);
        }

        public override void processPacket(NetHandler.PacketHandler handle)
        {
            handle.HandlePacketKeepAlive(this);
        }

    }
}
