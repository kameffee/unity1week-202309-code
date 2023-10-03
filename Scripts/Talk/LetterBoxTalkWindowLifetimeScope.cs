using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Unity1week202309.Talk
{
    [Serializable]
    public class LetterBoxTalkWindowLifetimeScope
    {
        [SerializeField]
        private LetterBoxTalkView _letterBoxTalkWindowViewPrefab;

        public void Configure(IContainerBuilder builder)
        {
            builder.Register<LetterBoxTalkUseCase>(Lifetime.Singleton);
            builder.RegisterEntryPoint<LetterBoxTalkWindowPresenter>();
            builder.RegisterComponentInNewPrefab(_letterBoxTalkWindowViewPrefab, Lifetime.Singleton)
                .DontDestroyOnLoad();
        }
    }
}