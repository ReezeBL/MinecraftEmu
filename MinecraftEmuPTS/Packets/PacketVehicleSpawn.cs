using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace MinecraftEmuPTS.Packets
{
    class PacketVehicleSpawn : Packet
    {
        /** Entity ID of the object. */
        public int entityId;

        /** The X position of the object. */
        public int xPosition;

        /** The Y position of the object. */
        public int yPosition;

        /** The Z position of the object. */
        public int zPosition;

        /**
         * Not sent if the thrower entity ID is 0. The speed of this fireball along the X axis.
         */
        public int speedX;

        /**
         * Not sent if the thrower entity ID is 0. The speed of this fireball along the Y axis.
         */
        public int speedY;

        /**
         * Not sent if the thrower entity ID is 0. The speed of this fireball along the Z axis.
         */
        public int speedZ;

        /** The pitch in steps of 2p/256 */
        public int pitch;

        /** The yaw in steps of 2p/256 */
        public int yaw;

        /** The type of object. */
        public int type;

        /** 0 if not a fireball. Otherwise, this is the Entity ID of the thrower. */
        public int throwerEntityId;

        public PacketVehicleSpawn()
        {

        }

        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.entityId = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.type = DataInput.ReadByte();
            this.xPosition = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.yPosition = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.zPosition = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.pitch = DataInput.ReadByte();
            this.yaw = DataInput.ReadByte();
            this.throwerEntityId = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());

            if (this.throwerEntityId > 0)
            {
                this.speedX = IPAddress.NetworkToHostOrder(DataInput.ReadInt16());
                this.speedY = IPAddress.NetworkToHostOrder(DataInput.ReadInt16());
                this.speedZ = IPAddress.NetworkToHostOrder(DataInput.ReadInt16());
            }
        }
    }
}
