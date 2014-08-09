using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinecraftEmuPTS.Packets
{
    class PacketRespawn : Packet
    {
        public int respawnDimension;

        /**
         * The difficulty setting. 0 through 3 for peaceful, easy, normal, hard. The client always sends 1.
         */
        public int difficulty;

        /** Defaults to 128 */
        public int worldHeight;
        public int gameType;
        public String terrainType;

        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.respawnDimension = DataInput.ReadInt32();
            this.difficulty = DataInput.ReadByte();
            this.gameType = DataInput.ReadByte();
            this.worldHeight = DataInput.ReadInt16();
            String s = readString(DataInput, 16);
            this.terrainType = s;          
        }
    }
}
