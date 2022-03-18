using CommandSystem;
using UnityEngine;
using RemoteAdmin;
using System;
using Exiled.API.Features;
using System.Collections.Generic;
using MapGeneration.Distributors;
using MEC;

namespace Better079.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class DropGenerators : ICommand
    {
        public string Command => "gsave";
        public string Description => "Disabling all engaged generators";

        public string[] Aliases => Array.Empty<string>();

        private bool _dropAllowed = true;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!Better079.Instance.Config.GeneratorsDropEnabled)
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
            
            if (_dropAllowed)
            {
                player.GameObject.GetComponent<Scp079PlayerScript>().Mana = 0f;

                foreach (Scp079Generator generator in GameObject.FindObjectsOfType<Scp079Generator>())
                {
                    generator.Activating = false;

                    Map.TurnOffAllLights(1f);
                }

                Map.PlayAmbientSound(6);
                Map.PlayIntercomSound(false);

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
