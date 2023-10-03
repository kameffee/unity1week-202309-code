using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Kameffee.Audio
{
    [Serializable]
    public class AudioLifetimeScope
    {
        [SerializeField]
        private AudioResource _audioResource;

        public void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance<AudioResource>(_audioResource);

            builder.Register<AudioResourceLoader>(Lifetime.Singleton);
            builder.Register<AudioPlayer>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();

            builder.Register<AudioSettingsService>(Lifetime.Singleton);
            builder.Register<CreateAudioSettingViewModelUseCase>(Lifetime.Singleton);

            builder.RegisterComponentOnNewGameObject<BgmPlayer>(Lifetime.Singleton).DontDestroyOnLoad();
            builder.RegisterComponentOnNewGameObject<SePlayer>(Lifetime.Singleton).DontDestroyOnLoad();
        }
    }
}