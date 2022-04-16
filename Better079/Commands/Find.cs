using System;
using System.Collections.Generic;
using System.Linq;
using CommandSystem;
using Exiled.API.Features;
using Exiled.API.Features.Roles;
using RemoteAdmin;
using MEC;
using Random = UnityEngine.Random;

namespace Better079.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Find : ICommand
    {
        public string Command => "find";
        public string Description => "Teleport to random player room";

        public string[] Aliases => Array.Empty<string>();

        private bool _findAllowed = true;

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

            if (!_findAllowed)
            {
                response = "Error. Wait before use this ability again";
                return false;
            }

            List<Player> playerList = Player.List.ToList();
            
            Player targetPlayer = SelectRandomPlayer(playerList);

            bool playerSelected = false;
            for (int i = 0; i < playerList.Count; i++)
            {
                if (targetPlayer.CurrentRoom != null && targetPlayer.CurrentRoom.Cameras != null && targetPlayer != caller)
                {
                    playerSelected = true;
                    break;
                }

                targetPlayer = SelectRandomPlayer(playerList);
            }

            if (!playerSelected)
            {
                response = "Error! The system cannot find a suitable player. Try again.";
                return false;
            }

            scp079Role.Camera = targetPlayer.CurrentRoom.Cameras.First();
            
            _findAllowed = false;

            Timing.CallDelayed(Better079.Instance.Config.FindCooldown, () => _findAllowed = true);

            response = "Switching camera...";
            return true;
        }

        private Player SelectRandomPlayer(List<Player> list) => list[Random.Range(0, list.Count)];
    }
}