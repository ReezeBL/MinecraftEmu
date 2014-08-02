using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace MinecraftEmuPTS.Packets
{
    class PacketSpawnPosition : Packet
    {
        /** X coordinate of spawn. */
    public int xPosition;

    /** Y coordinate of spawn. */
    public int yPosition;

    /** Z coordinate of spawn. */
    public int zPosition;

    public PacketSpawnPosition() {
        this.PacketID = 6;
    }

    public PacketSpawnPosition(int par1, int par2, int par3)
    {
        this.PacketID = 6;
        this.xPosition = par1;
        this.yPosition = par2;
        this.zPosition = par3;
    }

    /**
     * Abstract. Reads the raw packet data from the data stream.
     */
    protected override void readData(BinaryReader par1DataInput) 
    {
        this.xPosition = IPAddress.NetworkToHostOrder(par1DataInput.ReadInt32());
        this.yPosition = IPAddress.NetworkToHostOrder(par1DataInput.ReadInt32());
        this.zPosition = IPAddress.NetworkToHostOrder(par1DataInput.ReadInt32());
    }

    /**
     * Abstract. Writes the raw packet data to the data stream.
     */
    protected override void writeData(BinaryWriter par1DataOutput) 
    {
        par1DataOutput.Write(IPAddress.HostToNetworkOrder(this.xPosition));
        par1DataOutput.Write(IPAddress.HostToNetworkOrder(this.yPosition));
        par1DataOutput.Write(IPAddress.HostToNetworkOrder(this.zPosition));
    }
    }
}
