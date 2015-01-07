using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace MinecraftEmuPTS.Packets
{
    class PacketExplosion : Packet
    {
        public double explosionX;
        public double explosionY;
        public double explosionZ;
        public float explosionSize;
        //public List chunkPositionRecords;

        /** X velocity of the player being pushed by the explosion */
        private float playerVelocityX;

        /** Y velocity of the player being pushed by the explosion */
        private float playerVelocityY;

        /** Z velocity of the player being pushed by the explosion */
        private float playerVelocityZ;

        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.explosionX = BitConverter.Int64BitsToDouble(IPAddress.NetworkToHostOrder(DataInput.ReadInt64())); ;
            this.explosionY = BitConverter.Int64BitsToDouble(IPAddress.NetworkToHostOrder(DataInput.ReadInt64())); ;
            this.explosionZ = BitConverter.Int64BitsToDouble(IPAddress.NetworkToHostOrder(DataInput.ReadInt64())); ;
            this.explosionSize = BitConverter.ToSingle(BitConverter.GetBytes(IPAddress.NetworkToHostOrder(DataInput.ReadInt32())), 0);
            int i = DataInput.ReadInt32();          
            int j = (int)this.explosionX;
            int k = (int)this.explosionY;
            int l = (int)this.explosionZ;

            for (int i1 = 0; i1 < i; ++i1)
            {
                int j1 = DataInput.ReadByte() + j;
                int k1 = DataInput.ReadByte() + k;
                int l1 = DataInput.ReadByte() + l;              
            }

            this.playerVelocityX = BitConverter.ToSingle(BitConverter.GetBytes(IPAddress.NetworkToHostOrder(DataInput.ReadInt32())), 0);
            this.playerVelocityY = BitConverter.ToSingle(BitConverter.GetBytes(IPAddress.NetworkToHostOrder(DataInput.ReadInt32())), 0);
            this.playerVelocityZ = BitConverter.ToSingle(BitConverter.GetBytes(IPAddress.NetworkToHostOrder(DataInput.ReadInt32())), 0);
        }

    }
}
