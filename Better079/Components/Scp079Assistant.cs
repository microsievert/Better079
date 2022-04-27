using System.Collections.Generic;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using MEC;
using UnityEngine;

namespace Better079.Components
{
    public class Scp079Assistant : MonoBehaviour
    {
        private Player _player;
        private Escape _escape;
        private List<Player> _scp079List;

        void Start()
        {
            _escape = GetComponent<Escape>();
            _player = Player.Get(GetComponent<ReferenceHub>());
            _scp079List = Player.Get(RoleType.Scp079).ToList();

            Timing.RunCoroutine(EscapeChecker());
        }

        public IEnumerator<float> DropCountdown(Item item)
        {
            _player.ShowHint(Better079.Instance.Translation.DeviceDropMessage.Replace("{time}", Better079.Instance.Config.CopyTakeTime.ToString()));
            
            yield return Timing.WaitForSeconds(Better079.Instance.Config.CopyTakeTime);

            if (!_player.HasItem(item))
                Destroy(this);
        }

        private IEnumerator<float> EscapeChecker()
        {
            for (;;)
            {
                yield return Timing.WaitForSeconds(0.6f);

                if (_player.Zone != ZoneType.Surface)
                    continue;

                if (Vector3.Distance(transform.position, _escape.worldPosition) <= (Escape.radius + 5.5f) && _scp079List.Count != 0)
                {
                    foreach (Player scp079 in _scp079List)
                        scp079.GameObject.GetComponent<Scp079Extension>().ForceEscape(_player.Role.Team);

                    Destroy(this);
                }
            }
        }
    }
}