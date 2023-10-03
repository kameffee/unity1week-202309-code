using Kameffee.Audio;
using Kameffee.Scenes;
using Unity1week202309.InGame.MouseCursor;
using Unity1week202309.Talk;
using Unity1week202309.TapEffect;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Unity1week202309
{
    public class RootLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private ScenesLifetimeScope _scenesLifetimeScope;

        [SerializeField]
        private InGameLifetimeScope _inGameLifetimeScope;

        [SerializeField]
        private AudioLifetimeScope _audioLifetimeScope;

        [SerializeField]
        private LetterBoxTalkWindowLifetimeScope _letterBoxTalkWindowLifetimeScope;

        [SerializeField]
        private TapEffectLifetimeScope _tapEffectLifetimeScope;

        protected override void Configure(IContainerBuilder builder)
        {
            _scenesLifetimeScope.Configure(builder);
            _inGameLifetimeScope.Configure(builder);
            _audioLifetimeScope.Configure(builder);
            _letterBoxTalkWindowLifetimeScope.Configure(builder);
            _tapEffectLifetimeScope.Configure(builder);

            builder.RegisterComponentOnNewGameObject<AudioListener>(Lifetime.Singleton, "AudioListener")
                .DontDestroyOnLoad();

            builder.RegisterBuildCallback(resolver => resolver.Resolve<AudioListener>());
        }
    }
}