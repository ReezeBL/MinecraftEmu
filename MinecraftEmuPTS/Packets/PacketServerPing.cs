using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace MinecraftEmuPTS.NetHandler
{
    class PacketServerPing : Packet
    {
        public int readSuccessfully;
        public String host;
        public int port;

        public PacketServerPing() { }


        public PacketServerPing(int par1, String par2Str, int par3)
        {
            this.PacketID = 254;
            this.readSuccessfully = par1;
            this.host = par2Str;
            this.port = par3;
        }

        protected override void writeData(System.IO.BinaryWriter DataOutput)
        {
            DataOutput.Write((byte)1);
            DataOutput.Write((byte)250);
            writeString("MC|PingHost", DataOutput);
            DataOutput.Write(IPAddress.HostToNetworkOrder((short)( 3 + 2 * this.host.Length + 4)));
            DataOutput.Write((byte)this.readSuccessfully);
            writeString(this.host, DataOutput);
            DataOutput.Write(IPAddress.HostToNetworkOrder(this.port));
        }
    }
}
