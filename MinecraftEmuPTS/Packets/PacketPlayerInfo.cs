using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace MinecraftEmuPTS.Packets
{
    class PacketPlayerInfo : Packet
    {
        /** The player's name. */
        public String playerName;

        /** Byte that tells whether the player is connected. */
        public bool isConnected;
        public int ping;

        public PacketPlayerInfo() {
            this.PacketID = 201;
        }

        public PacketPlayerInfo(String par1Str, bool par2, int par3)
        {
            this.PacketID = 201;
            this.playerName = par1Str;
            this.isConnected = par2;
            this.ping = par3;
        }

        protected override void readData(BinaryReader DataInput)
        {
            this.playerName = readString(DataInput, 16);
            this.isConnected = DataInput.ReadByte() != 0;
            this.ping = IPAddress.NetworkToHostOrder(DataInput.ReadInt16());
        }

        protected override void writeData(BinaryWriter DataOutput)
        {
            writeString(this.playerName, DataOutput);
            DataOutput.Write(this.isConnected);
            DataOutput.Write(IPAddress.HostToNetworkOrder((short)this.ping));
        }
    }
}
