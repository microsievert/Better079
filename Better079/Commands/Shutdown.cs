using System;
using System.Linq;

using Exiled.API.Features;
using Exiled.API.Features.Roles;
using Exiled.Permissions.Extensions;

using CommandSystem;

namespace Better079.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Shutdown : ICommand
    {
        public string Command => "shutdown";
        public string Description => "Usage: shutdown <playerId> <time> | Allows admins to lost signal for SCP-079.";

        public string[] Aliases => Array.Empty<string>();

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("bpc.shutdown"))
            {
                response = "You don't have permissions to use that command!";
                return false;
            }

            if (!arguments.Any())
            {
                response = "Command requires at least 2 arguments.";
                return false;
            }

            if (!int.TryParse(arguments.At(0), out int targetId) || !float.TryParse(arguments.At(1), out float shutdownTime))
            {
                response = "Arguments parsing failure. Try again.";
                return false;
            }

            Player targetPlayer = Player.Get(targetId);

            if (targetPlayer == null)
            {
                response = "Player with that id doesn't exists.";
                return false;
            }

            if (!(targetPlayer.Role is Scp079Role scp079Role))
            {
                response = "Command can be applied only for SCP-079.";
                return false;
            }
            
            scp079Role.LoseSignal(shutdownTime);

            response = "";
            return true;
        }
    }
}