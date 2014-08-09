using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace MinecraftEmuPTS.Packets
{
    class PacketUpdateAttributes : Packet
    {
        public PacketUpdateAttributes()
        {

        }

        protected override void readData(System.IO.BinaryReader DataInput)
        {
            DataInput.ReadInt32();
            int i = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            for (int j = 0; j < i; ++j)
            {
                readString(DataInput, 64);
                DataInput.ReadDouble();
                short l = IPAddress.NetworkToHostOrder(DataInput.ReadInt16());
                for (int k = 0; k < l; ++k)
                {
                    DataInput.ReadInt64();
                    DataInput.ReadInt64();
                    DataInput.ReadInt64();
                    DataInput.ReadByte();
                }
            }
        }
    }
}
