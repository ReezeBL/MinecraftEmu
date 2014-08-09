using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinecraftEmuPTS.Packets
{
    class PacketCloseWindow : Packet
    {
        public int windowId;
        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.windowId = DataInput.ReadByte();
        }
    }
}
