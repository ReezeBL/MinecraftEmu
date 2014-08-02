using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace MinecraftEmuPTS.Packets
{
    class PacketHandshake : Packet
    {
        private int protocolVersion;
        private String username;
        private String serverHost;
        private int serverPort;

        public PacketHandshake(Packet packet)
        {
            this.PacketID = 2;
            this.RawData = packet.RawData;
            this.Read();
        }

        public PacketHandshake() {
            this.PacketID = 2;
        }
        public PacketHandshake(int ver, String username, String host, int port)
        {
            this.PacketID = 2;
            this.protocolVersion = ver;
            this.username = username;
            this.serverHost = host;
            this.serverPort = port;    
        }
        override protected void readData(BinaryReader DataInput)
        {
            this.protocolVersion = DataInput.ReadByte();
            this.username = readString(DataInput, 16);
            this.serverHost = readString(DataInput, 255);
            this.serverPort = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
        }
        override protected void writeData(BinaryWriter DataOutput)
        {
            DataOutput.Write((byte)this.protocolVersion);
            writeString(this.username, DataOutput);
            writeString(this.serverHost, DataOutput);
            DataOutput.Write(IPAddress.HostToNetworkOrder(this.serverPort));
        }

        public int getPacketSize()
        {
            return 3 + 2 * this.username.Length;
        }
        public int getProtocolVersion()
        {
            return this.protocolVersion;
        }
        public String getUsername()
        {
            return this.username;
        }

    }
}
