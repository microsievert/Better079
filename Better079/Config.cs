using System.Collections.Generic;
using Exiled.API.Interfaces;
using System.ComponentModel;
using Exiled.API.Enums;

namespace Better079
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        [Description("Enable/Disable spawn message for SCP-079")]
        public bool Scp079SpawnMessageEnabled { get; set; } = true;

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

        [Description("Can player use .copy ability?")]
        public bool CopyEnabled { get; set; } = true;

        [Description("How many time players need to copy SCP-079")]
        public float CopyTime { get; set; } = 50f;

        [Description("How many time player have to take dropped chaos insurgency device")]
        public float CopyTakeTime { get; set; } = 20f;
        
        [Description("Which teams can help SCP-079 escape?")]
        public List<Team> CopyAllowedTeams { get; set; } = new List<Team>
        {
            Team.CDP,
            Team.CHI
        };

        [Description("In which rooms copy command can be used?")]
        public List<RoomType> CopyAllowedRooms { get; set; } = new List<RoomType>
        {
            RoomType.HczServers,
            RoomType.HczNuke
        };

        [Description("SCP-079 escape CASSIE (Set empty string to disable)")]
        public string EscapeCassie { get; set; } = "Attention . SCP 0 7 9 escaped from site . . emergency . high level of alarm .";

        [Description("SCP-079 copied CASSIE (Set empty string to disable)")]
        public string CopyCassie { get; set; } = "Attention . unknown U S B device detected in data room . All facility guards please report data room immediately";
        
        [Description("Should keycard bypass be given to commands that helped SCP-079 escape?")]
        public bool EscapeBypassEnabled { get; set; } = true;

        [Description("Activate alpha warhead after SCP-079 escape?")]
        public bool EscapeActivateWarhead { get; set; } = true;

        [Description("Forceclass SCP-079 after escape?")]
        public bool EscapeForceclass { get; set; } = true;
    }
}