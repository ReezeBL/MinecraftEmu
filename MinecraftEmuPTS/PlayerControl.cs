using MinecraftEmuPTS.GameInfo;
using MinecraftEmuPTS.NetHandler;
using MinecraftEmuPTS.Packets;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace MinecraftEmuPTS
{
    class PlayerControl
    {
        ConnectionManager manager;
        public bool AutoFeed = false;
        public Timer timer;
        public PlayerControl(ConnectionManager _manager)
        {
            manager = _manager;           
        }

        public void Command(params String[] p)
        {
            if(manager.Connected){
                switch (p[0])
                {
                    case "chat":
                        manager.AddToSendingQueue(new PacketChat(p[1]));
                        break;
                    case "move":                      
                        this.MovePlayer(p[1], Convert.ToDouble(p[2]));
                        break;
                    case "attack":
                        this.AttackEntity(Convert.ToInt32(p[1]));
                        break;
                    case "use":                      
                        this.InteractWithEntity(Convert.ToInt32(p[1]));
                        break;
                    case "slot":                       
                        this.SweepSlot(Convert.ToInt32(p[1]));
                        break;
                    case "drop":
                        this.DropItem();
                        break;
                    case "useitemc":
                        this.StartUseItem();
                        if (timer != null) timer.Dispose();
                        timer = new Timer(itemusecallback, null, 2500, Timeout.Infinite);
                        break;
                    case "useitem":
                        this.StartUseItem();
                        break;
                    case "respawn":
                        this.Respawn();
                        break;
                    case "feed":                      
                        if (p[1] == "on")
                            AutoFeed = true;
                        else
                            AutoFeed = false;
                        break;
                    case "disconnect":
                        manager.AddToSendingQueue(new PacketDisconnect("Quitting"));
                        manager.Connected = false;
                        break;
                    default:
                        CustomLib.putsc("Unknown command!\n", Color.Aquamarine);
                        break;
                }
            }
        }


        #region Commands
        public void MovePlayer(String dir, double n)
        {
            if (dir != null)
            {
                if (dir == "x")
                    manager.player.x += n;
                if (dir == "y")
                {
                    manager.player.onground = false;
                    manager.player.yFeet += n;
                    manager.player.yHead += n;
                }
                if (dir == "z")
                    manager.player.z += n;
            }
        }

        public void AttackEntity(int TargetId)
        {
            manager.AddToSendingQueue(new PacketUseEntity(manager.player.Id, TargetId, 1));
        }

        public void InteractWithEntity(int TargetId)
        {
            manager.AddToSendingQueue(new PacketUseEntity(manager.player.Id, TargetId, 0));
        }

        public void SweepSlot(int slot)
        {
            manager.AddToSendingQueue(new PacketBlockItemSwitch(slot));
            manager.player.HeldSlot = slot;
        }

        public void DropItem()
        {
            manager.AddToSendingQueue(new PacketBlockDig(4, 0, 0, 0, 0));
        }

        public void DropStack()
        {
            manager.AddToSendingQueue(new PacketBlockDig(3, 0, 0, 0, 0));
        }

        public void StartUseItem()
        {
            ItemStack item = manager.player.getHeldItem();
            if (item != null)
                manager.AddToSendingQueue(new PacketPlace(-1, -1, -1, 255, item, 0, 0, 0));
            else
            {                
                CustomLib.putsc("You dont hold any item!\n", Color.Aquamarine);
            }
        }

        public void StopUseItem()
        {
            manager.AddToSendingQueue(new PacketBlockDig(0, 0, 0, 5, 255));
        }

        public void Respawn()
        {
            manager.AddToSendingQueue(new PacketClientCommand(1));
        }
        #endregion

        private void itemusecallback(Object obj)
        {
            manager.pControl.StopUseItem();
        }
    
    }
}
