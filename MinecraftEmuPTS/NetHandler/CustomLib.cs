using MinecraftEmuPTS.Packets;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace MinecraftEmuPTS.NetHandler
{
    class CustomLib
    {
        public static MainWindow form;
        public static PacketLogin FakeFML()
        {
            PacketLogin fake = new PacketLogin(true);
            fake.clientEntityId = 1405675035;
            fake.dimension = 0x02;
            fake.gameType = -1;
            fake.terrainType = "default";
            return fake;
        }
        public static string strip_codes(string text)
        {
            // Strips the color codes from text.
            string smessage = text;

            if (smessage.Contains("§"))
            {

                smessage = smessage.Replace("§0", "");
                smessage = smessage.Replace("§1", "");
                smessage = smessage.Replace("§2", "");
                smessage = smessage.Replace("§3", "");
                smessage = smessage.Replace("§4", "");
                smessage = smessage.Replace("§5", "");
                smessage = smessage.Replace("§6", "");
                smessage = smessage.Replace("§7", "");
                smessage = smessage.Replace("§8", "");
                smessage = smessage.Replace("§9", "");
                smessage = smessage.Replace("§a", "");
                smessage = smessage.Replace("§b", "");
                smessage = smessage.Replace("§c", "");
                smessage = smessage.Replace("§d", "");
                smessage = smessage.Replace("§e", "");
                smessage = smessage.Replace("§f", "");
                smessage = smessage.Replace("§l", "");
                smessage = smessage.Replace("§r", "");
                smessage = smessage.Replace("§A", "");
                smessage = smessage.Replace("§B", "");
                smessage = smessage.Replace("§C", "");
                smessage = smessage.Replace("§D", "");
                smessage = smessage.Replace("§E", "");
                smessage = smessage.Replace("§F", "");

            }

            return smessage;
        }
        public static string convertCode(string text, string color)
        {
            switch (color.ToLower())
            {
                case "black":
                    text = "§0" + text;
                    break;
                case "dark blue":
                    text = "§1" + text;
                    break;
                case "dark green":
                    text = "§2" + text;
                    break;
                case "dark cyan":
                    text = "§3" + text;
                    break;
                case "dark red":
                    text = "§4" + text;
                    break;
                case "purple":
                    text = "§5" + text;
                    break;
                case "gold":
                    text = "§6" + text;
                    break;
                case "gray":
                    text = "§7" + text;
                    break;
                case "dark gray":
                    text = "§8" + text;
                    break;
                case "blue":
                    text = "§9" + text;
                    break;
                case "bright green":
                    text = "§a" + text;
                    break;
                case "cyan":
                    text = "§b" + text;
                    break;
                case "red":
                    text = "§c" + text;
                    break;
                case "pink":
                    text = "§d" + text;
                    break;
                case "yellow":
                    text = "§e" + text;
                    break;
                case "white":
                    text = "§f" + text;
                    break;
            }
            return text;
        }
        public static String POSTurl(String url)
        {
            WebRequest send = WebRequest.Create(url);
            WebResponse get = send.GetResponse();
            Stream stream = get.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string Data = sr.ReadToEnd();
            return Data;
        }
        public static string GetAuthToken()
        {
            if (File.Exists("AuthToken.txt"))
            {
                String[] Lines = File.ReadAllLines("AuthToken.txt");
                return Lines[0];
            }
            return "";
        }
        public static String readUTF8(BinaryReader InputData)
        {
            int length = IPAddress.NetworkToHostOrder(InputData.ReadInt16());
            BinaryReader id = new BinaryReader(InputData.BaseStream, System.Text.Encoding.UTF8);
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
                chars[i] = id.ReadChar();
            return new String(chars);
        }
        public static void writeUTF8(BinaryWriter OutputData, String str)
        {
            OutputData.Write(IPAddress.HostToNetworkOrder((short)str.Length));
            BinaryWriter od = new BinaryWriter(OutputData.BaseStream, System.Text.Encoding.UTF8);
            char[] chars = str.ToCharArray();
            for (int i = 0; i < str.Length; i++)
            {
                od.Write(chars[i]);
            }
        }       
        public static void handleColors(string text, string Style)
        {
            if (!text.Contains("§"))
            {
                putsc(text, Color.White, Style);
                return;
            }

            int count = CharCount(text, "§");

            putsc("", Color.Aqua);
            //---
            while (count != 0)
            {
                if (text.StartsWith("§") == false && count > 0)
                {
                    string temp = text.Substring(0, text.IndexOf("§"));
                    text = text.Substring(text.IndexOf("§"), text.Length - text.IndexOf("§"));
                    putsc(temp, Color.White, Style);
                }
                else
                {
                    string code = text.Substring(text.IndexOf("§") + 1, 1);
                    code = code.ToLower();

                    if (count == 1)
                    {
                        string temp = text.Substring(text.IndexOf("§") + 2, text.Length - text.IndexOf("§") - 2);

                        switch (code)
                        {
                            case "0":
                                putsc(temp, Color.FromArgb(0, 0, 0), Style);
                                break;
                            case "1":
                                putsc(temp, Color.FromArgb(0, 0, 170), Style);
                                break;
                            case "2":
                                putsc(temp, Color.FromArgb(0, 170, 0), Style);
                                break;
                            case "3":
                                putsc(temp, Color.FromArgb(0, 170, 170),Style);
                                break;
                            case "4":
                                putsc(temp, Color.FromArgb(170, 0, 0), Style);
                                break;
                            case "5":
                                putsc(temp, Color.FromArgb(170, 0, 170), Style);
                                break;
                            case "6":
                                putsc(temp, Color.FromArgb(255, 170, 0), Style);
                                break;
                            case "7":
                                putsc(temp, Color.FromArgb(170, 170, 170), Style);
                                break;
                            case "8":
                                putsc(temp, Color.FromArgb(85, 85, 85), Style);
                                break;
                            case "9":
                                putsc(temp, Color.FromArgb(85, 85, 255), Style);
                                break;
                            case "a":
                                putsc(temp, Color.FromArgb(85, 255, 85), Style);
                                break;
                            case "b":
                                putsc(temp, Color.FromArgb(85, 255, 255), Style);
                                break;
                            case "c":
                                putsc(temp, Color.FromArgb(255, 85, 85), Style);
                                break;
                            case "d":
                                putsc(temp, Color.FromArgb(255, 85, 255), Style);
                                break;
                            case "e":
                                putsc(temp, Color.FromArgb(255, 255, 85), Style);
                                break;
                            case "f":
                                putsc(temp, Color.FromArgb(255, 255, 255), Style);
                                break;
                        }

                        count--;
                    }
                    else
                    {
                        string temp = text.Substring(text.IndexOf("§") + 2, text.Length - text.IndexOf("§") - 2);
                        string temp2;

                        if (temp.Contains("§"))
                            temp2 = temp.Substring(0, temp.IndexOf("§"));
                        else
                            temp2 = temp;

                        if (temp != "")
                            if (temp2 != "")
                                temp = temp.Substring(temp2.Length, (temp.Length - temp2.Length));
                        switch (code)
                        {
                            case "0":
                                putsc(temp2, Color.FromArgb(0, 0, 0), Style);
                                break;
                            case "1":
                                putsc(temp2, Color.FromArgb(0, 0, 170), Style);
                                break;
                            case "2":
                                putsc(temp2, Color.FromArgb(0, 170, 0), Style);
                                break;
                            case "3":
                                putsc(temp2, Color.FromArgb(0, 170, 170), Style);
                                break;
                            case "4":
                                putsc(temp2, Color.FromArgb(170, 0, 0), Style);
                                break;
                            case "5":
                                putsc(temp2, Color.FromArgb(170, 0, 170), Style);
                                break;
                            case "6":
                                putsc(temp2, Color.FromArgb(255, 170, 0), Style);
                                break;
                            case "7":
                                putsc(temp2, Color.FromArgb(170, 170, 170), Style);
                                break;
                            case "8":
                                putsc(temp2, Color.FromArgb(85, 85, 85), Style);
                                break;
                            case "9":
                                putsc(temp2, Color.FromArgb(85, 85, 255), Style);
                                break;
                            case "a":
                                putsc(temp2, Color.FromArgb(85, 255, 85), Style);
                                break;
                            case "b":
                                putsc(temp2, Color.FromArgb(85, 255, 255), Style);
                                break;
                            case "c":
                                putsc(temp2, Color.FromArgb(255, 85, 85), Style);
                                break;
                            case "d":
                                putsc(temp2, Color.FromArgb(255, 85, 255), Style);
                                break;
                            case "e":
                                putsc(temp2, Color.FromArgb(255, 255, 85), Style);
                                break;
                            case "f":
                                putsc(temp2, Color.FromArgb(255, 255, 255), Style);
                                break;
                            default:
                                putsc(temp2, Color.FromArgb(255, 255, 255), Style);
                                break;
                        }
                        count--;
                        if (temp.Contains("§"))
                        {
                            text = temp;
                        }
                    }
                }

            }
        }
        public static void putsc(String text, Color color, String style = "")
        {
            if (form != null)
            {
                ConsoleLine line;
                line.text = text;
                line.color = color;
                line.font = style;
                form.ConsoleWriteFormated(line);
            }
        }        
        public static int CharCount(string Origstring, string Chars)
        {
            int Count = 0;

            for (int i = 0; i < Origstring.Length - 1; i++)
            {
                if (Origstring.Substring(i, 1) == Chars)
                    Count++;
            }

            return Count;
        }
    }
}
