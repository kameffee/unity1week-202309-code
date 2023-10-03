using System.Collections.Generic;
using Unity1week202309.InGame.Config;
using Unity1week202309.InGame.Move;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Unity1week202309.InGame.FastMoveGame
{
    public class FastMoveLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private ConfigButtonLifetimeScope _configButtonLifetimeScope;

        [SerializeField]
        private MoveRouteData _moveRouteData;

        protected override void Configure(IContainerBuilder builder)
        {
            _configButtonLifetimeScope.Configure(builder);

            builder.RegisterComponentInHierarchy<ButtonObjectView>();
            builder.RegisterComponentInHierarchy<AutoLoopMove>();
            builder.RegisterInstance(_moveRouteData);
            builder.RegisterEntryPoint<GameLoop>();
        }
    }
}