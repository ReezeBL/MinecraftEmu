using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace MinecraftEmuPTS.NetHandler
{
    class PacketPlayerAbilities : Packet
    {
        private byte flags;
        private int flySpeed;
        private int walkSpeed;

        public PacketPlayerAbilities() {
            this.PacketID = 202;
        }

        public PacketPlayerAbilities(byte flags, int ms, int fs)
        {
            this.PacketID = 202;
        }

        protected override void readData(System.IO.BinaryReader DataInput)
        {
            flags = DataInput.ReadByte();
            flySpeed = DataInput.ReadInt32();
            walkSpeed = DataInput.ReadInt32();
        }
        protected override void writeData(System.IO.BinaryWriter DataOutput)
        {
            DataOutput.Write(flags);
            DataOutput.Write(IPAddress.HostToNetworkOrder(flySpeed));
            DataOutput.Write(IPAddress.HostToNetworkOrder(walkSpeed));
        }
    }
}
