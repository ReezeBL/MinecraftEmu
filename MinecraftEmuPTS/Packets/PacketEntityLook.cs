using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using MinecraftEmuPTS.Packets;

namespace MinecraftEmuPTS.NetHandler
{
    class PacketEntityLook : PacketEntity
    {
        public PacketEntityLook()
        {
            this.rotating = true;
        }

        protected override void readData(System.IO.BinaryReader DataInput)
        {
            base.readData(DataInput);
            this.yaw = DataInput.ReadByte();
            this.pitch = DataInput.ReadByte();
        }
    }
}
