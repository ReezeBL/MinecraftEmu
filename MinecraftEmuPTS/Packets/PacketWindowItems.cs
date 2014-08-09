using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using MinecraftEmuPTS.GameInfo;

namespace MinecraftEmuPTS.Packets
{
    class PacketWindowItems : Packet
    {
        public int WindowId;
        public ItemStack[] items;
        public PacketWindowItems() {
            this.PacketID = 104;
        }
        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.WindowId = DataInput.ReadByte();
            short num = IPAddress.NetworkToHostOrder(DataInput.ReadInt16());
            this.items = new ItemStack[num];
            for (int i = 0; i < num; i++)
            {
                items[i] = readItemStack(DataInput);
            }
        }
    }
}
