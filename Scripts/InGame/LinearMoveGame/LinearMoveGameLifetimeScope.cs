using Unity1week202309.InGame.Config;
using Unity1week202309.InGame.Move;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Unity1week202309.InGame.LinearMoveGame
{
    public class LinearMoveGameLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private ConfigButtonLifetimeScope _configButtonLifetimeScope;

        [SerializeField]
        private MoveRouteSettings _moveRouteSettings;

        protected override void Configure(IContainerBuilder builder)
        {
            _configButtonLifetimeScope.Configure(builder);

            builder.RegisterInstance(_moveRouteSettings);
            builder.RegisterComponentInHierarchy<ButtonObjectView>();
            builder.RegisterComponentInHierarchy<RandomMoveSequencer>();
            builder.RegisterEntryPoint<GameLoop>();
        }
    }
}