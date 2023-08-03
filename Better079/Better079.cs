using System;

using Exiled.API.Features;

using HarmonyLib;

using Better079.Events;

namespace Better079
{
    public class Better079 : Plugin<Config, Translation>
    {
        public override string Author => "microsievert";
        public override string Name => "Better079";
        
        public override Version RequiredExiledVersion { get; } = new ("6.0.0");

        public static Better079 Instance;

        private PlayerHandlers _playerHandlers;
        
        private Harmony _harmonyInstance;

        public override void OnEnabled()
        {
            Instance = this;

            RegisterEvents();

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            UnregisterEvents();

            Instance = null;

            base.OnDisabled();
        }

        // Events

        private void RegisterEvents()
        {
            _playerHandlers = new PlayerHandlers();

            Exiled.Events.Handlers.Player.Spawning += _playerHandlers.OnSpawning;
            Exiled.Events.Handlers.Player.DroppingItem += _playerHandlers.OnDroppingItem;
        }

        private void UnregisterEvents()
        {
            Exiled.Events.Handlers.Player.Spawning -= _playerHandlers.OnSpawning;
            Exiled.Events.Handlers.Player.DroppingItem -= _playerHandlers.OnDroppingItem;

            _playerHandlers = null;
        }
    }
}
