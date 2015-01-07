using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinecraftEmuPTS.Packets
{
    class PacketChat : Packet
    {
        public String message;
        private bool isServer;

        public PacketChat() { 
            isServer = true;
        }

        public PacketChat(String message)
        {
            this.PacketID = 3;
            this.message = message;
        }

        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.message = readString(DataInput, 32767);
        }

        protected override void writeData(System.IO.BinaryWriter DataOutput)
        {
            writeString(this.message, DataOutput);
        }

        public override void processPacket(NetHandler.PacketHandler handle)
        {
            handle.HandlePacketChat(this);
        }
    }
}
