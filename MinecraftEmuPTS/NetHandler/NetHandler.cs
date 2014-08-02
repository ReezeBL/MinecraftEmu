using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MinecraftEmuPTS.Packets;

namespace MinecraftEmuPTS.NetHandler
{
    class INetHandler
    {
        /**
     * determine if it is a server handler
     */

        /**
         * Handle Packet51MapChunk (full chunk update of blocks, metadata, light levels, and optionally biome data)
         */
        public void handleMapChunk(PacketChunkData par1Packet51MapChunk) { }

        /**
         * Default handler called for packets that don't have their own handlers in NetClientHandler; currentlly does
         * nothing.
         */
        public void unexpectedPacket(Packet par1Packet) { }

        public void handleErrorMessage(String par1Str, Object[] par2ArrayOfObj) { }

        public void handleKickDisconnect(PacketDisconnect par1Packet255KickDisconnect)
        {
            this.unexpectedPacket(par1Packet255KickDisconnect);
        }

        public void handleLogin(PacketLogin par1Packet1Login)
        {
            this.unexpectedPacket(par1Packet1Login);
        }

        public void handleFlying(PacketFlying par1Packet10Flying)
        {
            this.unexpectedPacket(par1Packet10Flying);
        }

        public void handleMultiBlockChange(PacketMultiBlockChange par1Packet52MultiBlockChange)
        {
            this.unexpectedPacket(par1Packet52MultiBlockChange);
        }

        public void handleBlockDig(PacketBlockDig par1Packet14BlockDig)
        {
            this.unexpectedPacket(par1Packet14BlockDig);
        }

        public void handleBlockChange(PacketBlockChange par1Packet53BlockChange)
        {
            this.unexpectedPacket(par1Packet53BlockChange);
        }

        public void handleNamedEntitySpawn(PacketNamedEntitySpawn par1Packet20NamedEntitySpawn)
        {
            this.unexpectedPacket(par1Packet20NamedEntitySpawn);
        }

        public void handleEntity(PacketEntity par1Packet30Entity)
        {
            this.unexpectedPacket(par1Packet30Entity);
        }

        public void handleEntityTeleport(PacketEntityTeleport par1Packet34EntityTeleport)
        {
            this.unexpectedPacket(par1Packet34EntityTeleport);
        }

        public void handlePlace(PacketPlace par1Packet15Place)
        {
            this.unexpectedPacket(par1Packet15Place);
        }

        public void handleBlockItemSwitch(PacketBlockItemSwitch par1Packet16BlockItemSwitch)
        {
            this.unexpectedPacket(par1Packet16BlockItemSwitch);
        }

        public void handleDestroyEntity(PacketDestroyEntity par1Packet29DestroyEntity)
        {
            this.unexpectedPacket(par1Packet29DestroyEntity);
        }

        public void handleCollect(PacketCollect par1Packet22Collect)
        {
            this.unexpectedPacket(par1Packet22Collect);
        }

        public void handleChat(PacketChat par1Packet3Chat)
        {
            this.unexpectedPacket(par1Packet3Chat);
        }

        public void handleVehicleSpawn(PacketVehicleSpawn par1Packet23VehicleSpawn)
        {
            this.unexpectedPacket(par1Packet23VehicleSpawn);
        }

        public void handleAnimation(PacketAnimation par1Packet18Animation)
        {
            this.unexpectedPacket(par1Packet18Animation);
        }

        /**
         * runs registerPacket on the given Packet19EntityAction
         */
        public void handleEntityAction(PacketEntityAction par1Packet19EntityAction)
        {
            this.unexpectedPacket(par1Packet19EntityAction);
        }

        public void handleClientProtocol(PacketHandshake par1Packet2ClientProtocol)
        {
            this.unexpectedPacket(par1Packet2ClientProtocol);
        }

        public void handleServerAuthData(PacketServerAuthData par1Packet253ServerAuthData)
        {
            this.unexpectedPacket(par1Packet253ServerAuthData);
        }

        public void handleSharedKey(PacketSharedKey par1Packet252SharedKey)
        {
            this.unexpectedPacket(par1Packet252SharedKey);
        }

        public void handleMobSpawn(PacketMobSpawn par1Packet24MobSpawn)
        {
            this.unexpectedPacket(par1Packet24MobSpawn);
        }

        public void handleUpdateTime(PacketUpdateTime par1Packet4UpdateTime)
        {
            this.unexpectedPacket(par1Packet4UpdateTime);
        }

        public void handleSpawnPosition(PacketSpawnPosition par1Packet6SpawnPosition)
        {
            this.unexpectedPacket(par1Packet6SpawnPosition);
        }

        /**
         * Packet handler
         */
        public void handleEntityVelocity(PacketEntityVelocity par1Packet28EntityVelocity)
        {
            this.unexpectedPacket(par1Packet28EntityVelocity);
        }

