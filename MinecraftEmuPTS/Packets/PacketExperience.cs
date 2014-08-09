using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
namespace MinecraftEmuPTS.NetHandler
{
    class PacketExperience : Packet
    {
        /** The current experience bar points. */
        public float experience;

        /** The total experience points. */
        public int experienceTotal;

        /** The experience level. */
        public int experienceLevel;


        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.experience = BitConverter.ToSingle(BitConverter.GetBytes(IPAddress.NetworkToHostOrder(DataInput.ReadInt32())), 0);
            this.experienceLevel = IPAddress.NetworkToHostOrder(DataInput.ReadInt16());
            this.experienceTotal = IPAddress.NetworkToHostOrder(DataInput.ReadInt16());
        }
    }
}
