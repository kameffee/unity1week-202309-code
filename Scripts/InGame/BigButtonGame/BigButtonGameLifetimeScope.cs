using Unity1week202309.InGame.Config;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Unity1week202309.InGame.BigButtonGame
{
    public class BigButtonGameLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private ConfigButtonLifetimeScope _configButtonLifetimeScope;

        protected override void Configure(IContainerBuilder builder)
        {
            _configButtonLifetimeScope.Configure(builder);

            builder.RegisterComponentInHierarchy<ButtonObjectView>();
            builder.RegisterEntryPoint<GameLoop>();
        }
    }
}