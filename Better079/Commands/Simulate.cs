using System;
using CommandSystem;
using Exiled.API.Features;
using Exiled.API.Features.Roles;
using MEC;
using RemoteAdmin;

namespace Better079.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Simulate : ICommand
    {
        public string Command => "simulate";
        public string Description => "Send fake message to C.A.S.S.I.E by ID";

        public string[] Aliases => Array.Empty<string>();

        private bool _simulateAllowed = true;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!Better079.Instance.Config.SimulateEnabled)
            {
                response = "Command disabled by server owner.";
                return false;
            }
            
            if (!(sender is PlayerCommandSender))
            {
                response = "Only players can call this command.";
                return false;
            }

            Player caller = Player.Get(sender);

            if (!caller.Role.Is<Scp079Role>(out _))
            {
                response = "Only SCP-079 can call this command";
                return false;
            }
            
            if (!_simulateAllowed)
            {
                response = "Error. Wait before use this ability again";
                return false;
            }

            if (arguments.Count >= 1)
            {
                string cassieKey = arguments.At<string>(0);
                if (Better079.Instance.Config.SimulateCassies.ContainsKey(arguments.At<string>(0)))
                {
                    Cassie.GlitchyMessage(Better079.Instance.Config.SimulateCassies[cassieKey], 4, 0.2f);
                }

                _simulateAllowed = false;

                Timing.CallDelayed(Better079.Instance.Config.SimulateCooldown, () => _simulateAllowed = true);

                response = "Fake C.A.S.S.I.E message injected successfully";
                return true;
            }
            else
            {
                string consoleNotice = "\n Command should be called with cassie id. IDs: ";
                foreach (string id in Better079.Instance.Config.SimulateCassies.Keys)
                {
                    consoleNotice = consoleNotice + $"\n {id}";
                }

                response = consoleNotice;
                return false;
            }
        }
    }
}