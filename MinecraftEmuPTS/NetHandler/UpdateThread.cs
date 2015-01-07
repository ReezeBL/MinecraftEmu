using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using MinecraftEmuPTS.Packets;
using System.Drawing;
using System.IO;
using System.Net.Sockets;

namespace MinecraftEmuPTS.NetHandler
{
    class UpdateThread
    {
        //PacketFlying player_position; 
        ConnectionManager manager;
        public UpdateThread(ConnectionManager manager)
        {
            this.manager = manager;
            AutoFeed = false;                  
            Run();
        }
        bool AutoFeed;
        Timer timer;

        private void Run()
        {
            while (manager.Connected)
            {
                try
                {
                    if (manager.InGame)
                    {
                        ParseCommand();
                        AI();
                        manager.AddToSendingQueue(new PacketPlayerLookMove(manager.player.x, manager.player.yFeet, manager.player.yHead, manager.player.z, manager.player.yaw, manager.player.pitch, manager.player.onground));
                    }
                    manager.SendPacket();
                    Thread.Sleep(50);
                }
                catch (IOException ex)
                {
                    break;
                }
                catch (SocketException ex)
                {
                    break;
                }
                catch (Exception ex)
                {
                    Logger.WriteLog(ex);
                    break;
                }
            }
            if (timer != null)
            {
                timer.Dispose();
            }
            manager.InGame = false;
            manager.Connected = false;
        }

        private void ParseCommand()
        {
            while (manager.Tasks.Count > 0)
            {
                string command = manager.Tasks.Dequeue();
                switch (command)
                {
                    case "chat":
                        manager.AddToSendingQueue(new PacketChat(manager.Tasks.Dequeue()));
                        break;
                    case "move":
                        string[] parameters = manager.Tasks.Dequeue().Split(" ".ToCharArray());
                        if (parameters.Length < 2)
                            break;
                        string dir = parameters[0];
                        double n = Convert.ToDouble(parameters[1]);
                        manager.pControl.MovePlayer(dir, n);
                        break;

                    case "attack":
                        int TargetId = Convert.ToInt32(manager.Tasks.Dequeue());
                        manager.pControl.AttackEntity(TargetId);
                        break;
                    case "use":
                        TargetId = Convert.ToInt32(manager.Tasks.Dequeue());
                        manager.pControl.InteractWithEntity(TargetId);
                        break;
                    case "slot":
                        int slot = Convert.ToInt32(manager.Tasks.Dequeue());
                        manager.pControl.SweepSlot(slot);
                        break;
                    case "drop":
                        manager.pControl.DropItem();
                        break;
                    case "useitemc":
                        manager.pControl.StartUseItem();
                        timer = new Timer(itemusecallback, null, 2500, Timeout.Infinite);
                        break;
                    case "useitem":
                        manager.pControl.StartUseItem();
                        break;
                    case "respawn":
                        manager.pControl.Respawn();
                        break;
                    case "feed":
                        String flag = manager.Tasks.Dequeue();
                        if (flag == "on")
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

        private void AI()
        {
            AutoFeeding();
        }

        private void AutoFeeding()
        {
            if (AutoFeed)
            {
                if (manager.player.hunger <= 18)
                {
                    manager.Tasks.Enqueue("useitemc");
                }
            }
        }

        private void itemusecallback(Object obj)
        {
            manager.pControl.StopUseItem();
        }
    }
}
