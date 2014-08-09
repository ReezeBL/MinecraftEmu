using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace MinecraftEmuPTS.Packets
{
    class PacketPlayerLook : PacketFlying
    {
        public PacketPlayerLook()
        {
            this.PacketID = 12;
            this.rotating = true;
        }
        public PacketPlayerLook(float par1, float par2, bool par3)
        {
            this.PacketID = 12;
            this.yaw = par1;
            this.pitch = par2;
            this.onGround = par3;
            this.rotating = true;
        }

        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.yaw = (float)IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.pitch = (float)IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            base.readData(DataInput);
        }

        protected override void writeData(System.IO.BinaryWriter DataOutput)
        {
            DataOutput.Write(IPAddress.HostToNetworkOrder((int)this.yaw));
            DataOutput.Write(IPAddress.HostToNetworkOrder((int)this.pitch));
            base.writeData(DataOutput);
        }
    }
}
