using Exiled.API.Features.Roles;
using Exiled.Events.EventArgs;
using Better079.Components;
using MEC;
using Mirror;
using UnityEngine;

namespace Better079.Events
{
    public class PlayerHandlers
    {
        public void OnSpawning(SpawningEventArgs ev)
        {
            if (!ev.Player.Role.Is<Scp079Role>(out _))
                return;
            
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