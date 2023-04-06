using System;
using System.Collections.Generic;

using Exiled.API.Features;
using Exiled.API.Features.Roles;

using UnityEngine;

using PlayerRoles;

using MEC;

namespace Better079.Components
{
    public class Scp079Extension : MonoBehaviour
    {
        private Player _player;
        private ReferenceHub _playerHub;
        private Scp079Role _playerRole;

        void Start()
        {
            _playerHub = GetComponent<ReferenceHub>();
            _player = Player.Get(_playerHub);
            _playerRole = _player.Role as Scp079Role;

            if (Better079.Instance.Config.AutoTierEnabled)
                Timing.RunCoroutine(AutoTierUpgrade());
        }
        
        public void ForceEscape(Team team)
        {
            if (Better079.Instance.Config.EscapeActivateWarhead)
                Warhead.Start();
            
            if (Better079.Instance.Config.EscapeBypassEnabled)
                foreach (Player player in Player.Get(team))
                    player.IsBypassModeEnabled = true;

            if (Better079.Instance.Config.EscapeCassie != String.Empty)
                Cassie.Message(Better079.Instance.Config.EscapeCassie, isSubtitles: false);
            
            if (Better079.Instance.Config.EscapeForceclass)
                _player.RoleManager.ServerSetRole(RoleTypeId.Spectator, RoleChangeReason.Escaped, RoleSpawnFlags.All);
        }

        public void CallSystemGlitch()
        {
            _playerRole.Energy -= Better079.Instance.Config.Scp2179EnergyLost;
            
            _playerRole.LoseSignal(15f);
        }

        private IEnumerator<float> AutoTierUpgrade()
        {
            yield return Timing.WaitForSeconds(Better079.Instance.Config.AutoTierTime);

            _playerRole.Level = (byte)Better079.Instance.Config.AutoTierLevel;
        }
    }
}