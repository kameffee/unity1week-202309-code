using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Unity1week202309.TapEffect
{
    [Serializable]
    public class TapEffectLifetimeScope
    {
        [SerializeField]
        private TapEffectView _tapEffectViewPrefab;
        
        public void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInNewPrefab(_tapEffectViewPrefab, Lifetime.Singleton)
                .DontDestroyOnLoad();
            builder.RegisterEntryPoint<TapEffectPresenter>();
        }
    }
}