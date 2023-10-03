using Kameffee.License;
using Unity1week202309.InGame;
using Unity1week202309.InGame.Config;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Unity1week202309.Title
{
    public class TitleLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private LicenseView _licenseViewPrefab;

        [SerializeField]
        private ConfigButtonLifetimeScope _configButtonLifetimeScope;

        protected override void Configure(IContainerBuilder builder)
        {
            _configButtonLifetimeScope.Configure(builder);

            builder.RegisterComponentInHierarchy<Camera>();
            builder.RegisterEntryPoint<InputPresenter>();

            builder.RegisterComponentInHierarchy<TitleView>();
            builder.RegisterEntryPoint<TitleEntryPoint>();

            builder.RegisterComponentInHierarchy<ButtonObjectView>();

            builder.Register<GetLicenseTextUseCase>(Lifetime.Scoped).WithParameter("License");
            builder.Register<LicensePresenter>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
            builder.RegisterComponentInNewPrefab<LicenseView>(_licenseViewPrefab, Lifetime.Scoped);
        }
    }
}