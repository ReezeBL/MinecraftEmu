using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace MinecraftEmuPTS.Packets
{
    class PacketNamedEntitySpawn : Packet
    {
        /** The entity ID, in this case it's the player ID. */
        public int entityId;

        /** The player's name. */
        public String name;

        /** The player's X position. */
        public int xPosition;

        /** The player's Y position. */
        public int yPosition;

        /** The player's Z position. */
        public int zPosition;

        /** The player's rotation. */
        public byte rotation;

        /** The player's pitch. */
        public byte pitch;

        /** The current item the player is holding. */
        public int currentItem;


        public PacketNamedEntitySpawn() { }
        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.entityId = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.name = readString(DataInput, 16);
            this.xPosition = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.yPosition = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.zPosition = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.rotation = DataInput.ReadByte();
            this.pitch = DataInput.ReadByte();
            this.currentItem = IPAddress.NetworkToHostOrder(DataInput.ReadInt16());
            readWatchableObjects(DataInput);
        }
    }
}
