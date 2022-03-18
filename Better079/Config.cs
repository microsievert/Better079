using Exiled.API.Interfaces;
using System.ComponentModel;

namespace Better079
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        [Description("Mark peoples on SCP-079 map")]
        public bool BetterMapEnabled { get; set; } = true;

        [Description("Allow SCP-079 use overcharge ability")]
        public bool OverchargeEnabled { get; set; } = true;

        [Description("Overcharge command energy price for SCP-079")]
        public float OverchargePrice { get; set; } = 90f;

        [Description("Maximum overcharge command time for SCP-079")]
        public float OverchargeMaxtime { get; set; } = 55f;

        [Description("Allow SCP-079 use generators save ability (Disable all generators)")]
        public bool GeneratorsDropEnabled { get; set; } = true;

        [Description("Generators save ability cooldown")]
        public float GeneratorsDropCooldown { get; set; } = 200f;

        [Description("Upgrade SCP-079 Tier level automatically?")]
        public bool AutoTierEnabled { get; set; } = true;

        [Description("Target tier level after upgrade")]
        public int AutoTierLevel { get; set; } = 2;

        [Description("The time to receive a level")]
        public float AutoTierTime { get; set; } = 280f;
    }
}