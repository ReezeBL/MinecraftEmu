using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace MinecraftEmuPTS.Packets
{
    class PacketPlayNoteBlock : Packet
    {
        public int xLocation;
        public int yLocation;
        public int zLocation;

        /** 1=Double Bass, 2=Snare Drum, 3=Clicks / Sticks, 4=Bass Drum, 5=Harp */
        public int instrumentType;

        /**
         * The pitch of the note (between 0-24 inclusive where 0 is the lowest and 24 is the highest).
         */
        public int pitch;

        /** The block ID this action is set for. */
        public int blockId;

        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.xLocation = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.yLocation = IPAddress.NetworkToHostOrder(DataInput.ReadInt16());
            this.zLocation = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.instrumentType = DataInput.ReadByte();
            this.pitch = DataInput.ReadByte();
            this.blockId = IPAddress.NetworkToHostOrder(DataInput.ReadInt16()) &4095;
        }
    }
}
