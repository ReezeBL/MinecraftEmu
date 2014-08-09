using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using MinecraftEmuPTS.Packets;

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
            while (true)
            {
                if (manager.Connected)
                {
                    ParseCommand();
                    AI();
                    manager.AddToSendingQueue(new PacketPlayerLookMove(ConnectionManager.player.x, ConnectionManager.player.yFeet, ConnectionManager.player.yHead, ConnectionManager.player.z, ConnectionManager.player.yaw, ConnectionManager.player.pitch, ConnectionManager.player.onground));
                    Thread.Sleep(50);
                }
            }
        }

        private void ParseCommand()
        {
            if (ConnectionManager.Tasks.Count > 0)
            {
                string command = ConnectionManager.Tasks.Dequeue();
                switch (command)
                {
                    case "chat":
                        manager.AddToSendingQueue(new PacketChat(ConnectionManager.Tasks.Dequeue()));
                        break;
                    case "move":
                        string[] parameters = ConnectionManager.Tasks.Dequeue().Split(" ".ToCharArray());
                        if (parameters.Length < 2)
                            break;
                        string dir = parameters[0];
                        double n = Convert.ToDouble(parameters[1]);
                        manager.MovePlayer(dir, n);
                        break;

                    case "attack":
                        int TargetId = Convert.ToInt32(ConnectionManager.Tasks.Dequeue());
                        manager.AttackEntity(TargetId);
                        break;
                    case "use":
                        TargetId = Convert.ToInt32(ConnectionManager.Tasks.Dequeue());
                        manager.InteractWithEntity(TargetId);
                        break;
                    case "slot":
                        int slot = Convert.ToInt32(ConnectionManager.Tasks.Dequeue());
                        manager.SweepSlot(slot);
                        break;
                    case "drop":
                        manager.DropItem();
                        break;
                    case "useitemc":
                        manager.StartUseItem();
                        timer = new Timer(itemusecallback, null, 2500,Timeout.Infinite);
                        break;
                    case "useitem":
                        manager.StartUseItem();
                        break;
                    case "respawn":
                        manager.Respawn();
                        break;
                    case "feed":
                        String flag = ConnectionManager.Tasks.Dequeue();
                        if (flag == "on")
                            AutoFeed = true;
                        else
                            AutoFeed = false;
                        break;
                    case "disconnect":
                        manager.AddToSendingQueue(new PacketDisconnect("Quitting"));
                        break;
                    default:
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
                if (ConnectionManager.player.hunger <= 18)
                {
                    ConnectionManager.Tasks.Enqueue("useitemc");
                }
            }
        }

        private void itemusecallback(Object obj)
        {
            manager.StopUseItem();
        }
    }
}
