using Exiled.API.Features;

using HarmonyLib;

using PlayerRoles;

using InventorySystem.Items.ThrowableProjectiles;

using Better079.Components;

namespace Better079.Patches
{
    [HarmonyPatch(typeof(Scp2176Projectile), nameof(Scp2176Projectile.ServerShatter))]
    internal static class Scp2176ProjectilePatches
    {
        private static void Prefix(Scp2176Projectile __instance)
        {
            if (!Better079.Instance.Config.Scp2179DamageEnabled)
                return;
            
            Room explosionRoom = Map.FindParentRoom(__instance._transform.gameObject);

            if (explosionRoom == null)
                return;

            if (Better079.Instance.Config.Scp2176DamageRooms.Contains(explosionRoom.Type))
                foreach (Player player in Player.Get(RoleTypeId.Scp079))
                    if (player.GameObject.TryGetComponent(out Scp079Extension extensionsScript))
                        extensionsScript.CallSystemGlitch();
        }
    }
}