using System;

using Exiled.API.Features;
using Exiled.API.Features.Roles;

using CommandSystem;

using RemoteAdmin;

using Better079.API.Classes;

namespace Better079.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Overload : DelayedCommand, ICommand
    {
        public string Command => "overload";
        public string Description => "[SCP-079 ABILITY] Disabling all engaged generators (Can be called only as SCP-079)";

        public string[] Aliases => Array.Empty<string>();

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!Better079.Instance.Config.GeneratorsDropEnabled)
            {
                response = "Command disabled by server owner or you is not player.";
                return false;
            }

            if (!(sender is PlayerCommandSender))
            {
                response = "Only players can call this command.";
                return false;
            }

            Player player = Player.Get(sender);
            Scp079Role playerRole;
            
            if (!player.Role.Is(out playerRole))
            {
                response = "Sorry but only SCP-079 can call this command.";
                return true;
            }
            
            if (_isReady)
            {
                playerRole.Energy = 0f;

                foreach (Generator generator in Generator.List)
                {
                    generator.IsActivating = false;

                    Map.TurnOffAllLights(1f);
                }

                Map.PlayAmbientSound(6);

                ForceDelay(Better079.Instance.Config.GeneratorsDropCooldown);

                response = "Successfully protected. Generators disabled.";
                return true;
            }
            
            response = "Denied. Request error. Wait access.";
            return false;
        }
    }
}
