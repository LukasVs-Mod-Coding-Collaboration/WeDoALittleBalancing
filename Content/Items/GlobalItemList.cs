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
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Creative;
using Terraria.GameContent.Prefixes;
using static Humanizer.In;
using static Terraria.ModLoader.PlayerDrawLayer;
using Terraria.Utilities;
using System.ComponentModel;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Items;
using WeDoALittleBalancing.Common.Utilities;
using WeDoALittleBalancing.Common.ModSystems;

namespace WeDoALittleBalancing.Content.Items
{
    internal class GlobalItemList : GlobalItem
    {
        public override bool InstancePerEntity => false;

        public static UnifiedRandom random = new UnifiedRandom();

        public static readonly int[] fishronWeapons =
        {
            ItemID.Tsunami,
            ItemID.TempestStaff,
            ItemID.RazorbladeTyphoon,
            ItemID.Flairon,
            ItemID.BubbleGun
        };

        public static readonly int[] empressWeapons =
        {
            ItemID.FairyQueenMagicItem,
            ItemID.PiercingStarlight,
            ItemID.RainbowWhip,
            ItemID.FairyQueenRangedItem,
            ItemID.SparkleGuitar,
            ItemID.EmpressBlade
        };

        public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if
            (
                item.type == ItemID.LeadAxe ||
                item.type == ItemID.LeadBroadsword ||
                item.type == ItemID.LeadHammer ||
                item.type == ItemID.LeadPickaxe
            )
            {
                target.AddBuff(BuffID.Poisoned, 1800, false);
            }
            base.OnHitNPC(item, player, target, hit, damageDone);
        }

