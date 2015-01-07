using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace MinecraftEmuPTS.NetHandler
{
    class PacketWeather : Packet
    {
        public int entityID;
        public int posX;
        public int posY;
        public int posZ;
        public int isLightningBolt;

        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.entityID = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.isLightningBolt = DataInput.ReadByte();
            this.posX = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.posY = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.posZ = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
        }
    }
}
