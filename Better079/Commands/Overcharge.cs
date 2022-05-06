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
        public string Description => "[SCP-079 ABILITY] Disables all light in facility for selected time (Can be called only as SCP-079)";

        public string[] Aliases => Array.Empty<string>();

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!Better079.Instance.Config.OverchargeEnabled)
            {
                response = "Command disabled by server owner.";
                return false;
            }

            if (!(sender is PlayerCommandSender) || !arguments.Any())
            {
                response = "Command cannot be executed without required arguments or in console.";
                return false;
            }

            Player player = Player.Get(sender);
            Scp079Role playerRole;

            if (!player.Role.Is(out playerRole))
            {
                response = "Sorry but only SCP-079 can call this command.";
                return true;
            }

            if (float.TryParse(arguments.At(0), out float time) && playerRole.Script.Mana >= Better079.Instance.Config.OverchargePrice)
            {
                if (time > Better079.Instance.Config.OverchargeMaxtime)
                {
                    response = $"Error! Time can't be higher than {Better079.Instance.Config.OverchargeMaxtime} sec";
                    return false;
                }

                Map.TurnOffAllLights(time);
                Map.PlayAmbientSound(UnityEngine.Random.Range(6, 7));

                playerRole.Script.Mana -= Better079.Instance.Config.OverchargePrice;

                response = "Command successfully executed by facility servers.";
                return true;
            }
            else
            {
                response = $"You don't have energy to use ability ({Better079.Instance.Config.OverchargePrice} points) or you is using incorrect time format (time should be writed in seconds).";
                return false;
            }
        }
    }
}