using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MinecraftEmuPTS.Packets
{
    class PacketSharedKey : Packet
    {
        private byte[] sharedSecret = new byte[0];
        private byte[] verifyToken = new byte[0];
      
        public PacketSharedKey()
        {
            this.PacketID = 252;
        }

        public PacketSharedKey(byte[] Secret, byte[] Token)
        {
            this.PacketID = 252;
            sharedSecret = Secret;
            verifyToken = Token;
        }

        override protected void readData(BinaryReader DataInput)
        {
            this.sharedSecret = readBytesFromStream(DataInput);
            this.verifyToken = readBytesFromStream(DataInput);
        }

        override protected void writeData(BinaryWriter DataOutput)
        {
            writeByteArray(DataOutput, this.sharedSecret);
            writeByteArray(DataOutput, this.verifyToken);
        }
    }
}
