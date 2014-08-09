using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace MinecraftEmuPTS.NetHandler
{
    class PacketTileEntityData : Packet
    {
        /** The X position of the tile entity to update. */
        public int xPosition;

        /** The Y position of the tile entity to update. */
        public int yPosition;

        /** The Z position of the tile entity to update. */
        public int zPosition;

        /** The type of update to perform on the tile entity. */
        public int actionType;

        public PacketTileEntityData()
        {
            this.PacketID = 132;
        }

        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.xPosition = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.yPosition = IPAddress.NetworkToHostOrder(DataInput.ReadInt16());
            this.zPosition = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.actionType = DataInput.ReadByte();
            readNBTTag(DataInput);
        }
    }
}
