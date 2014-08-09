using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
namespace MinecraftEmuPTS.Packets
{
    class PacketUseEntity : Packet
    {
        /** The entity of the player (ignored by the server) */
        public int playerEntityId;

        /** The entity the player is interacting with */
        public int targetEntity;

        /**
         * Seems to be true when the player is pointing at an entity and left-clicking and false when right-clicking.
         */
        public int isLeftClick;

        public PacketUseEntity()
        {
            this.PacketID = 7;
        }

        public PacketUseEntity(int pe, int target, int leftclick)
        {
            this.PacketID = 7;
            this.playerEntityId = pe;
            this.targetEntity = target;
            this.isLeftClick = leftclick;
        }

        protected override void writeData(System.IO.BinaryWriter DataOutput)
        {
            DataOutput.Write(IPAddress.HostToNetworkOrder(playerEntityId));
            DataOutput.Write(IPAddress.HostToNetworkOrder(targetEntity));
            DataOutput.Write((byte)isLeftClick);
        }
    }
}
