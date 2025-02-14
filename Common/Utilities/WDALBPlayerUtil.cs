/*
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

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using Terraria;
using Terraria.Utilities;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using WeDoALittleBalancing.Content.Items;
using Terraria.ModLoader.IO;
//using WeDoALittleBalancing.Content.Buffs;

namespace WeDoALittleBalancing.Common.Utilities
{
    internal class WDALBPlayerUtil : ModPlayer
    {
        public Player player;
        public int wreckedResistanceStack;
        public int vulnerableStack;
        public bool syncDevastated;
        public int statLifeDevastated;
        public static UnifiedRandom random = new UnifiedRandom();
        public long currentTick;
        
        public override void Initialize()
        {
            player = this.Player;
            wreckedResistanceStack = 0;
            vulnerableStack = 0;
            syncDevastated = false;
            statLifeDevastated = player.statLifeMax2;
            currentTick = 0;
        }
/*
        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("WDALTDevastatedStack"))
            {
                statLifeDevastated = tag.GetInt("WDALTDevastatedStack");
            }
            if (tag.ContainsKey("WDALTWreckedResistanceStack"))
            {
                wreckedResistanceStack = tag.GetInt("WDALTWreckedResistanceStack");
            }
            if (tag.ContainsKey("WDALTVulnerableStack"))
            {
                vulnerableStack = tag.GetInt("WDALTVulnerableStack");
            }
        }

        public override void SaveData(TagCompound tag)
        {
            if (player.HasBuff(ModContent.BuffType<Devastated>()))
            {
                tag["WDALTDevastatedStack"] = statLifeDevastated;
            }
            if (wreckedResistanceStack > 0)
            {
                tag["WDALTWreckedResistanceStack"] = wreckedResistanceStack;
            }
            if (vulnerableStack > 0)
            {
                tag["WDALTVulnerableStack"] = vulnerableStack;
            }
        }
*/
        private void ResetVariables()
        {
            wreckedResistanceStack = 0;
            vulnerableStack = 0;
            syncDevastated = false;
            statLifeDevastated = player.statLifeMax2;
        }

        public override void UpdateDead()
        {
            ResetVariables();
        }

        public override void PreUpdate()
        {
            currentTick++;
        }

        public override void PostUpdate()
        {
            GlobalItemList.ModifySetBonus(player);
        }
/*
        public override void PostUpdateEquips()
        {
            if (player.HasBuff(ModContent.BuffType<WreckedResistance>()))
            {
                float modifierWR = (float)(90 - (wreckedResistanceStack * 10)) * 0.01f;
                player.DefenseEffectiveness *= modifierWR;
            }
            else
            {
                wreckedResistanceStack = 0;
            }
            if (player.HasBuff(ModContent.BuffType<Vulnerable>()))
            {
                float modifierV = (float)(90 - (vulnerableStack * 10)) * 0.01f;
                player.endurance *= modifierV;
            }
            else
            {
                vulnerableStack = 0;
            }
            if (player.HasBuff(ModContent.BuffType<Devastated>()))
            {
                if (syncDevastated && player.statLife < player.statLifeMax2 && player.statLife > 0)
                {
                    statLifeDevastated = player.statLife;
                    syncDevastated = false;
                }
                player.statLifeMax2 = statLifeDevastated;
            }
            else
            {
                statLifeDevastated = player.statLifeMax2;
            }
        }

        public override bool ConsumableDodge(Player.HurtInfo info)
        {
            if (info.DamageSource.SourceProjectileType == ProjectileID.PhantasmalDeathray)
            {
                if (info.DamageSource.SourceProjectileLocalIndex >= 0 && info.DamageSource.SourceProjectileLocalIndex < Main.projectile.Length)
                {
                    if (Main.projectile[info.DamageSource.SourceProjectileLocalIndex].GetGlobalProjectile<WDALBProjectileUtil>().TryGetParentNPC(out NPC npc))
                    {
                        if (npc.type == NPCID.MoonLordHead && Main.masterMode)
                        {
                            Devastated.DisintegratePlayer(player);
                            return true;
                        }
                    }
                }
            }
            return base.ConsumableDodge(info);
        }

        public override bool FreeDodge(Player.HurtInfo info)
        {
            if (info.DamageSource.SourceProjectileType == ProjectileID.PhantasmalDeathray)
            {
                if (info.DamageSource.SourceProjectileLocalIndex >= 0 && info.DamageSource.SourceProjectileLocalIndex < Main.projectile.Length)
                {
                    if (Main.projectile[info.DamageSource.SourceProjectileLocalIndex].GetGlobalProjectile<WDALBProjectileUtil>().TryGetParentNPC(out NPC npc))
                    {
                        if (npc.type == NPCID.MoonLordHead && Main.masterMode)
                        {
                            Devastated.DisintegratePlayer(player);
                            return true;
                        }
                    }
                }
            }
            return base.FreeDodge(info);
        }

        public override void UpdateLifeRegen()
        {
            player.buffImmune[ModContent.BuffType<WreckedResistance>()] = false;
            player.buffImmune[ModContent.BuffType<Vulnerable>()] = false;
            player.buffImmune[ModContent.BuffType<Devastated>()] = false;
            base.UpdateLifeRegen();
        }
*/
        public static bool IsBossActive()
        {
            for(int i = 0;i < Main.npc.Length; i++)
            {
                if
                (
                    Main.npc[i].active &&
                    (
                        Main.npc[i].boss ||
                        Main.npc[i].type == NPCID.EaterofWorldsHead ||
                        Main.npc[i].type == NPCID.EaterofWorldsBody ||
                        Main.npc[i].type == NPCID.EaterofWorldsTail
                    )
                )
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsBehindHousingWall()
        {
            int posX = (int)(player.position.X + (float)(player.width / 2)) / 16;
            int posY = (int)(player.position.Y + (float)(player.height / 2)) / 16;
            if (Main.wallHouse[Main.tile[posX, posY].WallType])
            {
                return true;
            }
            return false;
        }

        public bool HasPlayerAcessoryEquipped(int itemID)
        {
            int offset = 3;
            int loopLimit = 5;
            loopLimit += player.extraAccessorySlots;
            if(Main.masterMode)
            {
                loopLimit++;
            }
            for(int i = offset;i < (offset + loopLimit); i++) //Search through all accessory slots
            {
                if(player.armor[i].type == itemID)
                {
                    return true;
                }
            }
            return false;
        }

        public int GetAmountOfEquippedAccessoriesWithPrefixFromPlayer(int prefixID) //Fancy name much smart, haha :P
        {
            int equippedAmount = 0;
            int offset = 3;
            int loopLimit = 5;
            loopLimit += player.extraAccessorySlots;
            if(Main.masterMode)
            {
                loopLimit++;
            }
            for(int i = offset;i < (offset + loopLimit); i++) //Search through all accessory slots
            {
                if(player.armor[i].prefix == prefixID)
                {
                    equippedAmount++;
                }
            }
            return equippedAmount;
        }

        public bool HasPlayerHelmetEquipped(int itemID)
        {
            int offset = 0;
            if(player.armor[offset].type == itemID)
            {
                return true;
            }
            return false;
        }

        public bool HasPlayerChestplateEquipped(int itemID)
        {
            int offset = 1;
            if(player.armor[offset].type == itemID)
            {
                return true;
            }
            return false;
        }

        public bool HasPlayerLeggingsEquipped(int itemID)
        {
            int offset = 2;
            if(player.armor[offset].type == itemID)
            {
                return true;
            }
            return false;
        }
    }
}
