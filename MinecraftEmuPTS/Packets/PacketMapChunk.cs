using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace MinecraftEmuPTS.Packets
{
    class PacketMapChunk : Packet
    {
        /** The x-position of the transmitted chunk, in chunk coordinates. */
        public int xCh;

        /** The z-position of the transmitted chunk, in chunk coordinates. */
        public int zCh;

        /**
         * The y-position of the lowest chunk Section in the transmitted chunk, in chunk coordinates.
         */
        public int yChMin;

        /**
         * The y-position of the highest chunk Section in the transmitted chunk, in chunk coordinates.
         */
        public int yChMax;

        /** The transmitted chunk data, decompressed. */
        private byte[] chunkData;

        /** The compressed chunk data */
        private byte[] compressedChunkData;

        /**
         * Whether to initialize the Chunk before applying the effect of the Packet51MapChunk.
         */
        public bool includeInitialize;

        /** The length of the compressed chunk data byte array. */
        private int tempLength;

        /** A temporary storage for the compressed chunk data byte array. */
        private static byte[] temp = new byte[196864];


        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.xCh = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.zCh = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.includeInitialize = DataInput.ReadBoolean();
            this.yChMin = IPAddress.NetworkToHostOrder(DataInput.ReadInt16());
            this.yChMax = IPAddress.NetworkToHostOrder(DataInput.ReadInt16());
            this.tempLength = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            DataInput.ReadBytes(this.tempLength);
        }
    }
}
