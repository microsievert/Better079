using Better079.Events;
using Exiled.API.Features;
using System;

namespace Better079
{
    public class Better079 : Plugin<Config>
    {
        public override string Author => "microsievert";
        public override Version RequiredExiledVersion => new Version("5.0.0");

        public static Better079 Instance;

        private PlayerHandlers _playerHandlers;

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
            Exiled.Events.Handlers.Player.Died += _playerHandlers.OnDied;
            Exiled.Events.Handlers.Player.ChangingRole += _playerHandlers.OnChangingRole;
        }

        private void UnregisterEvents()
        {
            Exiled.Events.Handlers.Player.Spawning -= _playerHandlers.OnSpawning;
            Exiled.Events.Handlers.Player.Died -= _playerHandlers.OnDied;
            Exiled.Events.Handlers.Player.ChangingRole -= _playerHandlers.OnChangingRole;

            _playerHandlers = null;
        }
    }
}
