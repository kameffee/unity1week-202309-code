using VContainer;
using VContainer.Unity;

namespace Unity1week202309.InGame.LoadingGame
{
    public class LoadingGameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<ButtonObjectView>();
            builder.RegisterEntryPoint<GameLoop>();

            builder.RegisterComponentInHierarchy<LoadingProgressView>();
        }
    }
}