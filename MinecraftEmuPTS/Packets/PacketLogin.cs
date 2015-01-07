using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace MinecraftEmuPTS.Packets
{
    class PacketLogin : Packet
    {
        public int clientEntityId;
        public String terrainType;
        public bool hardcoreMode;
        public int gameType;
        public int dimension;
        public byte difficultySetting;
        public byte worldHeight;
        public byte maxPlayers;
        private bool vanillaCompatible;

        public PacketLogin(bool vanilka)
        {
            this.PacketID = 1;
            this.vanillaCompatible = vanilka;
        }

        public PacketLogin() {
            this.PacketID = 1;
            vanillaCompatible = false;
        }

        protected override void readData(BinaryReader par1DataInput)
        {
            this.PacketID = 1;
            this.clientEntityId = IPAddress.NetworkToHostOrder(par1DataInput.ReadInt32());
            String s = readString(par1DataInput, 16);
            this.terrainType = s;

            byte b0 = par1DataInput.ReadByte();
            this.hardcoreMode = (b0 & 8) == 8;
            int i = b0 & -9;
            this.gameType = b0;

            if (vanillaCompatible)
            {
                this.dimension = par1DataInput.ReadByte();
            }
            else
            {
                this.dimension = IPAddress.NetworkToHostOrder(par1DataInput.ReadInt32());
            }

            this.difficultySetting = par1DataInput.ReadByte();
            this.worldHeight = par1DataInput.ReadByte();
            this.maxPlayers = par1DataInput.ReadByte();
        }
        protected override void writeData(BinaryWriter DataOutput)
        {
            DataOutput.Write(IPAddress.HostToNetworkOrder(this.clientEntityId));
            writeString(this.terrainType,DataOutput);
            if (this.hardcoreMode)
            {
                this.gameType |= 8;
            }
            DataOutput.Write((byte)this.gameType);

            if (vanillaCompatible)
            {
                DataOutput.Write((byte)this.dimension);
            }
            else
            {
                DataOutput.Write(IPAddress.HostToNetworkOrder(this.dimension));
            }

            DataOutput.Write(this.difficultySetting);
            DataOutput.Write(this.worldHeight);
            DataOutput.Write(this.maxPlayers);
        }
    }
}
