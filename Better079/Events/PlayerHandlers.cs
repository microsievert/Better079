using Exiled.Events.EventArgs;
using Exiled.API.Features;
using System.Collections.Generic;
using Exiled.API.Features.Roles;
using Better079.Utils;
using UnityEngine;
using System.Linq;
using MEC;

namespace Better079.Events
{
    public class PlayerHandlers
    {   
        private Dictionary<Player, CoroutineHandle[]> PlayersCoroutines = new Dictionary<Player, CoroutineHandle[]>();

        public void OnSpawning(SpawningEventArgs ev)
        {
            if (!ev.Player.Role.Is<Scp079Role>(out _))
                return;

            PlayersCoroutines.Add(ev.Player, new CoroutineHandle[] { Timing.RunCoroutine(AbilityProcessor(ev.Player)), Timing.RunCoroutine(TierLevelup(ev.Player)) });
        }

        public void OnDied(DiedEventArgs ev)
        {
            if (ev.Target.Role.Is<Scp079Role>(out _))
                TerminateCheckCoroutines(ev.Target);
        }

        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (ev.Player.Role.Is<Scp079Role>(out _))
                TerminateCheckCoroutines(ev.Player);
        }

        private void TerminateCheckCoroutines(Player player)
        {
            if (!PlayersCoroutines.ContainsKey(player))
                return;

            Timing.KillCoroutines(PlayersCoroutines[player]);
        }

        private IEnumerator<float> AbilityProcessor(Player scp079)
        {
            Scp079PlayerScript scp079Script = scp079.ReferenceHub.scp079PlayerScript;

            yield return Timing.WaitForSeconds(1f);

            List<Vector3> coordinatesPocket = new List<Vector3>();

            for (;;)
            {
                foreach (Player target in Player.List.Where(player => MapUtils.InSameZone(player.GameObject.transform, scp079Script.currentCamera.transform) && player != scp079))
                    coordinatesPocket.Add(target.Position);

                scp079Script.TargetSetupIndicators(scp079.ReferenceHub.networkIdentity.connectionToClient, coordinatesPocket);

                coordinatesPocket.Clear();

                yield return Timing.WaitForSeconds(0.6f);
            }
        }
        private IEnumerator<float> TierLevelup(Player scp079)
        {
            Scp079PlayerScript playerScript = scp079.ReferenceHub.scp079PlayerScript;

            yield return Timing.WaitForSeconds(Better079.Instance.Config.AutoTierTime);

            playerScript.ForceLevel((byte)Better079.Instance.Config.AutoTierLevel, true);
        }
    }
}