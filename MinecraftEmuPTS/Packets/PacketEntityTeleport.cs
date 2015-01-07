using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace MinecraftEmuPTS.Packets
{
    class PacketEntityTeleport : Packet
    {
        /** ID of the entity. */
        public int entityId;

        /** X position of the entity. */
        public int xPosition;

        /** Y position of the entity. */
        public int yPosition;

        /** Z position of the entity. */
        public int zPosition;

        /** Yaw of the entity. */
        public byte yaw;

        /** Pitch of the entity. */
        public byte pitch;

        public PacketEntityTeleport()
        {

        }

        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.entityId = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.xPosition = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.yPosition = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.zPosition = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.yaw = DataInput.ReadByte();
            this.pitch = DataInput.ReadByte();
        }

        public override void processPacket(NetHandler.PacketHandler handle)
        {
            handle.HandlePacketEntityTeleport(this);
        }
    }
}