        /**
         * Packet handler
         */
        public void handleEntityMetadata(PacketEntityMetadata par1Packet40EntityMetadata)
        {
            this.unexpectedPacket(par1Packet40EntityMetadata);
        }

        /**
         * Packet handler
         */
        public void handleAttachEntity(PacketAttachEntity par1Packet39AttachEntity)
        {
            this.unexpectedPacket(par1Packet39AttachEntity);
        }

        public void handleUseEntity(PacketUseEntity par1Packet7UseEntity)
        {
            this.unexpectedPacket(par1Packet7UseEntity);
        }

        /**
         * Packet handler
         */
        public void handleEntityStatus(PacketEntityStatus par1Packet38EntityStatus)
        {
            this.unexpectedPacket(par1Packet38EntityStatus);
        }

        /**
         * Recieves player health from the server and then proceeds to set it locally on the client.
         */
        public void handleUpdateHealth(PacketUpdateHealth par1Packet8UpdateHealth)
        {
            this.unexpectedPacket(par1Packet8UpdateHealth);
        }

        /**
         * respawns the player
         */
        public void handleRespawn(PacketRespawn par1Packet9Respawn)
        {
            this.unexpectedPacket(par1Packet9Respawn);
        }

        public void handleExplosion(PacketExplosion par1Packet60Explosion)
        {
            this.unexpectedPacket(par1Packet60Explosion);
        }

        public void handleOpenWindow(PacketOpenWindow par1Packet100OpenWindow)
        {
            this.unexpectedPacket(par1Packet100OpenWindow);
        }

        public void handleCloseWindow(PacketCloseWindow par1Packet101CloseWindow)
        {
            this.unexpectedPacket(par1Packet101CloseWindow);
        }

        public void handleWindowClick(PacketWindowClick par1Packet102WindowClick)
        {
            this.unexpectedPacket(par1Packet102WindowClick);
        }

        public void handleSetSlot(PacketSetSlot par1Packet103SetSlot)
        {
            this.unexpectedPacket(par1Packet103SetSlot);
        }

        public void handleWindowItems(PacketWindowItems par1Packet104WindowItems)
        {
            this.unexpectedPacket(par1Packet104WindowItems);
        }

        /**
         * Updates Client side signs
         */
        public void handleUpdateSign(PacketUpdateSign par1Packet130UpdateSign)
        {
            this.unexpectedPacket(par1Packet130UpdateSign);
        }

        public void handleUpdateProgressbar(PacketUpdateProgressbar par1Packet105UpdateProgressbar)
        {
            this.unexpectedPacket(par1Packet105UpdateProgressbar);
        }

        public void handlePlayerInventory(PacketPlayerInventory par1Packet5PlayerInventory)
        {
            this.unexpectedPacket(par1Packet5PlayerInventory);
        }

        public void handleTransaction(PacketTransaction par1Packet106Transaction)
        {
            this.unexpectedPacket(par1Packet106Transaction);
        }

        /**
         * Packet handler
         */
        public void handleEntityPainting(PacketEntityPainting par1Packet25EntityPainting)
        {
            this.unexpectedPacket(par1Packet25EntityPainting);
        }

        public void handleBlockEvent(PacketPlayNoteBlock par1Packet54PlayNoteBlock)
        {
            this.unexpectedPacket(par1Packet54PlayNoteBlock);
        }

        /**
         * Increment player statistics
         */
        public void handleStatistic(PacketStatistic par1Packet200Statistic)
        {
            this.unexpectedPacket(par1Packet200Statistic);
        }

        public void handleSleep(PacketSleep par1Packet17Sleep)
        {
            this.unexpectedPacket(par1Packet17Sleep);
        }

        public void func_110774_a(PacketPlayerInput par1Packet27PlayerInput)
        {
            this.unexpectedPacket(par1Packet27PlayerInput);
        }

        public void handleGameEvent(PacketGameEvent par1Packet70GameEvent)
        {
            this.unexpectedPacket(par1Packet70GameEvent);
        }

        /**
         * Handles weather packet
         */
        public void handleWeather(PacketWeather par1Packet71Weather)
        {
            this.unexpectedPacket(par1Packet71Weather);
        }

        /**
         * Contains logic for handling packets containing arbitrary unique item data. Currently this is only for maps.
         */
        public void handleMapData(PacketMapData par1Packet131MapData)
        {
            this.unexpectedPacket(par1Packet131MapData);
        }

        public void handleDoorChange(PacketDoorChange par1Packet61DoorChange)
        {
            this.unexpectedPacket(par1Packet61DoorChange);
        }

        /**
         * Handle a server ping packet.
         */
        public void handleServerPing(PacketServerPing par1Packet254ServerPing)
        {
            this.unexpectedPacket(par1Packet254ServerPing);
        }

