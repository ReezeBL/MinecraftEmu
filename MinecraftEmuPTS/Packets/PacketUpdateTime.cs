using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace MinecraftEmuPTS.Packets
{
    class PacketUpdateTime : Packet
    {
        /** World age in ticks. */
        public long worldAge;

        /** The world time in minutes. */
        public long time;

        public PacketUpdateTime() {
            this.PacketID = 4;
        }

        public PacketUpdateTime(long par1, long par3, bool par5)
        {
            this.PacketID = 4;
            this.worldAge = par1;
            this.time = par3;

            if (!par5)
            {
                this.time = -this.time;

                if (this.time == 0L)
                {
                    this.time = -1L;
                }
            }
        }

        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.worldAge = IPAddress.NetworkToHostOrder(DataInput.ReadInt64());
            this.time = IPAddress.NetworkToHostOrder(DataInput.ReadInt64());
        }

    }
}
