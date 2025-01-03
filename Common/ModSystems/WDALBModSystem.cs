/*
    WeDoALittleBalancing is a Terraria Mod made with tModLoader.
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

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace WeDoALittleBalancing.Common.ModSystems
{
    internal class WDALBModSystem : ModSystem
    {

        public static bool isCalamityModPresent = false;
        public static bool isThoriumModPresent = false;
        public static bool isConsolariaModPresent = false;
        public static bool isSpiritModPresent = false;
        public static bool isSpookyModPresent = false;
        public static readonly string calamityModName = "CalamityMod";
        public static readonly string thoriumModName = "ThoriumMod";
        public static readonly string consolariaModName = "Consolaria";
        public static readonly string spiritModName = "SpiritMod";
        public static readonly string spookyModName = "Spooky";
        public static bool MCIDIntegrity;
        
        public override void OnModLoad()
        {
            if(ModLoader.HasMod(spookyModName))
            {
                isSpookyModPresent = true;
                WeDoALittleBalancing.logger.Info("Spooky Mod detected.");
                WeDoALittleBalancing.logger.Info("Spooky Mod Bosses can now inflict Vulnerable and Wrecked Resistance.");
            }
            if(ModLoader.HasMod(spiritModName))
            {
                isSpiritModPresent = true;
                WeDoALittleBalancing.logger.Info("Spirit Mod detected.");
                WeDoALittleBalancing.logger.Info("Spirit Mod Bosses can now inflict Vulnerable and Wrecked Resistance.");
            }
            if(ModLoader.HasMod(consolariaModName))
            {
                isConsolariaModPresent = true;
                WeDoALittleBalancing.logger.Info("Consolaria Mod detected.");
                WeDoALittleBalancing.logger.Info("Vampire Miners can now drop Yellow Crystals.");
            }
            if(ModLoader.HasMod(thoriumModName))
            {
                isThoriumModPresent = true;
                WeDoALittleBalancing.logger.Info("Thorium Mod detected.");
                WeDoALittleBalancing.logger.Info("WeDoALittleBalancing has advanced compatibility features for Thorium Mod.");
                WeDoALittleBalancing.logger.Info("For a list of changes this Mod does to Thorium Mod, please see this Mod's description.");
                WeDoALittleBalancing.logger.Info("If you report any bugs to the Thorium Mod developers, make sure this Mod is disabled first.");
            }
            if(ModLoader.HasMod(calamityModName))
            {
                isCalamityModPresent = true;
                WeDoALittleBalancing.logger.Warn("Calamity Mod detected.");
                WeDoALittleBalancing.logger.Warn("WeDoALittleBalancing may not work well together with Calamity Mod.");
                WeDoALittleBalancing.logger.Warn("Most rebalancing features of WeDoALittleBalancing are not compatible with Calamity Mod.");
                WeDoALittleBalancing.logger.Warn("Disabling most rebalancing features of WeDoALittleBalancing to ensure the game stays playable...");
            }
            MCIDIntegrity = WDALBModContentID.SetContentIDs();
            if (!MCIDIntegrity)
            {
                WeDoALittleBalancing.logger.Error("ModContentID Integrity Check Failed! Please report this to the WeDoALittleBalancing developers.");
                WeDoALittleBalancing.logger.Error("Disabling all Cross-Compatibilty related features...");
            }
            RegisterHooks();
            base.OnModLoad();
        }

        public override void OnModUnload()
        {
            UnregisterHooks();
            base.OnModUnload();
        }

        public static void RegisterHooks()
        {

        }

        public static void UnregisterHooks()
        {

        }

        public static bool TryGetCalamityMod(out Mod calamityMod)
        {
            if(ModLoader.TryGetMod(calamityModName, out Mod mod))
            {
                calamityMod = mod;
                return true;
            }
            else
            {
                calamityMod = null;
                return false;
            }
        }

        public static bool TryGetThoriumMod(out Mod thoriumMod)
        {
            if(ModLoader.TryGetMod(thoriumModName, out Mod mod))
            {
                thoriumMod = mod;
                return true;
            }
            else
            {
                thoriumMod = null;
                return false;
            }
        }

        public static bool TryGetConsolariaMod(out Mod consolariaMod)
        {
            if(ModLoader.TryGetMod(consolariaModName, out Mod mod))
            {
                consolariaMod = mod;
                return true;
            }
            else
            {
                consolariaMod = null;
                return false;
            }
        }

        public static bool TryGetSpiritMod(out Mod spiritMod)
        {
            if(ModLoader.TryGetMod(spiritModName, out Mod mod))
            {
                spiritMod = mod;
                return true;
            }
            else
            {
                spiritMod = null;
                return false;
            }
        }

        public static bool TryGetSpookyMod(out Mod spookyMod)
        {
            if(ModLoader.TryGetMod(spookyModName, out Mod mod))
            {
                spookyMod = mod;
                return true;
            }
            else
            {
                spookyMod = null;
                return false;
            }
        }
    }
}

