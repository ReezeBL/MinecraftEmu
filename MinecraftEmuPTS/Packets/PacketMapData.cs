using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace MinecraftEmuPTS.NetHandler
{
    class PacketMapData : Packet
    {
        public short itemID;
        /**
    * Contains a unique ID for the item that this packet will be populating.
    */
        public short uniqueID;

        /**
         * Contains a buffer of arbitrary data with which to populate an individual item in the world.
         */
        public byte[] itemData;

        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.itemID = IPAddress.NetworkToHostOrder(DataInput.ReadInt16());
            this.uniqueID = IPAddress.NetworkToHostOrder(DataInput.ReadInt16());
            this.itemData = new byte[IPAddress.NetworkToHostOrder(DataInput.ReadInt16())];
            DataInput.Read(this.itemData,0,this.itemData.Length);
        }
    }
}
