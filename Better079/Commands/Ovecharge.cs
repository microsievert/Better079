using System;
using CommandSystem;
using UnityEngine;
using RemoteAdmin;
using Exiled.API.Features;

namespace Better079.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Ovecharge : ICommand
    {
        public string Command => "overcharge";
        public string Description => "Disables all light system";

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
                response = "Only players can call this command";
                return false;
            }

            Player player = Player.Get((sender as PlayerCommandSender).ReferenceHub);

            if (player.Role.Type != RoleType.Scp079)
            {
                response = "Sorry but you can't use this command while you is not SCP-079";
                return true;
            }

            Scp079PlayerScript playerScript = player.ReferenceHub.scp079PlayerScript;

            if (float.TryParse(arguments.At<string>(0), out float time) && playerScript.Mana >= 90)
            {
                if (time > Better079.Instance.Config.OverchargeMaxtime)
                {
                    response = $"Error! Time can't be higher than {Better079.Instance.Config.OverchargeMaxtime} sec";
                    return false;
                }

                Map.TurnOffAllLights(time);

                GameObject.FindObjectOfType<AmbientSoundPlayer>().RpcPlaySound(UnityEngine.Random.Range(6, 7));

                playerScript.Mana -= Better079.Instance.Config.OverchargePrice;

                response = "Command successfully executed by facility servers.";
                return true;
            }

            response = "Command execution error";
            return true;
        }
    }
}