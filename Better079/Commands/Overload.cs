using CommandSystem;
using RemoteAdmin;
using System;
using Exiled.API.Features;
using Exiled.API.Features.Roles;
using MEC;

namespace Better079.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Overload : ICommand
    {
        public string Command => "overload";
        public string Description => "[SCP-079 ABILITY] Disabling all engaged generators (Can be called only as SCP-079)";

        public string[] Aliases => Array.Empty<string>();

        private bool _dropAllowed = true;

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
            
            if (_dropAllowed)
            {
                playerRole.Script.Mana = 0f;

                foreach (Generator generator in Generator.List)
                {
                    generator.IsActivating = false;

                    Map.TurnOffAllLights(1f);
                }

                Map.PlayAmbientSound(6);

                _dropAllowed = false;

                Timing.CallDelayed(Better079.Instance.Config.GeneratorsDropCooldown, () => _dropAllowed = true);

                response = "Successfully protected. Generators disabled.";
                return true;
            }
            else
            {
                response = "Denied. Request error. Wait access.";
                return false;
            }
        }
    }
}
