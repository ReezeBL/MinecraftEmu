using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MinecraftEmuPTS.Packets
{
    class PacketClientCommand : Packet
    {
        public int forceRespawn;

        public PacketClientCommand(Packet packet)
        {
            this.PacketID = 205;
            this.RawData = packet.RawData;
            this.Read();
        }

        public PacketClientCommand() {
            this.PacketID = 205;
        }

        public PacketClientCommand(int par1)
        {
            this.PacketID = 205;
            this.forceRespawn = par1;
        }

        override protected void readData(BinaryReader DataInput)
        {
            this.forceRespawn = DataInput.ReadByte();
        }

        override protected void writeData(BinaryWriter DataOutput)
        {
            DataOutput.Write((byte)(forceRespawn & 205));
        }

    }
}
