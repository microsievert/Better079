using System;
using System.Linq;

using Exiled.API.Features;

using PlayerRoles;

using RemoteAdmin;

using CommandSystem;

using MEC;

using Better079.Components;

namespace Better079.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Copy : ICommand
    {
        public string Command => "copy";
        public string Description => "[SCP-079 SUPPORT ABILITY] Allows you to copy SCP-079 and help he escape.";

        public string[] Aliases => Array.Empty<string>();

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!Better079.Instance.Config.CopyEnabled)
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
            Room room = caller.CurrentRoom;

            if (!Better079.Instance.Config.CopyAllowedTeams.Contains(caller.Role.Team))
            {
                response = "You can't cooperate with SCP-079 as this role";
                return false;
            }

            if (room == null || !Better079.Instance.Config.CopyAllowedRooms.Contains(room.Type))
            {
                response = "You can't call this command in this room";
                return false;
            }

            if (caller.CurrentItem == null || caller.CurrentItem.Type != ItemType.KeycardChaosInsurgency)
            {
                response = "Please hold your chaos insurgency device to copy SCP-079";
                return false;
            }

            Timing.CallDelayed(Better079.Instance.Config.CopyTime, () =>
            {
                if (caller.CurrentRoom == null || room != caller.CurrentRoom || caller.CurrentItem == null || caller.CurrentItem.Type != ItemType.KeycardChaosInsurgency)
                {
                    caller.ShowHint(Better079.Instance.Translation.HackFailure, 10f);
                    
                    return;
                }

                if (!Player.Get(RoleTypeId.Scp079).Any())
                {
                    caller.ShowHint(Better079.Instance.Translation.HackFailureNoPlayers, 10f);
                    
                    return;
                }

                caller.GameObject.AddComponent<Scp079Assistant>();
                
                caller.ShowHint(Better079.Instance.Translation.HackSuccessfully, 10f);

                if (Better079.Instance.Config.CopyCassie == String.Empty)
                    return;
                
                Cassie.Message(Better079.Instance.Config.CopyCassie);
            });

            response = "Hack process started. Stay here until process will end.";
            return true;
        }
    }
}