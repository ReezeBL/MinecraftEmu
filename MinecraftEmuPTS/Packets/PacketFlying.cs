using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinecraftEmuPTS.Packets
{
    class PacketFlying : Packet
    {
        /** The player's X position. */
        public double xPosition;

        /** The player's Y position. */
        public double yPosition;

        /** The player's Z position. */
        public double zPosition;

        /** The player's stance. (boundingBox.minY) */
        public double stance;

        /** The player's yaw rotation. */
        public float yaw;

        /** The player's pitch rotation. */
        public float pitch;

        /** True if the client is on the ground. */
        public bool onGround;

        /** Boolean set to true if the player is moving. */
        public bool moving;

        /** Boolean set to true if the player is rotating. */
        public bool rotating;

        public PacketFlying()
        {
            this.PacketID = 10;
        }

        public PacketFlying(bool par1)
        {
            this.PacketID = 10;
            this.onGround = par1;
        }

        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.onGround = DataInput.ReadByte() != 0;
        }

        protected override void writeData(System.IO.BinaryWriter DataOutput)
        {
            DataOutput.Write(this.onGround);
        }
    }
}
