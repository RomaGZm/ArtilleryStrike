
using AS.Gameplay.Core.Input;
using GameAssets.Gameplay.Player;
using GameAssets.Gameplay.Player.Code;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace AS.Gameplay.Core.Gameplay
{

    public sealed class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private PlayerView playerView;
        [SerializeField] private Transform projectileContainer;
        protected override void Configure(IContainerBuilder builder)
        {

            builder.Register<InputService>(Lifetime.Singleton);
            builder.RegisterEntryPoint<PlayerController>(Lifetime.Singleton);

            builder.RegisterInstance(playerView);


        }
    }
}