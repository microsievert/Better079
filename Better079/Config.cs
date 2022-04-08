using System.Collections.Generic;
using Exiled.API.Interfaces;
using System.ComponentModel;
using Exiled.API.Enums;

namespace Better079
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        [Description("Is SCP-2176 damage to SCP-079 enabled")]
        public bool Scp2179DamageEnabled { get; set; } = true;

        [Description("Amount of energy that will be lost by SCP-079")]
        public float Scp2179EnergyLost { get; set; } = 100f;
        
        [Description("Setup all rooms for SCP-2179 damage effect")]
        public List<RoomType> Scp2176DamageRooms { get; set; } = new List<RoomType>
        {
            RoomType.HczServers,
            RoomType.HczNuke,
            RoomType.EzIntercom
        };

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

        [Description("Allo SCP-079 to use .find ability")]
        public bool FindEnabled { get; set; } = true;

        [Description("SCP-079 .find cooldown in seconds")]
        public float FindCooldown { get; set; } = 20f;

        [Description("Allow SCP-079 to use .simulate ability")] 
        public bool SimulateEnabled { get; set; } = true;

        [Description("SCP-079 .simulate cooldown in seconds")]
        public float SimulateCooldown { get; set; } = 120f;

        [Description("Add some cassies to play as SCP-079 using .simulate")]
        public Dictionary<string, string> SimulateCassies { get; set; } = new Dictionary<string, string>
        {
            ["mtf"] = "MtfUnit epsilon 11 designated NATO_X 15 HasEntered . AllRemaining . UncalculatedScpsLeft",
            ["ci"] = "Attention all security guards . ChaosInsurgency group spotted near facility",
            ["recont"] = "SCP 0 7 9 ContainedSuccessfully by autonomic security system"
        };
    }
}