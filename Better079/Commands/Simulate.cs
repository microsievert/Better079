using System;
using System.Linq;
using System.Text;
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
            
            if (!(sender is PlayerCommandSender) || !arguments.Any())
            {
                StringBuilder consoleNotice = new StringBuilder("\n Command should be called as player and with cassie id. IDs: ");
                foreach (string id in Better079.Instance.Config.SimulateCassies.Keys)
                    consoleNotice.Append($"\n {id}");

                response = consoleNotice.ToString();
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

            string cassieKey = arguments.At(0);
            if (Better079.Instance.Config.SimulateCassies.ContainsKey(arguments.At(0)))
            {
                Cassie.GlitchyMessage(Better079.Instance.Config.SimulateCassies[cassieKey], 4, 0.2f);
            }
            else
            {
                response = "Error. Unknown cassie id. Enter this command without arguments to see all message id's";
                return false;
            }

            _simulateAllowed = false;

            Timing.CallDelayed(Better079.Instance.Config.SimulateCooldown, () => _simulateAllowed = true);

            response = "Fake C.A.S.S.I.E message injected successfully";
            return true;
        }
    }
}