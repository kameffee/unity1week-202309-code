using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Unity1week202309.InGame.Config
{
    [Serializable]
    public class ConfigButtonLifetimeScope
    {
        [SerializeField]
        private ConfigWindowView _configWindowViewPrefab;
        
        [SerializeField]
        private ConfigWindowSettings _configWindowSettings;

        public void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance<ConfigWindowSettings>(_configWindowSettings);
            builder.RegisterEntryPoint<ConfigWindowPresenter>(Lifetime.Scoped);
            builder.RegisterComponentInHierarchy<ConfigButtonView>();
            builder.RegisterFactory<ConfigWindowView>(
                resolver => { return () => GameObject.Instantiate(_configWindowViewPrefab); }, Lifetime.Scoped);
            builder.Register<CreateCursorSizeSettingsViewModelUseCase>(Lifetime.Scoped);
            builder.Register<CreateConfigWindowViewModelUseCase>(Lifetime.Scoped);
            builder.Register<VisibleForStartButtonUseCase>(Lifetime.Scoped);
        }
    }
}