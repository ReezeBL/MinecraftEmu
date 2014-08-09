using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
namespace MinecraftEmuPTS.NetHandler
{
    class PacketSetPlayerTeam : Packet
    {
        /** A unique name for the team. */
        public String teamName = "";

        /** Only if mode = 0 or 2. */
        public String teamDisplayName = "";

        /**
         * Only if mode = 0 or 2. Displayed before the players' name that are part of this team.
         */
        public String teamPrefix = "";

        /**
         * Only if mode = 0 or 2. Displayed after the players' name that are part of this team.
         */
        public String teamSuffix = "";
        public int mode;

        /** Only if mode = 0 or 2. */
        public int friendlyFire;


        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.teamName = readString(DataInput, 16);
            this.mode = DataInput.ReadByte();

            if (this.mode == 0 || this.mode == 2)
            {
                this.teamDisplayName = readString(DataInput, 32);
                this.teamPrefix = readString(DataInput, 16);
                this.teamSuffix = readString(DataInput, 16);
                this.friendlyFire = DataInput.ReadByte();
            }

            if (this.mode == 0 || this.mode == 3 || this.mode == 4)
            {
                short short1 = IPAddress.NetworkToHostOrder(DataInput.ReadInt16());

                for (int i = 0; i < short1; ++i)
                {
                    readString(DataInput, 16);
                }
            }
        }
    }
}
