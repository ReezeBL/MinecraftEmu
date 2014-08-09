using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace MinecraftEmuPTS.NetHandler
{
    class PacketDoorChange : Packet
    {
        public int sfxID;
        public int auxData;
        public int posX;
        public int posY;
        public int posZ;
        private bool disableRelativeVolume;

        public PacketDoorChange() { }

        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.sfxID = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.posX = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.posY = DataInput.ReadByte() & 255;
            this.posZ = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.auxData = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.disableRelativeVolume = DataInput.ReadBoolean();
        }
    }
}
