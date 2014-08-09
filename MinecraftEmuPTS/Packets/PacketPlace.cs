using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using MinecraftEmuPTS.GameInfo;
namespace MinecraftEmuPTS.Packets
{
    class PacketPlace : Packet
    {
        private int xPosition;
        private int yPosition;
        private int zPosition;

        /** The offset to use for block/item placement. */
        private int direction;
        //private ItemStack itemStack;
        private ItemStack item;
        /** The offset from xPosition where the actual click took place */
        private float xOffset;

        /** The offset from yPosition where the actual click took place */
        private float yOffset;

        /** The offset from zPosition where the actual click took place */
        private float zOffset;

        public PacketPlace(int par1, int par2, int par3, int par4, ItemStack par5, float par6, float par7, float par8)
        {
            this.PacketID = 15;
            this.xPosition = par1;
            this.yPosition = par2;
            this.zPosition = par3;
            this.direction = par4;
            this.item = par5;
            this.xOffset = par6;
            this.yOffset = par7;
            this.zOffset = par8;
        }
        protected override void writeData(System.IO.BinaryWriter DataOutput)
        {
            DataOutput.Write(IPAddress.HostToNetworkOrder(this.xPosition));
            DataOutput.Write((byte)this.yPosition);
            DataOutput.Write(IPAddress.HostToNetworkOrder(this.zPosition));
            DataOutput.Write((byte)this.direction);
            writeItemStack(item,DataOutput);
            DataOutput.Write((byte)(this.xOffset * 16.0F));
            DataOutput.Write((byte)(this.yOffset * 16.0F));
            DataOutput.Write((byte)(this.zOffset * 16.0F));
        }
    }
}
