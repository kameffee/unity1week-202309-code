using VContainer;
using VContainer.Unity;

namespace Unity1week202309.Result
{
    public class ResultLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<ResultView>();
            builder.RegisterEntryPoint<ResultPresenter>();
        }
    }
}