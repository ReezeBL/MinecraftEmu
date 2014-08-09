using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace MinecraftEmuPTS.NetHandler
{
    class PacketLevelSound : Packet
    {
        /** e.g. step.grass */
        private String soundName;

        /** Effect X multiplied by 8 */
        private int effectX;

        /** Effect Y multiplied by 8 */
        private int effectY = Int32.MaxValue;

        /** Effect Z multiplied by 8 */
        private int effectZ;

        /** 1 is 100%. Can be more. */
        private float volume;

        /** 63 is 100%. Can be more. */
        private int pitch;

        public PacketLevelSound() { }

        protected override void readData(System.IO.BinaryReader DataInput)
        {
            this.soundName = readString(DataInput, 256);
            this.effectX = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.effectY = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.effectZ = IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.volume = (float)IPAddress.NetworkToHostOrder(DataInput.ReadInt32());
            this.pitch = DataInput.ReadByte();
        }
    }
}
