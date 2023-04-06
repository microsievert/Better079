using System;
using System.Collections.Generic;
using System.Linq;

using Exiled.API.Features;
using Exiled.API.Features.Roles;

using CommandSystem;

using RemoteAdmin;

using PlayerRoles;

using Better079.API.Classes;

using Random = UnityEngine.Random;

namespace Better079.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Find : DelayedCommand, ICommand
    {
        public string Command => "find";
        public string Description => "[SCP-079 ABILITY] Teleport to random player room. Usage: .find <playerId/playerNick> or just .find";

        public string[] Aliases => Array.Empty<string>();

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!Better079.Instance.Config.FindEnabled)
            {
                response = "Command disabled by server owner.";
                return false;
            }
            
            if (!(sender is PlayerCommandSender))
            {
                response = "Error. Only players can use this command.";
                return false;
            }

            Player caller = Player.Get(sender);

            if (!caller.Role.Is(out Scp079Role scp079Role))
            {
                response = "Error. Only SCP-079 can use this command";
                return false;
            }

            if (!_isReady)
            {
                response = "Error. Wait before use this ability again";
                return false;
            }
            
            ForceDelay(Better079.Instance.Config.FindCooldown);

            if (arguments.Any() && Better079.Instance.Config.FindSpecified)
            {
                if (TryFindSpecified(arguments.At(0), out Player targetPlayer) && TryFindCamera(targetPlayer, out Camera targetCam))
                {
                    scp079Role.Camera = targetCam;

                    response = "Moving to target..";
                    return true;
                }

                response = "Player has not been found. Try again.";
                return false;
            }

            if (!TrySelectRandomTarget(Player.List, out Player randomTargetPlayer))
            {
                response = "No targets detected. Try again.";
                return false;
            }

            if (!TryFindCamera(randomTargetPlayer, out Camera targetCamSec))
            {
                response = "No cameras was found. Try again.`";
                return false;
            }

            scp079Role.Camera = targetCamSec;

            response = "Switching camera...";
            return true;
        }

        private bool TrySelectRandomTarget(IEnumerable<Player> list, out Player resultPlayer)
        {
            IEnumerable<Player> targetPlayersList = list.Where(x => x.Role.Team != Team.SCPs);

            if (!targetPlayersList.Any())
            {
                resultPlayer = null;
                return false;
            }

            resultPlayer = targetPlayersList.ElementAt(Random.Range(0, targetPlayersList.Count() - 1));
            return true;
        }

        private bool TryFindSpecified(string targetMention, out Player target)
        {
            if (int.TryParse(targetMention, out int playerId))
            {
                Player targetPlayer = Player.Get(playerId);

                if (targetPlayer != null)
                {
                    target = targetPlayer;
                    return true;
                }
            }

            foreach (Player targetPlayer in Player.List)
            {
                if (targetPlayer.Nickname.Equals(targetMention))
                {
                    target = targetPlayer;
                    return true;
                }
            }

            target = null;
            return false;
        }

        private bool TryFindCamera(Player player, out Camera targetCamera)
        {
            if (player.CurrentRoom.Cameras.Any())
            {
                targetCamera = player.CurrentRoom.Cameras.First();
                return true;
            }

            targetCamera = null;
            return false;
        }
    }
}