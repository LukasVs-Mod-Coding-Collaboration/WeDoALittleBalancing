using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace WeDoALittleBalancing.Common.Configs
{
    public class WDALBServerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;


        [Header("Balancing")]

        [DefaultValue(false)]
        [ReloadRequired]
        public bool DisableRebalancing;
        
        [Header("Items")]

        [DefaultValue(false)]
        [ReloadRequired]
        public bool DisableRodOfHarmonyCooldown;

        [Header("OtherMods")]

        [DefaultValue(false)]
        [ReloadRequired]
        public bool DisableCalamityCompatibilityMode;
        
    }
}