        /**
         * Handle an entity effect packet.
         */
        public void handleEntityEffect(PacketEntityEffect par1Packet41EntityEffect)
        {
            this.unexpectedPacket(par1Packet41EntityEffect);
        }

        /**
         * Handle a remove entity effect packet.
         */
        public void handleRemoveEntityEffect(PacketRemoveEntityEffect par1Packet42RemoveEntityEffect)
        {
            this.unexpectedPacket(par1Packet42RemoveEntityEffect);
        }

        /**
         * Handle a player information packet.
         */
        public void handlePlayerInfo(PacketPlayerInfo par1Packet201PlayerInfo)
        {
            this.unexpectedPacket(par1Packet201PlayerInfo);
        }

        /**
         * Handle a keep alive packet.
         */
        public void handleKeepAlive(PacketKeepAlive par1Packet0KeepAlive)
        {
            this.unexpectedPacket(par1Packet0KeepAlive);
        }

        /**
         * Handle an experience packet.
         */
        public void handleExperience(PacketExperience par1Packet43Experience)
        {
            this.unexpectedPacket(par1Packet43Experience);
        }

        /**
         * Handle a creative slot packet.
         */
        public void handleCreativeSetSlot(PacketCreativeSetSlot par1Packet107CreativeSetSlot)
        {
            this.unexpectedPacket(par1Packet107CreativeSetSlot);
        }

        /**
         * Handle a entity experience orb packet.
         */
        public void handleEntityExpOrb(PacketEntityExpOrb par1Packet26EntityExpOrb)
        {
            this.unexpectedPacket(par1Packet26EntityExpOrb);
        }

        public void handleEnchantItem(PacketEnchantItem par1Packet108EnchantItem) { }

        public void handleCustomPayload(PacketCustomPayload par1Packet250CustomPayload) { }

        public void handleEntityHeadRotation(PacketEntityHeadRotation par1Packet35EntityHeadRotation)
        {
            this.unexpectedPacket(par1Packet35EntityHeadRotation);
        }

        public void handleTileEntityData(PacketTileEntityData par1Packet132TileEntityData)
        {
            this.unexpectedPacket(par1Packet132TileEntityData);
        }

        /**
         * Handle a player abilities packet.
         */
        public void handlePlayerAbilities(PacketPlayerAbilities par1Packet202PlayerAbilities)
        {
            this.unexpectedPacket(par1Packet202PlayerAbilities);
        }

        public void handleAutoComplete(PacketAutoComplete par1Packet203AutoComplete)
        {
            this.unexpectedPacket(par1Packet203AutoComplete);
        }

        public void handleClientInfo(PacketClientInfo par1Packet204ClientInfo)
        {
            this.unexpectedPacket(par1Packet204ClientInfo);
        }

        public void handleLevelSound(PacketLevelSound par1Packet62LevelSound)
        {
            this.unexpectedPacket(par1Packet62LevelSound);
        }

        public void handleBlockDestroy(PacketBlockDestroy par1Packet55BlockDestroy)
        {
            this.unexpectedPacket(par1Packet55BlockDestroy);
        }

        public void handleClientCommand(PacketClientCommand par1Packet205ClientCommand) { }

        public void handleMapChunks(PacketChunkData par1Packet56MapChunks)
        {
            this.unexpectedPacket(par1Packet56MapChunks);
        }

        /**
         * If this returns false, all packets will be queued for the main thread to handle, even if they would otherwise be
         * processed asynchronously. Used to avoid processing packets on the client before the world has been downloaded
         * (which happens on the main thread)
         */
        public bool canProcessPacketsAsync()
        {
            return false;
        }

        /**
         * Handle a set objective packet.
         */
        public void handleSetObjective(PacketSetObjective par1Packet206SetObjective)
        {
            this.unexpectedPacket(par1Packet206SetObjective);
        }

        /**
         * Handle a set score packet.
         */
        public void handleSetScore(PacketSetScore par1Packet207SetScore)
        {
            this.unexpectedPacket(par1Packet207SetScore);
        }

        /**
         * Handle a set display objective packet.
         */
        public void handleSetDisplayObjective(PacketSetDisplayObjective par1Packet208SetDisplayObjective)
        {
            this.unexpectedPacket(par1Packet208SetDisplayObjective);
        }

        /**
         * Handle a set player team packet.
         */
        public void handleSetPlayerTeam(PacketSetPlayerTeam par1Packet209SetPlayerTeam)
        {
            this.unexpectedPacket(par1Packet209SetPlayerTeam);
        }

        /**
         * Handle a world particles packet.
         */
        public void handleWorldParticles(PacketWorldParticles par1Packet63WorldParticles)
        {
            this.unexpectedPacket(par1Packet63WorldParticles);
        }

        public bool isConnectionClosed()
        {
            return false;
        }

        public virtual void handleVanilla250Packet(PacketCustomPayload payload){ }

    }
}
