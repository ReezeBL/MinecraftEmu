using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace MinecraftEmuPTS.NetHandler
{
    class WriteThread
    {
        ConnectionManager manager;
        public WriteThread(ConnectionManager manager)
        {
            this.manager = manager;
            Run();
        }

        private void Run()
        {
            while (true)
            {
                try
                {
                    manager.SendPacket();
                    Thread.Sleep(10);
                }
                catch(IOException ex)
                {
                    Logger.WriteLog(ex);
                    Console.WriteLine("Write thread is stopping");
                    Thread.CurrentThread.Abort();
                }
            }
        }


    }
}
