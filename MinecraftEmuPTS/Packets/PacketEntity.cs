using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
namespace MinecraftEmuPTS.Packets
{
    class PacketEntity : Packet
    {
        /** The ID of this entity. */
        public int entityId;

        /** The X axis relative movement. */
        public byte xPosition;

        /** The Y axis relative movement. */
        public byte yPosition;

        /** The Z axis relative movement. */
        public byte zPosition;

        /** The X axis rotation. */
        public byte yaw;

        /** The Y axis rotation. */
        public byte pitch;

        /** Boolean set to true if the entity is rotating. */
        public bool rotating;

        public PacketEntity() { }

        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.entityId = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
        }
    }
}
