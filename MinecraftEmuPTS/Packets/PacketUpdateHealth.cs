using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace MinecraftEmuPTS.Packets
{
    class PacketUpdateHealth : Packet
    {
        public float healthMP;
        public int food;

        /**
         * Players logging on get a saturation of 5.0. Eating food increases the saturation as well as the food bar.
         */
        public float foodSaturation;
        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.healthMP = BitConverter.ToSingle(BitConverter.GetBytes(IPAddress.NetworkToHostOrder(DataInput.ReadInt32())), 0);
            this.food = IPAddress.NetworkToHostOrder(DataInput.ReadInt16());
            this.foodSaturation = BitConverter.ToSingle(BitConverter.GetBytes(IPAddress.NetworkToHostOrder(DataInput.ReadInt32())), 0);
        }
    }
}
