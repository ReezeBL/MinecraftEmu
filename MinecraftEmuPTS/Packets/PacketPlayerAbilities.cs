using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinecraftEmuPTS.NetHandler
{
    class PacketPlayerAbilities : Packet
    {

        private float flySpeed;
        private float walkSpeed;

        public PacketPlayerAbilities() {
            this.PacketID = 202;
        }
        protected override void readData(System.IO.BinaryReader DataInput)
        {
            DataInput.ReadByte();
            flySpeed = DataInput.ReadInt32();
            walkSpeed = DataInput.ReadInt32();
        }
    }
}