        //Revert damage reduction from Spectre Hood
        public override void UpdateEquip(Item item, Player player)
        {
            if
            (
                item.type == ItemID.SpectreHood &&
                player.GetModPlayer<WDALBPlayerUtil>().HasPlayerChestplateEquipped(ItemID.SpectreRobe) &&
                player.GetModPlayer<WDALBPlayerUtil>().HasPlayerLeggingsEquipped(ItemID.SpectrePants)
            )
            {
                player.GetDamage(DamageClass.Magic) += (float)0.4;
            }
            if (item.type == ItemID.SpectreHood)
            {
                player.statManaMax2 += 60;
            }
            if
            (
                item.type == ItemID.MiningHelmet &&
                player.GetModPlayer<WDALBPlayerUtil>().HasPlayerChestplateEquipped(ItemID.MiningShirt) &&
                player.GetModPlayer<WDALBPlayerUtil>().HasPlayerLeggingsEquipped(ItemID.MiningPants)
            )
            {
                player.statDefense += 5;
            }
            if
            (
                item.type == ItemID.VortexHelmet &&
                player.GetModPlayer<WDALBPlayerUtil>().HasPlayerChestplateEquipped(ItemID.VortexBreastplate) &&
                player.GetModPlayer<WDALBPlayerUtil>().HasPlayerLeggingsEquipped(ItemID.VortexLeggings)
            )
            {
                player.GetArmorPenetration(DamageClass.Ranged) += (float)60.0;
            }
            if
            (
                item.type == ItemID.ChlorophyteMask ||
                item.type == ItemID.ChlorophyteHelmet ||
                item.type == ItemID.ChlorophyteHeadgear ||
                item.type == ItemID.ChlorophytePlateMail ||
                item.type == ItemID.ChlorophyteGreaves
            )
            {
                player.lifeRegen += 2;
            }
            base.UpdateEquip(item, player);
        }

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            if (item.prefix == PrefixID.Arcane)
            {
                player.statManaMax2 += 80;
            }
            if (item.prefix == PrefixID.Warding)
            {
                player.endurance += 0.01f;
            }
            if (item.prefix == PrefixID.Quick2)
            {
                player.GetAttackSpeed(DamageClass.Generic) += 0.04f;
            }
            if (item.prefix == PrefixID.Violent)
            {
                player.GetArmorPenetration(DamageClass.Melee) += 4f;
                player.GetArmorPenetration(DamageClass.MeleeNoSpeed) += 4f;
                player.GetArmorPenetration(DamageClass.SummonMeleeSpeed) += 4f;
            }
            if (item.type == ItemID.AvengerEmblem)
            {
                player.GetAttackSpeed(DamageClass.Generic) += 0.06f;
            }
            if (item.type == ItemID.SniperScope)
            {
                player.GetDamage(DamageClass.Ranged) += 0.02f;
            }
            if (item.type == ItemID.ReconScope)
            {
                player.GetDamage(DamageClass.Ranged) += 0.02f;
                player.GetCritChance(DamageClass.Ranged) += 2f;
            }
            if (item.type == ItemID.PygmyNecklace)
            {
                player.maxMinions += 1;
            }
            if (item.type == ItemID.AnkhShield)
            {
                player.statDefense += 4;
                player.DefenseEffectiveness *= 1.16f;
            }
            if (item.type == ItemID.WormScarf)
            {
                player.endurance += 0.03f;
            }
            base.UpdateAccessory(item, player, hideVisual);
        }

        public static void ModifySetBonus(Player player)
        {
            if
            (
                player.GetModPlayer<WDALBPlayerUtil>().HasPlayerHelmetEquipped(ItemID.SpectreHood) &&
                player.GetModPlayer<WDALBPlayerUtil>().HasPlayerChestplateEquipped(ItemID.SpectreRobe) &&
                player.GetModPlayer<WDALBPlayerUtil>().HasPlayerLeggingsEquipped(ItemID.SpectrePants)
            )
            {
                player.setBonus = "Generates 20% of magic damage as healing force\nMagic damage done to enemies heals the player with lowest health";
            }
            if
            (
                player.GetModPlayer<WDALBPlayerUtil>().HasPlayerHelmetEquipped(ItemID.VortexHelmet) &&
                player.GetModPlayer<WDALBPlayerUtil>().HasPlayerChestplateEquipped(ItemID.VortexBreastplate) &&
                player.GetModPlayer<WDALBPlayerUtil>().HasPlayerLeggingsEquipped(ItemID.VortexLeggings)
            )
            {
                player.setBonus += "\nIncreases ranged armor penetration by 60";
            }
            if
            (
                player.GetModPlayer<WDALBPlayerUtil>().HasPlayerHelmetEquipped(ItemID.BeetleHelmet) &&
                player.GetModPlayer<WDALBPlayerUtil>().HasPlayerChestplateEquipped(ItemID.BeetleShell) &&
                player.GetModPlayer<WDALBPlayerUtil>().HasPlayerLeggingsEquipped(ItemID.BeetleLeggings)
            )
            {
                player.setBonus += "\nThis is not affected by the Vulnerable debuff";
            }
            if
            (
                player.GetModPlayer<WDALBPlayerUtil>().HasPlayerHelmetEquipped(ItemID.SolarFlareHelmet) &&
                player.GetModPlayer<WDALBPlayerUtil>().HasPlayerChestplateEquipped(ItemID.SolarFlareBreastplate) &&
                player.GetModPlayer<WDALBPlayerUtil>().HasPlayerLeggingsEquipped(ItemID.SolarFlareLeggings)
            )
            {
                player.setBonus += "\nThis is not affected by the Vulnerable debuff";
            }
            if
            (
                player.GetModPlayer<WDALBPlayerUtil>().HasPlayerHelmetEquipped(ItemID.MiningHelmet) &&
                player.GetModPlayer<WDALBPlayerUtil>().HasPlayerChestplateEquipped(ItemID.MiningShirt) &&
                player.GetModPlayer<WDALBPlayerUtil>().HasPlayerLeggingsEquipped(ItemID.MiningPants)
            )
            {
                player.setBonus += "\n5 defense";
            }
        }

        //Adjust Tooltips accordingly
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.prefix == PrefixID.Arcane)
            {
                List<TooltipLine> infoLine = tooltips.FindAll(t => (t.Name == "PrefixAccMaxMana") && (t.Mod == "Terraria"));
                infoLine.ForEach(t => t.Text = "+100 mana");
            }
            if (item.prefix == PrefixID.Warding)
            {
                List<TooltipLine> infoLine = tooltips.FindAll(t => (t.Name == "PrefixAccDefense") && (t.Mod == "Terraria"));
                infoLine.ForEach(t => t.Text = "+4 defense\n+1% reduced damage taken");
            }
            if (item.prefix == PrefixID.Quick2)
            {
                List<TooltipLine> infoLine = tooltips.FindAll(t => (t.Name == "PrefixAccMoveSpeed") && (t.Mod == "Terraria"));
                infoLine.ForEach(t => t.Text = t.Text + "\n+4% attack speed");
            }
            if (item.prefix == PrefixID.Violent)
            {
                List<TooltipLine> infoLine = tooltips.FindAll(t => (t.Name == "PrefixAccMeleeSpeed") && (t.Mod == "Terraria"));
                infoLine.ForEach(t => t.Text = t.Text + "\n+4 melee armor penetration");
            }
            if (item.type == ItemID.SpectreHood)
            {
                TooltipLine extraManaLine = new TooltipLine(Mod, "PrefixAccMaxMana", "Increases maximum mana by 60");
                tooltips.Add(extraManaLine);
            }
            if (item.type == ItemID.ScytheWhip) //Dark Harvest
            {
                TooltipLine extraLoreLine = new TooltipLine(Mod, "Lore", "\"The harvest is bountiful this year\"");
                tooltips.Add(extraLoreLine);
            }
            if (item.type == ItemID.Bananarang)
            {
                TooltipLine extraLoreLine = new TooltipLine(Mod, "Lore", "\"Oh, these are some pretty cool bananas!\"");
                tooltips.Add(extraLoreLine);
            }
            if (item.type == ItemID.BoulderStatue)
            {
                TooltipLine extraLoreLine = new TooltipLine(Mod, "Lore", "\"We remember the Boulder.\"");
                tooltips.Add(extraLoreLine);
            }
            if (item.type == ItemID.ChlorophytePartisan)
            {
                TooltipLine extraManaLine = new TooltipLine(Mod, "WeaponLeechingDescription", "Recovers 5% of damage as health\nHeals up to 10 health per hit\nHeals less health the faster\nyou strike enemies\nHeals 75% less while immune");
                tooltips.Add(extraManaLine);
            }
            if (item.type == ItemID.ThunderStaff)
            {
                TooltipLine extraThunderspellLine = new TooltipLine(Mod, "WeaponLeechingDescription", "\"I CAST THUNDERSPELL!!\"");
                tooltips.Add(extraThunderspellLine);
            }
            if
            (
                item.type == ItemID.ChlorophyteMask ||
                item.type == ItemID.ChlorophyteHelmet ||
                item.type == ItemID.ChlorophyteHeadgear ||
                item.type == ItemID.ChlorophytePlateMail ||
                item.type == ItemID.ChlorophyteGreaves
            )
            {
                TooltipLine extraLifeRegenLine = new TooltipLine(Mod, "ArmorLifeRegenDescription", "Slowly regenerates life");
                tooltips.Add(extraLifeRegenLine);
            }
            if (item.type == ItemID.ChlorophyteShotbow)
            {
                TooltipLine extraManaLine = new TooltipLine(Mod, "WeaponArrowConversionDescription", "Converts wooden arrows into chlorophyte arrows");
                tooltips.Add(extraManaLine);
            }
            if (item.type == ItemID.MoltenFury)
            {
                TooltipLine extraManaLine = new TooltipLine(Mod, "WeaponArrowConversionDescription", "Converts wooden arrows into hellfire arrows");
                tooltips.Add(extraManaLine);
            }
            if (item.type == ItemID.DaedalusStormbow)
            {
                TooltipLine extraManaLine = new TooltipLine(Mod, "WeaponArrowConversionDescription", "Converts wooden arrows into holy arrows");
                tooltips.Add(extraManaLine);
            }
            if
            (
                item.type == ItemID.StaffoftheFrostHydra ||
                item.type == ItemID.PoisonStaff ||
                item.type == ItemID.VenomStaff ||
                item.type == ItemID.SkyFracture ||
                item.type == ItemID.MeteorStaff ||
                item.type == ItemID.InfernoFork ||
                item.type == ItemID.BlizzardStaff ||
                item.type == ItemID.FrostStaff ||
                item.type == ItemID.UnholyTrident ||
                item.type == ItemID.BookStaff ||
                item.type == ItemID.LunarFlareBook ||
                item.type == ItemID.BubbleGun
            )
            {
                TooltipLine extraCritChanceLine = new TooltipLine(Mod, "ProjectileHomingDescription", "Projectiles move towards the closest target");
                tooltips.Add(extraCritChanceLine);
            }
            if
            (
                item.type == ItemID.StaffoftheFrostHydra ||
                item.type == ItemID.QueenSpiderStaff ||
                item.type == ItemID.HoundiusShootius ||
                item.type == ItemID.RainbowCrystalStaff ||
                item.type == ItemID.MoonlordTurretStaff
            )
            {
                TooltipLine extraCritChanceLine = new TooltipLine(Mod, "ExtraCritChanceDescription", "Projectiles have a 15% chance to land a critical strike");
                tooltips.Add(extraCritChanceLine);
            }
            if
            (
                item.type == ItemID.RazorbladeTyphoon ||
                item.type == ItemID.BubbleGun
            )
            {
                TooltipLine extraCritChanceLine = new TooltipLine(Mod, "ExtraArmorPenetrationDescription", "Ignores 20 points of enemy Defense");
                tooltips.Add(extraCritChanceLine);
            }
            if
            (
                item.type == ItemID.PygmyNecklace
            )
            {
                List<TooltipLine> infoLine = tooltips.FindAll(t => (t.Name == "Tooltip0") && (t.Mod == "Terraria"));
                infoLine.ForEach(t => t.Text = "Increases your max number of minions by 2");
            }
            if
            (
                item.type == ItemID.AvengerEmblem
            )
            {
                List<TooltipLine> infoLine = tooltips.FindAll(t => (t.Name == "Tooltip0") && (t.Mod == "Terraria"));
                infoLine.ForEach(t => t.Text = "12% increased damage\n6% increased attack speed");
            }
            if
            (
                item.type == ItemID.SniperScope
            )
            {
                List<TooltipLine> infoLine = tooltips.FindAll(t => (t.Name == "Tooltip1") && (t.Mod == "Terraria"));
                infoLine.ForEach(t => t.Text = "12% increased ranged damage\n10% increased ranged critical strike chance");
            }
            if
            (
                item.type == ItemID.ReconScope
            )
            {
                List<TooltipLine> infoLine = tooltips.FindAll(t => (t.Name == "Tooltip1") && (t.Mod == "Terraria"));
                infoLine.ForEach(t => t.Text = "12% increased ranged damage\n12% increased ranged critical strike chance");
            }
            if
            (
                item.type == ItemID.WormScarf
            )
            {
                List<TooltipLine> infoLine = tooltips.FindAll(t => (t.Name == "Tooltip0") && (t.Mod == "Terraria"));
                infoLine.ForEach(t => t.Text = t.Text.Replace("17", "20"));
            }
            if
            (
                item.type == ItemID.AnkhShield
            )
            {
                List<TooltipLine> infoLine = tooltips.FindAll(t => (t.Name == "Defense") && (t.Mod == "Terraria"));
                infoLine.ForEach(t => t.Text = "8 defense\n16% increased defense effectiveness");
            }
            if
            (
                item.type == ItemID.RodOfHarmony
            )
            {
                TooltipLine chaosStateLine = new TooltipLine(Mod, "ChaosState", "Causes the chaos state");
                tooltips.Add(chaosStateLine);
            }
            if (WDALBModSystem.isThoriumModPresent && WDALBModSystem.MCIDIntegrity)
            {
                if
                (
                    item.DamageType == DamageClass.Throwing ||
                    item.DamageType == WDALBModContentID.GetThoriumDamageClass(WDALBModContentID.ThoriumDamageClass_BardDamage) ||
                    item.DamageType == WDALBModContentID.GetThoriumDamageClass(WDALBModContentID.ThoriumDamageClass_HealerDamage) ||
                    item.DamageType == WDALBModContentID.GetThoriumDamageClass(WDALBModContentID.ThoriumDamageClass_HealerTool) ||
                    item.DamageType == WDALBModContentID.GetThoriumDamageClass(WDALBModContentID.ThoriumDamageClass_HealerToolDamageHybrid) ||
                    item.DamageType == WDALBModContentID.GetThoriumDamageClass(WDALBModContentID.ThoriumDamageClass_TrueDamage)
                )
                {
                    TooltipLine thoriumClassExtraDmgLine =
                    (
                        new TooltipLine(Mod, "WDALTPowerup", "+25% damage (From WeDoALittleTrolling)")
                        {
                            IsModifier = true,
                            IsModifierBad = false,
                        }
                    );
                    tooltips.Add(thoriumClassExtraDmgLine);
                }
            }
            base.ModifyTooltips(item, tooltips);
        }

        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (item.type == ItemID.MoltenFury)
            {
                if (type == ProjectileID.FireArrow)
                {
                    type = ProjectileID.HellfireArrow;
                }
            }
            if (item.type == ItemID.ChlorophyteShotbow)
            {
                if (type == ProjectileID.WoodenArrowFriendly)
                {
                    type = ProjectileID.ChlorophyteArrow;
                }
            }
            if (item.type == ItemID.DaedalusStormbow)
            {
                if (type == ProjectileID.WoodenArrowFriendly)
                {
                    type = ProjectileID.HolyArrow;
                }
            }
            if (item.type == ItemID.LeadBow)
            {
                if (type == ProjectileID.WoodenArrowFriendly)
                {
                    type = ProjectileID.PoisonDartBlowgun;
                }
            }
            base.ModifyShootStats(item, player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }

        public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
        {
            if (WDALBModSystem.isThoriumModPresent && WDALBModSystem.MCIDIntegrity)
            {
                if
                (
                    item.DamageType == DamageClass.Throwing ||
                    item.DamageType == WDALBModContentID.GetThoriumDamageClass(WDALBModContentID.ThoriumDamageClass_BardDamage) ||
                    item.DamageType == WDALBModContentID.GetThoriumDamageClass(WDALBModContentID.ThoriumDamageClass_HealerDamage) ||
                    item.DamageType == WDALBModContentID.GetThoriumDamageClass(WDALBModContentID.ThoriumDamageClass_HealerTool) ||
                    item.DamageType == WDALBModContentID.GetThoriumDamageClass(WDALBModContentID.ThoriumDamageClass_HealerToolDamageHybrid) ||
                    item.DamageType == WDALBModContentID.GetThoriumDamageClass(WDALBModContentID.ThoriumDamageClass_TrueDamage)
                )
                {
                    damage += 0.25f;
                }
            }
            base.ModifyWeaponDamage(item, player, ref damage);
        }

        public override void SetStaticDefaults()
        {
            PrefixLegacy.ItemSets.GunsBows[ItemID.FlareGun] = true;
        }

        public override void SetDefaults(Item item)
        {
            if (WDALBModSystem.isCalamityModPresent)
            {
                base.SetDefaults(item);
                return;
            }
            if (item.type == ItemID.Phantasm)
            {
                item.damage = 50;
                item.crit = 1;
            }
            if (item.type == ItemID.VortexBeater)
            {
                item.damage = 60;
                item.crit = 2;
            }
            if (item.type == ItemID.FetidBaghnakhs)
            {
                item.damage = 70;
                item.useTime = 7;
                item.useAnimation = 7;
            }
            if (item.type == ItemID.LandMine)
            {
                item.value = Item.buyPrice(silver: 50);
            }
            if (item.type == ItemID.ChlorophyteShotbow)
            {
                item.useTime = 16;
                item.useAnimation = 16;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.Megashark)
            {
                item.useTime = 6;
                item.useAnimation = 6;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.DD2PhoenixBow) //Phantom Phoenix
            {
                item.damage = 35;
                item.crit = 1;
            }
            if (item.type == ItemID.CactusBreastplate)
            {
                item.defense += 2;
            }
            if (item.type == ItemID.CactusSword)
            {
                item.useTime = 25;
                item.useAnimation = 25;
                item.shootsEveryUse = true;
                item.knockBack = 5f;
            }
            if (item.type == ItemID.BoneSword)
            {
                item.shootsEveryUse = true;
                item.shoot = ProjectileID.BoneGloveProj;
                item.shootSpeed = 10f;
            }
            if (item.type == ItemID.EnchantedSword)
            {
                item.shootsEveryUse = true;
                item.knockBack = 5.25f;
            }
            if (item.type == ItemID.TheRottedFork)
            {
                if (item.damage <= 20)
                {
                    item.damage = 20;
                }
                item.useTime = 28;
                item.useAnimation = 28;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.FlamingMace)
            {
                if (item.damage <= 20)
                {
                    item.damage = 20;
                }
            }
            if (item.type == ItemID.BlueMoon)
            {
                if (item.damage <= 60)
                {
                    item.damage = 60;
                }
            }
            if (item.type == ItemID.Sunfury)
            {
                if (item.damage <= 70)
                {
                    item.damage = 70;
                }
            }
            if (item.type == ItemID.DarkLance)
            {
                if (item.damage <= 40)
                {
                    item.damage = 40;
                }
            }
            if (item.type == ItemID.Gungnir)
            {
                item.crit = 2;
            }
            if (item.type == ItemID.MonkStaffT1)
            {
                if (item.damage <= 55)
                {
                    item.damage = 55;
                }
            }
            if (item.type == ItemID.MonkStaffT2)
            {
                if (item.damage <= 55)
                {
                    item.damage = 55;
                }
            }
            if (item.type == ItemID.MonkStaffT3)
            {
                if (item.damage <= 150)
                {
                    item.damage = 150;
                }
            }
            if (item.type == ItemID.Terragrim)
            {
                if (item.damage <= 20)
                {
                    item.damage = 20;
                }
            }
            if (item.type == ItemID.Arkhalis)
            {
                if (item.damage <= 35)
                {
                    item.damage = 35;
                }
            }
            if (item.type == ItemID.FalconBlade)
            {
                item.damage = 30;
                item.useTime = 15;
                item.useAnimation = 15;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.BeamSword)
            {
                item.damage = 55;
                item.crit = 1;
                item.useTime = 30;
                item.useAnimation = 15;
                item.attackSpeedOnlyAffectsWeaponAnimation = false;
                item.GetGlobalItem<WDALBItemUtil>().attackSpeedRoundingErrorProtection = true;
            }
            if (item.type == ItemID.ClockworkAssaultRifle)
            {
                item.damage = 20;
            }
            if (item.type == ItemID.Marrow)
            {
                if (item.damage <= 60)
                {
                    item.damage = 60;
                }
                item.useTime = 18;
                item.useAnimation = 18;
                item.shootsEveryUse = true;
                item.crit = 2;
            }
            if (item.type == ItemID.FlintlockPistol)
            {
                item.damage = 15;
            }
            if (item.type == ItemID.FlareGun)
            {
                item.damage = 8;
                item.crit = 4;
                item.knockBack = 2f;
                item.DamageType = DamageClass.Ranged;
            }
            if (item.type == ItemID.ChristmasTreeSword)
            {
                item.shootSpeed = 10f;
            }
            if (item.type == ItemID.TheHorsemansBlade)
            {
                item.useTime = 20;
                item.useAnimation = 20;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.BatScepter)
            {
                item.damage = 50;
            }
            if (item.type == ItemID.CandyCornRifle)
            {
                item.ArmorPenetration = 48;
                item.damage = 48;
                item.useTime = 8;
                item.useAnimation = 8;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.JackOLanternLauncher)
            {
                item.damage = 70;
                item.crit = 11;
            }
            if (item.type == ItemID.NailGun)
            {
                item.damage = 90;
            }
            if (item.type == ItemID.MagnetSphere)
            {
                item.damage = 55;
            }
            /*
            if (item.type == ItemID.ReaverShark)
            {
                item.pick = 100;
            }
            */
            /*
            if (item.type == ItemID.ZapinatorOrange)
            {
                item.buffType = BuffID.Electrified;
                item.buffTime = 240;
            }
            if (item.type == ItemID.ZapinatorGray)
            {
                item.buffType = BuffID.Electrified;
                item.buffTime = 240;
            }
            */
            if (item.type == ItemID.LunarFlareBook)
            {
                item.damage = 110;
                item.mana = 8;
            }
            if (item.type == ItemID.LastPrism)
            {
                item.damage = 100;
                item.mana = 8;
            }
            if (item.type == ItemID.ClingerStaff)
            {
                item.damage = 60;
                item.knockBack = 10f;
            }
            if (item.type == ItemID.RazorbladeTyphoon)
            {
                item.damage = 90;
                item.ArmorPenetration = 30;
                item.mana = 15;
            }
            if (item.type == ItemID.BubbleGun)
            {
                item.damage = 64;
                item.useTime = 8;
                item.useAnimation = 8;
                item.shootsEveryUse = true;
                item.ArmorPenetration = 16;
                item.mana = 4;
            }
            if (item.type == ItemID.OpticStaff)
            {
                item.damage = 28;
            }
            if
            (
                item.type == ItemID.PygmyStaff ||
                item.type == ItemID.StormTigerStaff ||
                item.type == ItemID.DeadlySphereStaff ||
                item.type == ItemID.TempestStaff ||
                item.type == ItemID.RavenStaff
            )
            {
                item.damage += 5;
            }
            if (item.type == ItemID.XenoStaff)
            {
                item.damage = 40;
            }
            if (item.type == ItemID.Xenopopper)
            {
                item.damage = 48;
                item.useTime = 20;
                item.useAnimation = 20;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.StardustDragonStaff)
            {
                item.damage = 45;
            }
            if (item.type == ItemID.StardustCellStaff)
            {
                item.damage = 70;
            }
            if (item.type == ItemID.EmpressBlade) //Terraprisma
            {
                item.damage = 110;
            }
            if
            (
                item.type == ItemID.StardustHelmet ||
                item.type == ItemID.StardustBreastplate ||
                item.type == ItemID.StardustLeggings ||
                item.type == ItemID.NebulaHelmet ||
                item.type == ItemID.NebulaBreastplate ||
                item.type == ItemID.NebulaLeggings ||
                item.type == ItemID.VortexHelmet ||
                item.type == ItemID.VortexBreastplate ||
                item.type == ItemID.VortexLeggings ||
                item.type == ItemID.SolarFlareHelmet ||
                item.type == ItemID.SolarFlareBreastplate ||
                item.type == ItemID.SolarFlareLeggings
            )
            {
                item.defense += 2;
            }
            if
            (
                item.type == ItemID.TikiMask ||
                item.type == ItemID.TikiShirt ||
                item.type == ItemID.TikiPants
            )
            {
                item.shopCustomPrice = 300000;
            }
            if (item.type == ItemID.FishingPotion)
            {
                item.shopCustomPrice = Item.buyPrice(gold: 20);
            }
            if (item.type == ItemID.CratePotion)
            {
                item.shopCustomPrice = Item.buyPrice(gold: 40);
            }
            if (item.type == ItemID.SonarPotion)
            {
                item.shopCustomPrice = Item.buyPrice(gold: 60);
            }
            if
            (
                item.type == ItemID.SpookyHelmet ||
                item.type == ItemID.SpookyLeggings ||
                item.type == ItemID.SpookyBreastplate
            )
            {
                item.defense += 2;
            }
            if (item.type == ItemID.SpectreMask)
            {
                item.defense += 4;
            }
            if (item.type == ItemID.SpectreRobe)
            {
                item.defense += 2;
            }
            if
            (
                item.type == ItemID.ShroomiteHeadgear ||
                item.type == ItemID.ShroomiteMask ||
                item.type == ItemID.ShroomiteHelmet
            )
            {
                item.defense += 5;
            }
            if
            (
                item.type == ItemID.ShroomiteBreastplate ||
                item.type == ItemID.ShroomiteLeggings
            )
            {
                item.defense += 4;
            }
            if (item.type == ItemID.BeetleShell)
            {
                item.defense += 4;
            }
            if (item.type == ItemID.BeetleLeggings)
            {
                item.defense += 3;
            }

            // Buff all pre-hardmode summon armors

            if
            (
                item.type == ItemID.BeeHeadgear ||
                item.type == ItemID.BeeBreastplate ||
                item.type == ItemID.BeeGreaves ||
                item.type == ItemID.ObsidianHelm ||
                item.type == ItemID.ObsidianShirt ||
                item.type == ItemID.ObsidianPants ||
                item.type == ItemID.FlinxFurCoat
            )
            {
                item.defense += 2;
            }
            if
            (
                item.type == ItemID.FossilHelm ||
                item.type == ItemID.FossilShirt ||
                item.type == ItemID.FossilPants
            )
            {
                item.defense += 1;
            }

            if
            (
                item.type == ItemID.SpiderMask ||
                item.type == ItemID.SpiderBreastplate ||
                item.type == ItemID.SpiderGreaves ||
                item.type == ItemID.AncientBattleArmorHat || //Forbidden Mask
                item.type == ItemID.AncientBattleArmorShirt || //Forbidden Robes
                item.type == ItemID.AncientBattleArmorPants //Forbidden Treads
            )
            {
                item.defense += 4;
            }

            // Buff pre-hardmode whips

            if
            (
                item.type == ItemID.BlandWhip || //Leather Whip
                item.type == ItemID.ThornWhip || //Snapthorn
                item.type == ItemID.BoneWhip //Spinal Tap
            )
            {
                item.damage += 5;
            }

            // Buff hardmode whips

            if (item.type == ItemID.FireWhip) //Firecracker
            {
                item.damage += 8;
            }
            if (item.type == ItemID.CoolWhip) 
            {
                item.damage += 10;
            }
            if (item.type == ItemID.SwordWhip) //Durendal
            {
                item.damage += 15;
            }
            if (item.type == ItemID.ScytheWhip) //Dark Harvest
            {
                item.damage += 10;
                item.useTime = 25;
                item.useAnimation = 25;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.MaceWhip) //Morning Star
            {
                item.damage += 15;
            }
            if (item.type == ItemID.RainbowWhip) //Kaleidoscope
            {
                item.damage += 20;
            }

            if
            (
                item.type == ItemID.ElectrosphereLauncher
            )
            {
                item.autoReuse = true;
            }
            if (item.type == ItemID.StarWrath)
            {
                item.damage = 100;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.Meowmere)
            {
                item.damage = 250;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.SDMG)
            {
                item.damage = 120;
            }
            if (item.type == ItemID.Seedler)
            {
                item.damage = 64;
                item.useTime = 24;
                item.useAnimation = 24;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.TrueExcalibur)
            {
                item.damage = 80;
                item.useTime = 16;
                item.useAnimation = 16;
                item.shootsEveryUse = true;
                item.knockBack = 14f;
            }
            if (item.type == ItemID.Excalibur)
            {
                item.damage = 80;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.TrueNightsEdge)
            {
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.TerraBlade)
            {
                item.damage = 100;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.InfluxWaver)
            {
                item.damage = 80;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.PossessedHatchet)
            {
                item.damage = 90;
                item.useTime = 12;
                item.useAnimation = 12;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.PaladinsHammer)
            {
                item.damage = 100;
                item.shootSpeed = 24f;
            }
            if (item.type == ItemID.DayBreak)
            {
                item.useTime = 14;
                item.useAnimation = 14;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.SolarEruption)
            {
                if (item.damage <= 125)
                {
                    item.damage = 125;
                }
            }
            if (item.type == ItemID.IllegalGunParts)
            {
                item.shopCustomPrice = 80000;
            }
            if (item.type == ItemID.AntlionClaw) //Mandible Blade
            {
                if (item.damage <= 20)
                {
                    item.damage = 20;
                }
            }
            if (item.type == ItemID.ChainKnife)
            {
                item.damage = 16;
                if(item.Variant == ItemVariants.StrongerVariant)
                {
                    item.damage = 75;
                }
            }
            if (item.type == ItemID.ThunderSpear)
            {
                item.damage = 16;
            }
            if (item.type == ItemID.ThunderStaff)
            {
                item.damage = 22;
            }
            if (item.type == ItemID.Starfury)
            {
                item.damage = 28;
            }
            if
            (
                item.type == ItemID.JackOLanternMask ||
                item.type == ItemID.SWATHelmet ||
                item.type == ItemID.RainbowCrystalStaff ||
                item.type == ItemID.RainbowWhip
            )
            {
                item.material = true;
            }
            if (item.type == ItemID.SpiderStaff)
            {
                if (item.damage <= 32)
                {
                    item.damage = 32;
                }
            }
            if (item.type == ItemID.Smolstar) //Blade Staff
            {
                if (item.damage <= 8)
                {
                    item.damage = 8;
                }
            }
            if (item.type == ItemID.CrystalVileShard)
            {
                if (item.damage <= 30)
                {
                    item.damage = 30;
                }
            }
            if (item.type == ItemID.Hammush)
            {
                if (item.damage <= 60)
                {
                    item.damage = 60;
                }
                item.useTime = 25;
                item.useAnimation = 25;
            }
            if (item.type == ItemID.MushroomSpear)
            {
                item.crit = 8;
                item.useTime = 35;
                item.useAnimation = 35;
            }
            if (item.type == ItemID.ChlorophytePartisan)
            {
                if (item.damage <= 55)
                {
                    item.damage = 55;
                }
            }
            if
            (
                item.type == ItemID.ChlorophyteHeadgear ||
                item.type == ItemID.ChlorophyteHelmet ||
                item.type == ItemID.ChlorophyteMask ||
                item.type == ItemID.ChlorophytePlateMail ||
                item.type == ItemID.ChlorophyteGreaves
            )
            {
                item.defense += 3;
            }
            if (item.type == ItemID.ChlorophyteClaymore)
            {
                item.useTime = 52;
                item.useAnimation = 26;
                item.attackSpeedOnlyAffectsWeaponAnimation = false;
                item.GetGlobalItem<WDALBItemUtil>().attackSpeedRoundingErrorProtection = true;
            }
            if (item.type == ItemID.Trimarang)
            {
                item.damage = 24;
                item.useTime = 18;
                item.useAnimation = 18;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.Flamarang)
            {
                item.damage = 55;
            }
            if (item.type == ItemID.DeathSickle)
            {
                if (item.damage <= 60)
                {
                    item.damage = 60;
                }
                item.crit = 2;
            }
            if (item.type == ItemID.TitaniumSword)
            {
                item.damage = 75;
            }
            if (item.type == ItemID.TitaniumTrident)
            {
                item.damage = 60;
            }
            if (item.type == ItemID.TitaniumWaraxe)
            {
                item.damage = 90;
            }
            if (item.type == ItemID.TitaniumRepeater)
            {
                item.useTime = 15;
                item.useAnimation = 15;
            }
            if (item.type == ItemID.TitaniumHelmet)
            {
                item.defense += 10;
            }
            if (item.type == ItemID.TitaniumMask)
            {
                item.defense += 3;
            }
            if (item.type == ItemID.AdamantiteHelmet)
            {
                item.defense += 6;
            }
            if (item.type == ItemID.AdamantiteSword)
            {
                item.damage = 75;
            }
            if (item.type == ItemID.AdamantiteGlaive)
            {
                item.damage = 60;
            }
            if (item.type == ItemID.AdamantiteWaraxe)
            {
                item.damage = 90;
            }
            if (item.type == ItemID.AdamantiteRepeater)
            {
                item.useTime = 16;
                item.useAnimation = 16;
            }
            if (item.type == ItemID.HallowedRepeater)
            {
                item.useTime = 14;
                item.useAnimation = 14;
            }
            if (item.type == ItemID.FlowerofFire)
            {
                item.mana = 8;
                item.autoReuse = true;
            }
            if (item.type == ItemID.UnholyTrident)
            {
                item.damage = 55;
                item.mana = 20;
                item.autoReuse = true;
            }
            if
            (
                item.type == ItemID.PoisonStaff ||
                item.type == ItemID.VenomStaff
            )
            {
                if (item.damage <= 50)
                {
                    item.damage = 40;
                }
            }
            if (item.type == ItemID.BlizzardStaff)
            {
                item.damage = 64;
                item.UseSound = SoundID.Item9;
            }
            if (item.type == ItemID.NebulaBlaze)
            {
                item.damage = 145;
                item.mana = 8;
            }
            if (item.type == ItemID.NebulaArcanum)
            {
                item.damage = 90;
                item.mana = 20;
            }

            // Buff all pre-hardmode summons

            if
            (
                item.type == ItemID.AbigailsFlower ||
                item.type == ItemID.BabyBirdStaff || //Finch Staff
                item.type == ItemID.FlinxStaff ||
                item.type == ItemID.SlimeStaff ||
                item.type == ItemID.HornetStaff ||
                item.type == ItemID.VampireFrogStaff ||
                item.type == ItemID.ImpStaff
            )
            {
                item.damage += 2;
            }

            // Buff Ice-Biome related stuff
            // Pre-Hardmode
            if (item.type == ItemID.IceSickle)
            {
                if (item.damage <= 55)
                {
                    item.damage = 55;
                }
                item.crit = 1;
                item.autoReuse = true;
            }
            if (item.type == ItemID.FrostStaff)
            {
                if (item.damage <= 50)
                {
                    item.damage = 50;
                }
                item.autoReuse = true;
                item.crit = 1;
                item.mana = 8;
            }
            if (item.type == ItemID.IceBoomerang)
            {
                if (item.damage <= 24)
                {
                    item.damage = 24;
                }
                item.autoReuse = true;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.IceBlade)
            {
                if (item.damage <= 18)
                {
                    item.damage = 18;
                }
                item.autoReuse = true;
            }
            if (item.type == ItemID.SnowballCannon)
            {
                item.damage = 12;
                item.autoReuse = true;
                item.useTime = 16;
                item.useAnimation = 16;
                item.shootsEveryUse = true;
            }
            //Hardmode
            if (item.type == ItemID.Frostbrand)
            {
                if (item.damage <= 50)
                {
                    item.damage = 50;
                }
                item.useTime = 22;
                item.useAnimation = 22;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.IceBow)
            {
                if (item.damage <= 45)
                {
                    item.damage = 45;
                }
            }
            if (item.type == ItemID.FlowerofFrost)
            {
                item.mana = 8;
                item.autoReuse = true;
            }
            if (item.type == ItemID.StaffoftheFrostHydra)
            {
                item.damage = 80;
            }
            if (item.type == ItemID.QueenSpiderStaff)
            {
                item.damage = 45;
            }
            if (item.type == ItemID.HoundiusShootius)
            {
                item.damage = 35;
            }
            if (item.type == ItemID.RainbowCrystalStaff)
            {
                item.damage = 140;
            }
            if (item.type == ItemID.MoonlordTurretStaff)
            {
                item.damage = 110;
            }
            if (item.type == ItemID.SkyFracture)
            {
                if (item.damage <= 40)
                {
                    item.damage = 40;
                }
            }
            if (item.type == ItemID.MeteorStaff)
            {
                item.damage = 55;
            }
            if (item.type == ItemID.BookStaff)
            {
                item.damage = 40;
                item.mana = 12;
            }
            if (item.type == ItemID.Bananarang)
            {
                item.damage = 50;
            }
            if (item.type == ItemID.Uzi)
            {
                item.damage = 32;
                item.useTime = 8;
                item.useAnimation = 8;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.VenusMagnum)
            {
                item.damage = 56;
                item.useTime = 8;
                item.useAnimation = 8;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.LeafBlower)
            {
                item.damage = 56;
                item.useTime = 6;
                item.useAnimation = 6;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.TacticalShotgun)
            {
                item.damage = 36;
                item.useTime = 32;
                item.useAnimation = 32;
                item.autoReuse = true;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.SniperRifle)
            {
                item.damage = 240;
                item.crit = 28;
                item.useTime = 32;
                item.useAnimation = 32;
                item.autoReuse = true;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.Tsunami)
            {
                item.damage = 55;
                item.crit = 1;
                item.useTime = 25;
                item.useAnimation = 25;
                item.shootsEveryUse = true;
            }

            //Pre-Hardmode Ranger Rebalance

            if (item.type == ItemID.BeesKnees)
            {
                item.damage = 24;
                item.useTime = 22;
                item.useAnimation = 22;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.DemonBow)
            {
                item.damage = 16;
                item.useTime = 24;
                item.useAnimation = 24;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.HellwingBow)
            {
                item.useTime = 12;
                item.useAnimation = 12;
                item.shootsEveryUse = true;
            }
            if (item.type == ItemID.MoltenFury)
            {
                item.damage = 32;
                item.shootSpeed = 12f;
            }

            if (item.type == ItemID.Celeb2)
            {
                if (item.damage <= 75)
                {
                    item.damage = 75;
                }
            }
            if (item.type == ItemID.FireworksLauncher)
            {
                if (item.damage <= 35)
                {
                    item.damage = 35;
                }
            }
            if
            (
                item.type == ItemID.LeadAxe ||
                item.type == ItemID.LeadBroadsword ||
                item.type == ItemID.LeadHammer ||
                item.type == ItemID.LeadPickaxe ||
                item.type == ItemID.LeadBow
            )
            {
                item.useTime = (int)Math.Round((double)item.useTime * 1.3);
                item.useAnimation = (int)Math.Round((double)item.useAnimation * 1.3);
                item.knockBack += 4f;
            }

            //Buff Shortswords
            int[] shortSwordsToBuff =
            {
                ItemID.CopperShortsword,
                ItemID.TinShortsword,
                ItemID.IronShortsword,
                ItemID.SilverShortsword,
                ItemID.TungstenShortsword,
                ItemID.GoldShortsword,
                ItemID.PlatinumShortsword,
                ItemID.Gladius
            };
            if (shortSwordsToBuff.Contains(item.type))
            {
                item.damage += 4;
                item.knockBack += 1.5f;
            }
            if (item.type == ItemID.LeadShortsword)
            {
                item.knockBack += 4f;
            }
        }
    }
}
