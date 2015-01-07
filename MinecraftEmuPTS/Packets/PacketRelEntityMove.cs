using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinecraftEmuPTS.Packets
{
    class PacketRelEntityMove : PacketEntity
    {
        public PacketRelEntityMove() { }
        protected override void readData(System.IO.BinaryReader DataInput)
        {
            base.readData(DataInput);
            this.xPosition = DataInput.ReadByte();
            this.yPosition = DataInput.ReadByte();
            this.zPosition = DataInput.ReadByte();
        }
        public override void processPacket(NetHandler.PacketHandler handle)
        {
            handle.HandlePacketRelEntityMove(this);
        }
    }
}
