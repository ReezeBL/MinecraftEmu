using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinecraftEmuPTS.GameInfo
{
    class ModList
    {
        public static Dictionary<String, String> ModMap; 
        public static void Init(String Filename)
        {
            ModMap = new Dictionary<string, string>();
            String[] Mods = System.IO.File.ReadAllLines(@Filename);
            for (int i = 0; i < Mods.Length; i++)
            {
                String[] ModParams = Mods[i].Split(" ".ToCharArray());
                ModMap.Add(ModParams[0], ModParams[1]);
            }
        }
    }
}
