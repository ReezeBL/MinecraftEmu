using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace MinecraftEmuPTS.Packets
{
    class PacketPlayerLookMove : PacketFlying
    {
        public PacketPlayerLookMove()
        {
            this.PacketID = 13;
            this.rotating = true;
            this.moving = true;
        }

        public PacketPlayerLookMove(double par1, double par3, double par5, double par7, float par9, float par10, bool par11)
        {
            this.PacketID = 13;
            this.xPosition = par1;
            this.yPosition = par3;
            this.stance = par5;
            this.zPosition = par7;
            this.yaw = par9;
            this.pitch = par10;
            this.onGround = par11;
            this.rotating = true;
            this.moving = true;
        }

        protected override void readData(System.IO.BinaryReader DataInput)
        {
            //byte[] tryhard = DataInput.ReadBytes(41);
            this.xPosition = BitConverter.Int64BitsToDouble(IPAddress.NetworkToHostOrder(DataInput.ReadInt64()));
            this.yPosition = BitConverter.Int64BitsToDouble(IPAddress.NetworkToHostOrder(DataInput.ReadInt64()));
            this.stance = BitConverter.Int64BitsToDouble(IPAddress.NetworkToHostOrder(DataInput.ReadInt64()));
            this.zPosition = BitConverter.Int64BitsToDouble(IPAddress.NetworkToHostOrder(DataInput.ReadInt64()));
            this.yaw = BitConverter.ToSingle(BitConverter.GetBytes(IPAddress.NetworkToHostOrder(DataInput.ReadInt32())),0);
            this.pitch = BitConverter.ToSingle(BitConverter.GetBytes(IPAddress.NetworkToHostOrder(DataInput.ReadInt32())), 0);           
            this.onGround = DataInput.ReadBoolean();       
        }

        protected override void writeData(System.IO.BinaryWriter DataOutput)
        {           
            DataOutput.Write(IPAddress.HostToNetworkOrder(BitConverter.DoubleToInt64Bits(this.xPosition)));
            DataOutput.Write(IPAddress.HostToNetworkOrder(BitConverter.DoubleToInt64Bits(this.yPosition)));
            DataOutput.Write(IPAddress.HostToNetworkOrder(BitConverter.DoubleToInt64Bits(this.stance)));
            DataOutput.Write(IPAddress.HostToNetworkOrder(BitConverter.DoubleToInt64Bits(this.zPosition)));
            DataOutput.Write(IPAddress.HostToNetworkOrder((BitConverter.ToInt32(BitConverter.GetBytes(this.yaw),0))));
            DataOutput.Write(IPAddress.HostToNetworkOrder((BitConverter.ToInt32(BitConverter.GetBytes(this.pitch), 0))));        
            DataOutput.Write(this.onGround); 
        }
    }
}
