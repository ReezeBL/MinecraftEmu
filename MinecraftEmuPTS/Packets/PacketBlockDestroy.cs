using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace MinecraftEmuPTS.NetHandler
{
    class PacketBlockDestroy : Packet
    {
        /** Entity breaking the block */
        private int entityId;

        /** X posiiton of the block */
        private int posX;

        /** Y position of the block */
        private int posY;

        /** Z position of the block */
        private int posZ;

        /** How far destroyed this block is */
        private int destroyedStage;

        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.entityId = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.posX = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.posY = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.posZ = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.destroyedStage = DataInput.ReadByte();
        }
    }
}
