using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace MinecraftEmuPTS.Packets
{
    class PacketCustomPayload : Packet
    {
        /** Name of the 'channel' used to send data */
        public String channel;

        /** Length of the data to be read */
        public int length;

        /** Any data */
        public byte[] data;

        public PacketCustomPayload() {
            this.PacketID = 250;
        }

        public PacketCustomPayload(String par1Str, byte[] par2ArrayOfByte)
        {
            this.PacketID = 250;
            this.channel = par1Str;
            this.data = par2ArrayOfByte;

            if (par2ArrayOfByte != null)
            {
                this.length = par2ArrayOfByte.Length;

                if (this.length > 32767)
                {
                    throw new Exception("Payload may not be larger than 32k");
                }
            }
        }

        protected override void readData(BinaryReader DataInput)
        {
            this.channel = readString(DataInput, 20);
            this.length = IPAddress.NetworkToHostOrder(DataInput.ReadInt16());

            if (this.length > 0 && this.length < 32767)
            {
                this.data = new byte[this.length];
                DataInput.Read(this.data,0,this.data.Length);
            }
        }

        protected override void writeData(BinaryWriter DataOutput)
        {
            writeString(this.channel, DataOutput);
            DataOutput.Write(IPAddress.HostToNetworkOrder((short)this.length));

            if (this.data != null)
            {
                DataOutput.Write(this.data);
            }
        }
    }
}
