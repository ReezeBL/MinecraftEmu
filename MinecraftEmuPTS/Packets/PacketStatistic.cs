using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace MinecraftEmuPTS.NetHandler
{
    class PacketStatistic : Packet
    {
        public int statisticId;
        public int amount;

        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.statisticId = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.amount = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
        }
    }
}
