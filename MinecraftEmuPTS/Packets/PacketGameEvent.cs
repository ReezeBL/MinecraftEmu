using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinecraftEmuPTS.NetHandler
{
    class PacketGameEvent : Packet
    {
        /** 0: Invalid bed, 1: Rain starts, 2: Rain stops, 3: Game mode changed. */
        public int eventType;

        /**
         * When reason==3, the game mode to set.  See EnumGameType for a list of values.
         */
        public int gameMode;

        public PacketGameEvent() { }

        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.eventType = DataInput.ReadByte();
            this.gameMode = DataInput.ReadByte();
        }
    }
}
