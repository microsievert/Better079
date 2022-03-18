using Exiled.Events.EventArgs;
using Exiled.API.Features;
using System.Collections.Generic;
using Better079.Utils;
using UnityEngine;
using System.Linq;
using MEC;

namespace Better079.Events
{
    public class PlayerHandlers
    {
        private Player _scp079;
        private CoroutineHandle _scp079AbilityProcessorCoroutine;

        public void OnSpawning(SpawningEventArgs ev)
        {
            if (ev.Player.Role != RoleType.Scp079)
                return;

            _scp079 = ev.Player;

            if (Better079.Instance.Config.BetterMapEnabled)
                _scp079AbilityProcessorCoroutine = Timing.RunCoroutine(AbilityProcessor());

            if (Better079.Instance.Config.AutoTierEnabled)
                Timing.RunCoroutine(TierLevelup());
        }

        public void OnDied(DiedEventArgs ev)
        {
            if (ev.Target.Role == RoleType.Scp079)
                TerminateCheckCoroutines();
        }

        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (ev.Player.Role == RoleType.Scp079)
                TerminateCheckCoroutines();
        }

        private void TerminateCheckCoroutines() => Timing.KillCoroutines(_scp079AbilityProcessorCoroutine);

        private IEnumerator<float> AbilityProcessor()
        {
            if (_scp079 == null)
                yield break;

            Scp079PlayerScript scp079Script = _scp079.GameObject.GetComponent<Scp079PlayerScript>();

            yield return Timing.WaitForSeconds(1f);

            List<Vector3> coordinatesPocket = new List<Vector3>();

            for (;;)
            {
                if (_scp079 == null)
                    yield break;

                foreach (Player target in Player.List.Where(x => MapUtils.HaveSameZone(x.GameObject.transform, scp079Script.currentCamera.transform)))
                    coordinatesPocket.Add(target.Position);

                scp079Script.TargetSetupIndicators(_scp079.ReferenceHub.networkIdentity.connectionToClient, coordinatesPocket);

                coordinatesPocket.Clear();

                yield return Timing.WaitForSeconds(0.6f);
            }
        }

        private IEnumerator<float> TierLevelup()
        {
            Scp079PlayerScript playerScript = _scp079.GameObject.GetComponent<Scp079PlayerScript>();

            yield return Timing.WaitForSeconds(Better079.Instance.Config.AutoTierTime);

            if (_scp079 == null)
                yield break;

            playerScript.ForceLevel((byte)Better079.Instance.Config.AutoTierLevel, true);
        }
    }
}