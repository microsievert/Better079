using Exiled.API.Features.Roles;
using Exiled.Events.EventArgs;
using Better079.Components;
using MEC;

namespace Better079.Events
{
    public class PlayerHandlers
    {
        public void OnSpawning(SpawningEventArgs ev)
        {
            if (!ev.Player.Role.Is<Scp079Role>(out _))
                return;

            if (Better079.Instance.Config.Scp079SpawnMessageEnabled)
            {
                Timing.CallDelayed(10f, () =>
                {
                    if (ev.Player != null) 
                        ev.Player.ShowHint(Better079.Instance.Translation.SpawnMessage);
                });
            }

            ev.Player.GameObject.AddComponent<Scp079Extension>();
        }

        public void OnDroppingItem(DroppingItemEventArgs ev)
        {
            if (ev.Item.Type != ItemType.KeycardChaosInsurgency)
                return;

            if (ev.Player.GameObject.TryGetComponent(out Scp079Assistant assistantScript))
                Timing.RunCoroutine(assistantScript.DropCountdown(ev.Item));
        }
    }
}