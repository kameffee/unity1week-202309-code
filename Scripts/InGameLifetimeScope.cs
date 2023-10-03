using System;
using Unity1week202309.InGame;
using Unity1week202309.InGame.MouseCursor;
using Unity1week202309.Talk;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Unity1week202309
{
    [Serializable]
    public class InGameLifetimeScope
    {
        [SerializeField]
        private MouseCursorSettings _mouseCursorSettings;

        [SerializeField]
        private TalkMasterDataStore _talkMasterDataStore;
        
        [SerializeField]
        private MouseCursorCanvasView _mouseCursorCanvasViewPrefab;

        public void Configure(IContainerBuilder builder)
        {
            // MouseCursor
            builder.Register<ChangeMouseCursorUseCase>(Lifetime.Singleton);
            builder.Register<MouseCursorService>(Lifetime.Singleton);
            builder.RegisterInstance<MouseCursorSettings>(_mouseCursorSettings);
            builder.RegisterComponentInNewPrefab(_mouseCursorCanvasViewPrefab, Lifetime.Singleton)
                .DontDestroyOnLoad();
            builder.RegisterEntryPoint<MouseCursorPresenter>();
            

            // Level
            builder.Register<SuccessCurrentLevel>(Lifetime.Singleton);
            builder.Register<LevelService>(Lifetime.Singleton);

            // Talk
            builder.Register<TalkService>(Lifetime.Singleton);
            builder.Register<TalkMasterDataRepository>(Lifetime.Singleton);
            builder.RegisterInstance<TalkMasterDataStore>(_talkMasterDataStore);

            builder.RegisterBuildCallback(resolver =>
                resolver.Resolve<ChangeMouseCursorUseCase>().ChangeTo(MouseCursorType.Hand));
        }
    }
}