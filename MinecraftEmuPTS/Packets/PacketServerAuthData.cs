using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Security.Cryptography;
using System.Net.Sockets;
using System.Net;

namespace MinecraftEmuPTS.Packets
{
    class PacketServerAuthData : Packet
    {
        private String serverId;
        private byte[] publicKey;
        private byte[] verifyToken = new byte[0];  

        public PacketServerAuthData() {
            this.PacketID = 253;
        }
        public PacketServerAuthData(String ServerId, byte[] PublicKey, byte[] Token)
        {
            this.PacketID = 253;
            this.serverId = ServerId;
            this.publicKey = PublicKey;
            this.verifyToken = Token;
        }
        override protected void readData(BinaryReader DataInput)
        {
            this.serverId = readString(DataInput, 30);   
            this.publicKey = readBytesFromStream(DataInput);
            this.verifyToken = readBytesFromStream(DataInput);
        }
        protected override void writeData(BinaryWriter DataOutput)
        {
            writeString(this.serverId, DataOutput);
            writeByteArray(DataOutput, publicKey);
            writeByteArray(DataOutput, verifyToken);
        }    

        public byte[] GetPublicKey()
        {
            return this.publicKey;
        }

        public String getServerID()
        {
            return this.serverId;
        }

        public byte[] GetVerifyToken()
        {
            return this.verifyToken;
        }

        public override void processPacket(NetHandler.PacketHandler handle)
        {
            handle.HandlePacketServerAuthData(this);
        }
    }
}
