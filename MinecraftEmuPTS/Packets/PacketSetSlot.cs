using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MinecraftEmuPTS.GameInfo;
using System.Net;

namespace MinecraftEmuPTS.Packets
{
    class PacketSetSlot : Packet
    {
        public int windowId;

        /** Slot that should be updated */
        public int itemSlot;

        /** Item stack */
        public ItemStack myItemStack;

        public PacketSetSlot()
        {
            this.PacketID = 103;
        }
        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.windowId = DataInput.ReadByte();
            this.itemSlot = IPAddress.NetworkToHostOrder(DataInput.ReadInt16());
            this.myItemStack = readItemStack(DataInput);
        }

        public override void processPacket(NetHandler.PacketHandler handle)
        {
            handle.HandlePacketSetSlot(this);
        }
    }
}
