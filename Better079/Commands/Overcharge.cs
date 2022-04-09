using System;
using System.Linq;
using CommandSystem;
using RemoteAdmin;
using Exiled.API.Features;
using Exiled.API.Features.Roles;

namespace Better079.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Overcharge : ICommand
    {
        public string Command => "overcharge";
        public string Description => "Disables all light in facility for selected time (Can be called only as SCP-079)";

        public string[] Aliases => Array.Empty<string>();

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!Better079.Instance.Config.OverchargeEnabled)
            {
                response = "Command disabled by server owner.";
                return false;
            }

            if (!(sender is PlayerCommandSender))
            {
                response = "Only players can call this command.";
                return false;
            }

            if (!arguments.Any())
            {
                response = "Command should be called with argument (overcharge time)";
                return false;
            }

            Player player = Player.Get(sender);

            if (!player.Role.Is<Scp079Role>(out _))
            {
                response = "Sorry but only SCP-079 can call this command.";
                return true;
            }
            
            Scp079PlayerScript playerScript = player.ReferenceHub.scp079PlayerScript;

            if (float.TryParse(arguments.At<string>(0), out float time) && playerScript.Mana >= Better079.Instance.Config.OverchargePrice)
            {
                if (time > Better079.Instance.Config.OverchargeMaxtime)
                {
                    response = $"Error! Time can't be higher than {Better079.Instance.Config.OverchargeMaxtime} sec";
                    return false;
                }

                Map.TurnOffAllLights(time);
                Map.PlayAmbientSound(UnityEngine.Random.Range(6, 7));

                playerScript.Mana -= Better079.Instance.Config.OverchargePrice;

                response = "Command successfully executed by facility servers.";
                return true;
            }

            response = "Command execution error";
            return true;
        }
    }
}