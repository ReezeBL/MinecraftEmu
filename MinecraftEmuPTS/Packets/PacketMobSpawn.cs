using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace MinecraftEmuPTS.Packets
{
    class PacketMobSpawn : Packet
    {
        /** The entity ID. */
        public int entityId;

        /** The type of mob. */
        public int type;

        /** The X position of the entity. */
        public int xPosition;

        /** The Y position of the entity. */
        public int yPosition;

        /** The Z position of the entity. */
        public int zPosition;
        public int velocityX;
        public int velocityY;
        public int velocityZ;

        /** The yaw of the entity. */
        public byte yaw;

        /** The pitch of the entity. */
        public byte pitch;

        /** The yaw of the entity's head. */
        public byte headYaw;

        public PacketMobSpawn() { }
        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.entityId = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.type = DataInput.ReadByte() & 255;
            this.xPosition = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.yPosition = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.zPosition = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.yaw = DataInput.ReadByte();
            this.pitch = DataInput.ReadByte();
            this.headYaw = DataInput.ReadByte();
            this.velocityX = IPAddress.NetworkToHostOrder(DataInput.ReadInt16());
            this.velocityY = IPAddress.NetworkToHostOrder(DataInput.ReadInt16());
            this.velocityZ = IPAddress.NetworkToHostOrder(DataInput.ReadInt16());
            readWatchableObjects(DataInput);
        }
    }
}
