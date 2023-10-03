using Unity1week202309.InGame.Config;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Unity1week202309.InGame.Intro
{
    public class IntroLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private ConfigButtonLifetimeScope _configButtonLifetimeScope;

        protected override void Configure(IContainerBuilder builder)
        {
            _configButtonLifetimeScope.Configure(builder);

            builder.RegisterComponentInHierarchy<Camera>();
            builder.RegisterEntryPoint<InputPresenter>();

            builder.RegisterComponentInHierarchy<ButtonObjectView>();
            builder.Register<VisibleForStartButtonUseCase>(Lifetime.Scoped);
            builder.RegisterEntryPoint<StartButtonPresenter>();

            builder.RegisterEntryPoint<GameLoop>();
        }
    }
}