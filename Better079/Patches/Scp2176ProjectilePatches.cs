using Better079.Components;
using InventorySystem.Items.ThrowableProjectiles;
using Exiled.API.Features;
using HarmonyLib;
using MEC;

namespace Better079.Patches
{
    [HarmonyPatch(typeof(Scp2176Projectile), nameof(Scp2176Projectile.ServerShatter))]
    internal static class Scp2176ProjectilePatches
    {
        private static void Prefix(Scp2176Projectile __instance)
        {
            if (!Better079.Instance.Config.Scp2179DamageEnabled)
                return;
            
            Room explosionRoom = Map.FindParentRoom(__instance.gameObject);

            if (Better079.Instance.Config.Scp2176DamageRooms.Contains(explosionRoom.Type))
            {
                foreach (Player player in Player.Get(RoleType.Scp079))
                {
                    if (player.GameObject.TryGetComponent(out Scp079Extensions extensionsScript))
                        Timing.RunCoroutine(player.GameObject.GetComponent<Scp079Extensions>().CallSystemGlitch());
                }
            }
        }
    }
}