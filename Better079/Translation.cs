using System.ComponentModel;
using Exiled.API.Interfaces;

namespace Better079
{
    public sealed class Translation : ITranslation
    {
        [Description("User will see this message when he finish copying SCP-079")]
        public string HackSuccessfully { get; private set; } = "<color=green>SCP-079 copied successfully.</color> Go to GATE B escape zone to help SCP-079 escape";

        [Description("User will see this message when he fails copying SCP-079")] 
        public string HackFailure { get; private set; } = "Hack failure. You should stay on place and hold chaos insurgency hack device to copy SCP-079. Try again.";

        [Description("User will see this message when he drop device before SCP-079 escape")]
        public string DeviceDropMessage { get; private set; } = "Warning! You have {time} seconds to take device with SCP-079 back or copy will lost";

        [Description("User will see this message if no active players as SCP-079 found")]
        public string HackFailureNoPlayers { get; private set; } = "Hack failure. No SCP-079 detected in facility system";

        [Description("User will see this message on spawn as SCP-079")]
        public string SpawnMessage { get; private set; } = "Connecting... Access granted. <color=red>Open your console and enter .help to see your abilities</color>";
    }
}