using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinecraftEmuPTS.GameInfo
{
    class EntityPlayer
    {
        public int Id;
        public double x, yFeet,yHead, z;
        public float pitch, yaw;
        public float health, sat;
        public int hunger;
        public bool onground;
        public String name;
        public ItemStack[] Inventory;
        public int HeldSlot;
        public EntityPlayer()
        {
            Id = 0;
            x = 0;          
            z = 0;
            onground = true;
            Inventory = new ItemStack[45];
        }
        public override string ToString()
        {
            return name + " [" + (int)x+";"+(int)yFeet+";"+(int)z+"]";
        }
        public ItemStack getHeldItem()
        {
            return Inventory[36 + HeldSlot];
        }
    }
}
