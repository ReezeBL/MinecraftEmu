using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace MinecraftEmuPTS.Packets
{
    class PacketPlayerPosition : PacketFlying
    {
        public PacketPlayerPosition()
        {
            this.PacketID = 11;
            this.moving = true;
        }

        public PacketPlayerPosition(double par1, double par3, double par5, double par7, bool par9)
        {
            this.PacketID = 11;
            this.xPosition = par1;
            this.yPosition = par3;
            this.stance = par5;
            this.zPosition = par7;
            this.onGround = par9;
            this.moving = true;
        }

        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.xPosition = BitConverter.Int64BitsToDouble(IPAddress.NetworkToHostOrder(DataInput.ReadInt64()));
            this.yPosition = BitConverter.Int64BitsToDouble(IPAddress.NetworkToHostOrder(DataInput.ReadInt64()));
            this.stance = BitConverter.Int64BitsToDouble(IPAddress.NetworkToHostOrder(DataInput.ReadInt64()));
            this.zPosition = BitConverter.Int64BitsToDouble(IPAddress.NetworkToHostOrder(DataInput.ReadInt64()));
            base.readData(DataInput);
        }

        protected override void writeData(System.IO.BinaryWriter DataOutput)
        {
            DataOutput.Write(IPAddress.HostToNetworkOrder(BitConverter.DoubleToInt64Bits(this.xPosition)));
            DataOutput.Write(IPAddress.HostToNetworkOrder(BitConverter.DoubleToInt64Bits(this.yPosition)));
            DataOutput.Write(IPAddress.HostToNetworkOrder(BitConverter.DoubleToInt64Bits(this.stance)));
            DataOutput.Write(IPAddress.HostToNetworkOrder(BitConverter.DoubleToInt64Bits(this.zPosition)));
            base.writeData(DataOutput);
        }
    }
}
