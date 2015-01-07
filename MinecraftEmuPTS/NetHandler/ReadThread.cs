using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.IO;

namespace MinecraftEmuPTS.NetHandler
{
    class ReadThread
    {
        ConnectionManager manager;
        public static Int32 lastID;
        public ReadThread(ConnectionManager manager)
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
                    Packet packet = manager.GetDividedPacket();
                    if (packet != null)
                    {
                        lastID = packet.PacketID;
                        manager.PutPacket(packet);
                    }
                }
                catch (IOException ex)
                {
                    Console.WriteLine("Thread is stopping...\n Last packet id: " + lastID);
                    break;
                }
                catch (Exception ex)
                {
                    Logger.WriteLog(ex);
                    break;
                }
                //Thread.Sleep(latency);
            }
            manager.Connected = false;
        }
    }
}
