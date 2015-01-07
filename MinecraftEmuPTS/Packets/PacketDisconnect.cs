using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MinecraftEmuPTS.Packets
{
    class PacketDisconnect : Packet
    {
        private String reason;      

        public PacketDisconnect()
        {
            this.PacketID = 255;
        }

        public PacketDisconnect(String par1Str)
        {
            this.PacketID = 255;
            this.reason = par1Str;
        }

        protected override void readData(BinaryReader DataInput)
        {
            this.reason = readString(DataInput, 256);
        }

        protected override void writeData(BinaryWriter DataOutput)
        {
            writeString(this.reason, DataOutput);
        }

        public String getReason()
        {
            return this.reason;
        }

        public override void processPacket(NetHandler.PacketHandler handle)
        {
            handle.HandlePacketDisconnect(this);
        }
    }
}
