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
            Run();
        }        

        private void Run()
        {
            while (manager.Connected)
            {
                try
                {
                    if (manager.InGame)
                    {                      
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
           
            manager.InGame = false;
            manager.Connected = false;
        }        

        private void AI()
        {
            AutoFeeding();
        }

        private void AutoFeeding()
        {
            if (manager.pControl.AutoFeed)
            {
                if (manager.player.hunger <= 18)
                {
                    manager.pControl.Command("useitemc");
                }
            }
        }

        
    }
}
