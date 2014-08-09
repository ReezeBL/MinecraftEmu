using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using MinecraftEmuPTS.GameInfo;

namespace MinecraftEmuPTS.Packets
{
    class PacketPlayerInventory : Packet
    {
        /** Entity ID of the object. */
        public int entityID;

        /** Equipment slot: 0=held, 1-4=armor slot */
        public int slot;

        public ItemStack item;

        public PacketPlayerInventory() { }

        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.entityID = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.slot = IPAddress.NetworkToHostOrder(DataInput.ReadInt16());
            this.item = readItemStack(DataInput);
        }
    }
}
