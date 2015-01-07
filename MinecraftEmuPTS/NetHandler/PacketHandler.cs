using MinecraftEmuPTS.Encription;
using MinecraftEmuPTS.GameInfo;
using MinecraftEmuPTS.Packets;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace MinecraftEmuPTS.NetHandler
{
    class PacketHandler
    {
        ConnectionManager manager;
        public PacketHandler(ConnectionManager m_)
        {
            this.manager = m_;
        }

        public void HandlePacketKeepAlive(PacketKeepAlive packet)
        {
            manager.AddToSendingQueue(packet);
        }

        public void HandlePacketChat(PacketChat packet)
        {
            if (manager.HandleChatMessage)
            {
                String Message = packet.message;
                string name = "", message = "", type = "", color = "", style = "";
                var root = JObject.Parse(Message);

                foreach (KeyValuePair<String, JToken> app in root)
                {
                    var appName = app.Key;

                    switch (appName)
                    {
                        case "text":
                            message = app.Value.ToString();
                            if (message.Contains(" "))
                                name = CustomLib.strip_codes(message.Substring(0, message.IndexOf(" ")).Replace("<", "").Replace(">", "").Replace(":", "").Replace(" ", "").Replace("[", "").Replace("]", ""));
                            type = "chat";
                            break;
                        case "translate":
                            type = app.Value.ToString();
                            break;
                        case "using":
                            name = app.Value.First.ToString().Replace(" ", "");
                            message = app.Value.Last.ToString().Replace(" ", "");
                            break;
                        case "color":
                            color = app.Value.ToString();
                            break;
                        case "italic":
                            if (app.Value.ToString() == "true")
                                style = "italic";
                            break;
                    }
                    if (type == "chat.type.admin")
                    {
                        if (app.Value.HasValues == true)
                        {
                            name = app.Value[0].ToString();
                            JObject thisObj = JObject.Parse(app.Value[1].ToString());

                            foreach (KeyValuePair<String, JToken> part in thisObj)
                            {
                                var topName = part.Key;
                                switch (topName)
                                {
                                    case "translate":
                                        type = part.Value.ToString();
                                        break;
                                    case "using":
                                        message = part.Value.First.ToString();
                                        name = part.Value.Last.ToString();
                                        break;
                                }
                            }
                        }
                    }
                }

                // Now with the JSON Parsed, we have to add some special message cases.
                switch (type)
                {
                    case "multiplayer.player.joined":
                        message = name + " присоединился к серверу";
                        break;
                    case "multiplayer.player.left":
                        message = name + " покинул сервер";
                        break;
                    case "death.attack.outOfWorld":
                        message = name + " выпал за край мира";
                        break;
                    case "death.attack.explosion.player":
                        message = name + " был взорван " + message;
                        break;
                    case "death.attack.mob":
                        message = name + " был убит " + message;
                        break;
                    case "death.attack.arrow":
                        message = name + " был застрелен " + message;
                        break;
                    case "death.attack.player":
                        message = name + " был убит " + message;
                        break;
                    case "death.fell.killer":
                        message = name + " был обречен на падение " + message;
                        break;
                    case "death.attack.drown":
                        message = name + " задохнулся убегая от " + message;
                        break;
                    case "death.attack.lava":
                        message = name + " попытался поплавать в лаве";
                        break;
                    case "death.attack.fall":
                        message = name + " ударился об землю слишком сильно";
                        break;
                    case "death.attack.radiation":
                        message = name + " умер от радиации";
                        break;
                    case "death.fell.accident.generic":
                        message = name + " разбился насмерть";
                        break;
                    case "death.attack.starve":
                        message = name + " умер от голода";
                        break;
                    case "chat.type.text":
                        message = name + ": " + message;
                        //commandHandler ch = new commandHandler(socket, mainform, name, message);
                        break;
                    case "chat":
                        //commandHandler h = new commandHandler(socket, mainform, name, message);
                        break;
                    case "chat.type.emote":
                        message = "§d" + name + " " + message;
                        break;
                    case "chat.type.announcement":
                        message = "§d[" + name + "]:§f " + message;
                        break;
                    case "commands.tp.success":
                        message = name + " teleported to " + message;
                        break;
                    case "commands.op.success":
                        message = name + " was promoted to Op";
                        break;
                    default:
                        message = Message;
                        break;
                }
                if (color != "")
                    message = CustomLib.convertCode(message, color);
                CustomLib.handleColors(message, style);
                CustomLib.putsc("\r", Color.Aquamarine);
            }
        }

        public void HandlePacketUpdateHealth(PacketUpdateHealth packet)
        {
            manager.player.health = packet.healthMP;
            manager.player.hunger = packet.food;
            manager.player.sat = packet.foodSaturation;
            if (manager.player.health <= 0)
                manager.RaisePlayerDeathEvent();
        }

        public void HandlePacketPlayerLookMove(PacketPlayerLookMove packet)
        {
            manager.player.x = packet.xPosition;
            manager.player.yHead = packet.yPosition;
            manager.player.yFeet = packet.stance;
            manager.player.z = packet.zPosition;
            manager.player.pitch = packet.pitch;
            manager.player.yaw = packet.yaw;
            manager.player.onground = packet.onGround;
           
            manager.AddToSendingQueue(packet);
            if (!manager.InGame)
            {
                manager.RaiseConnectEvent();
                manager.InGame = true;
            }
        }

        public void HandlePacketBlockItemSwitch(PacketBlockItemSwitch packet)
        {
            manager.player.HeldSlot = packet.id;
        }

        public void HandlePacketNamedEntitySpawn(PacketNamedEntitySpawn packet)
        {
            EntityPlayer player = new EntityPlayer();
            player.name = packet.name;
            player.x = (double)packet.xPosition / 32.0;
            player.yFeet = (double)packet.yPosition / 32.0;
            player.z = (double)packet.zPosition / 32.0;
            player.yaw = (float)packet.rotation / 32.0f;
            player.pitch = (float)packet.pitch / 32.0f;
            if(!manager.PlayerList.ContainsKey(packet.entityId))
                manager.PlayerList.Add(packet.entityId, player);                       
        }

        public void HandlePacketDestroyEntity(PacketDestroyEntity packet)
        {
            int[] array = packet.entityId;
            EntityPlayer testplayer;            
            for (int i = 0; i < array.Length; i++)
            {

                if (manager.PlayerList.TryGetValue(array[i], out testplayer))
                {
                    manager.PlayerList.Remove(array[i]);                   
                }
            }            
        }

        public void HandlePacketRelEntityMove(PacketRelEntityMove packet)
        {
            EntityPlayer player;
            if (manager.PlayerList.TryGetValue(packet.entityId, out player))
            {
                player.name = manager.PlayerList[packet.entityId].name;
                player.x += (double)((SByte)packet.xPosition / 32.0);
                player.yFeet += (double)((SByte)packet.yPosition / 32.0);
                player.z += (double)((SByte)packet.zPosition / 32.0);
            }
        }

        public void HandlePacketRelEntityMoveLook(PacketRelEntityMoveLook packet)
        {
            EntityPlayer player;
            if (manager.PlayerList.TryGetValue(packet.entityId, out player))
            {
                player.name = manager.PlayerList[packet.entityId].name;
                player.x += (double)((SByte)packet.xPosition / 32.0);
                player.yFeet += (double)((SByte)packet.yPosition / 32.0);
                player.z += (double)((SByte)packet.zPosition / 32.0);
            }
        }

        public void HandlePacketEntityTeleport(PacketEntityTeleport packet)
        {
            EntityPlayer player;
            if (manager.PlayerList.TryGetValue(packet.entityId, out player))
            {
                player.name = manager.PlayerList[packet.entityId].name;
                player.x = (double)packet.xPosition / 32.0;
                player.yFeet = (double)packet.yPosition / 32.0;
                player.z = (double)packet.zPosition / 32.0;
            }
        }

        public void HandlePacketSetSlot(PacketSetSlot packet)
        {
            if (packet.windowId == 0)
            {
                manager.player.Inventory[packet.itemSlot] = packet.myItemStack;
            }
        }

        public void HandlePacketCustomPayload(PacketCustomPayload packet)
        {            
            if (packet.channel.Equals("FML"))
            {
                BinaryReader br = new BinaryReader(new MemoryStream(packet.data));
                int Type = br.ReadByte();              
                if (Type == 0)
                {
                    List<String> missing = new List<string>();
                    BinaryWriter bwr = new BinaryWriter(new MemoryStream());
                    int NOM = IPAddress.NetworkToHostOrder(br.ReadInt32());
                    bwr.Write((byte)1);

                    String[] modsid = new String[NOM];
                    Console.WriteLine("Server mods: ");

                    List<String> ClientMod = new List<String>();
                    for (int i = 0; i < NOM; i++)
                    {
                        modsid[i] = CustomLib.readUTF8(br);
                        Console.WriteLine(modsid[i]);
                        if (!ModList.ModMap.ContainsKey(modsid[i]))
                        {
                            missing.Add(modsid[i]);
                        }
                        else
                        {
                            ClientMod.Add(modsid[i]);
                        }

                    }
                    bwr.Write(IPAddress.HostToNetworkOrder(ClientMod.Count));
                    for (int i = 0; i < ClientMod.Count; i++)
                    {
                        CustomLib.writeUTF8(bwr, ClientMod[i]);
                        CustomLib.writeUTF8(bwr, ModList.ModMap[ClientMod[i]]);
                    }
                    bwr.Write(IPAddress.HostToNetworkOrder(missing.Count));
                    for (int i = 0; i < missing.Count; ++i)
                    {
                        CustomLib.writeUTF8(bwr, missing[i]);
                    }
                    byte[] buff = ((MemoryStream)bwr.BaseStream).GetBuffer();
                    int l = (int)bwr.BaseStream.Position;
                    byte[] send = new byte[l];
                    for (int i = 0; i < l; i++)
                        send[i] = buff[i];
                    manager.AddToSendingQueue(new PacketCustomPayload("FML", send));
                }
            }
        }

        public void HandlePacketServerAuthData(PacketServerAuthData packet)
        {

            if ("-".Equals(packet.getServerID()))
            {
                if (manager.FML)
                {
                    manager.AddToSendingQueue(CustomLib.FakeFML());
                }
                manager.AddToSendingQueue(new PacketClientCommand(0));
                Console.WriteLine("No auth required");
                return;
            }

            Console.WriteLine("ServerID: " + packet.getServerID());
            CryptManager.DecodePublicKey(packet.GetPublicKey());

            byte[] SharedKey = CryptManager.CreateSecretKey();
            byte[] SharedSecret = CryptManager.EncryptData(SharedKey);
            byte[] VerifyToken = CryptManager.EncryptData(packet.GetVerifyToken());


            String ServerHash = CryptManager.GetServerIdHash(packet.getServerID());
            Console.WriteLine("Server hash: " + ServerHash);
            string AuthToken = CustomLib.GetAuthToken();
            String AuthResponse = CustomLib.POSTurl(AuthToken + ServerHash);
            if (AuthResponse.Equals("OK"))
            {
                manager.AddToSendingQueue(new PacketSharedKey(SharedSecret, VerifyToken));
                Thread.Sleep(100);
                if (manager.FML)
                {
                    manager.AddToSendingQueue(CustomLib.FakeFML());
                }
                manager.AddToSendingQueue(new PacketClientCommand(0));
                Console.WriteLine("Client auth sucseed!");
                return;
            }
            Console.WriteLine("Client auth failed!");
            manager.Connected = false;
        }

        public void HandlePacketDisconnect(PacketDisconnect packet)
        {
            Console.WriteLine("Disconnected by server! Reason: " + packet.getReason());
            CustomLib.putsc("Disconnected by server! Reason: " + packet.getReason() + "\n", Color.Aquamarine);
            manager.Connected = false;
        }
    }
}
