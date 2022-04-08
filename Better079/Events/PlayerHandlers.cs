using Exiled.API.Features.Roles;
using Exiled.Events.EventArgs;
using Better079.Components;

namespace Better079.Events
{
    public class PlayerHandlers
    {
        public void OnSpawning(SpawningEventArgs ev)
        {
            if (!ev.Player.Role.Is<Scp079Role>(out _))
                return;

            ev.Player.GameObject.AddComponent<Scp079Extensions>();
        }
    }
}