﻿/*
    WeDoALittleTrolling is a Terraria Mod made with tModLoader.
    Copyright (C) 2022-2025 LukasV-Coding

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.IO;
using System.IO;
using Microsoft.CodeAnalysis;
using System.Security.Policy;
using WeDoALittleBalancing.Content.NPCs;

namespace WeDoALittleBalancing.Common.Utilities
{
    internal class WDALBProjectileUtil : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public readonly int extraUpdatesIceBoomerang = 1;
        //If you have perfect code this shouldn't be necessary, however, errare humanum est.
        //I just don't want another catastrophic crash bug like on 2023/06/28!
        public readonly int maxSourceDetectionRecursions = 8;
        public int currentSourceDetectionRecursion = 0;
        public bool recentlyPassedIceBoomerangSoundDelay = false;
        public IEntitySource spawnSource;
        public Entity parentEntity;
        public bool parentEntityExists;
        public NPC parentNPC;
        public bool parentNPCExists;
        public Player parentPlayer;
        public bool parentPlayerExists;
        /*
            @var parentHeldItem is client side only
        */
        public Item parentHeldItem;
        /*
            @var parentHeldItemExists is client side only
        */
        public bool parentHeldItemExists;
        public Vector2 spawnCenter;
        public long ticksAlive;

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            //SPAWN SOURCE IS CLIENT SIDE ONLY!!!!!!
            spawnCenter = projectile.Center;
            ticksAlive = 0;
            projectile.netUpdate = true;
            spawnSource = source;
            if(source is EntitySource_Parent parentSource)
            {
                parentEntity = parentSource.Entity;
                parentEntityExists = true;
            }
            else
            {
                parentEntity = null;
                parentEntityExists = false;
            }
            if(TryGetParentEntity(out Entity entity))
            {
                currentSourceDetectionRecursion = 0;
                ProcessParentSource(entity);
            }
            if(TryGetParentNPC(out NPC npc) && Main.netMode != NetmodeID.MultiplayerClient)
            {
                GlobalNPCs.OnSpawnProjectile(npc, projectile);
            }
            projectile.netUpdate = true;
            base.OnSpawn(projectile, source);
        }

        public override void SendExtraAI(Projectile projectile, BitWriter bitWriter, BinaryWriter binaryWriter)
        {
            binaryWriter.Write(projectile.scale);
            binaryWriter.Write(projectile.damage);
            if(parentNPCExists)
            {
                binaryWriter.Write(parentNPCExists);
                binaryWriter.Write(parentNPC.whoAmI);
            }
            else
            {
                binaryWriter.Write(parentNPCExists);
                binaryWriter.Write((int)-1);
            }
            if(parentPlayerExists)
            {
                binaryWriter.Write(parentPlayerExists);
                binaryWriter.Write(parentPlayer.whoAmI);
            }
            else
            {
                binaryWriter.Write(parentPlayerExists);
                binaryWriter.Write((int)-1);
            }
        }

        public override void ReceiveExtraAI(Projectile projectile, BitReader bitReader, BinaryReader binaryReader)
        {
            projectile.scale = binaryReader.ReadSingle();
            projectile.damage = binaryReader.ReadInt32();
            parentNPCExists = binaryReader.ReadBoolean();
            int parentNPCIndex = binaryReader.ReadInt32();
            if(parentNPCIndex > -1 && parentNPCIndex < Main.npc.Length)
            {
                parentNPC = Main.npc[parentNPCIndex];
            }
            else
            {
                parentNPC = null;
            }
            parentPlayerExists = binaryReader.ReadBoolean();
            int parentPlayerIndex = binaryReader.ReadInt32();
            if(parentPlayerIndex > -1 && parentPlayerIndex < Main.player.Length)
            {
                parentPlayer = Main.player[parentPlayerIndex];
            }
            else
            {
                parentPlayer = null;
            }
        }

        public override bool PreAI(Projectile projectile)
        {
            ticksAlive += 1;
            return base.PreAI(projectile);
        }

        
        public void ProcessParentSource(Entity entity)
        {
            //SPAWN SOURCE IS CLIENT SIDE ONLY!!!!!!
            if (entity is NPC parentEntity_npc)
            {
                parentNPC = parentEntity_npc;
                parentNPCExists = true;
            }
            else
            {
                parentNPC = null;
                parentNPCExists = false;
            }
            if (entity is Player parentEntity_player)
            {
                parentPlayer = parentEntity_player;
                parentPlayerExists = true;
                if (parentEntity_player.HeldItem != null)
                {
                    parentHeldItemExists = true;
                    parentHeldItem = parentEntity_player.HeldItem;
                }
                else
                {
                    parentHeldItem = null;
                    parentHeldItemExists = false;
                }
            }
            else
            {
                parentPlayer = null;
                parentPlayerExists = false;
                parentHeldItem = null;
                parentHeldItemExists = false;
            }
            if (entity is Projectile parentEntity_projectile)
            {
                if(parentEntity_projectile.GetGlobalProjectile<WDALBProjectileUtil>().TryGetParentEntity(out Entity newEntity))
                {
                    if(newEntity != entity)
                    {
                        currentSourceDetectionRecursion++;
                        if(currentSourceDetectionRecursion <= maxSourceDetectionRecursions)
                        {
                            ProcessParentSource(newEntity);
                        }
                    }
                }
            }
        }

        public bool HasRecentlyPassedSoundDelay(int projectileID)
        {
            if(projectileID == ProjectileID.IceBoomerang)
            {
                if(!recentlyPassedIceBoomerangSoundDelay)
                {
                    recentlyPassedIceBoomerangSoundDelay = true;
                    return false;
                }
                if(recentlyPassedIceBoomerangSoundDelay)
                {
                    recentlyPassedIceBoomerangSoundDelay = false;
                    return true;
                }
            }
            return false;
        }

        public bool TryGetParentEntity(out Entity entity)
        {
            entity = parentEntity;
            if(parentEntityExists)
            {
                return true;
            }
            return false;
        }

        public bool TryGetParentNPC(out NPC npc)
        {
            npc = parentNPC;
            if(parentNPCExists)
            {
                return true;
            }
            return false;
        }

        public bool TryGetParentPlayer(out Player player)
        {
            player = parentPlayer;
            if(parentPlayerExists)
            {
                return true;
            }
            return false;
        }

        public bool TryGetParentHeldItem(out Item item)
        {
            item = parentHeldItem;
            if(parentHeldItemExists)
            {
                return true;
            }
            return false;
        }
    }
}
