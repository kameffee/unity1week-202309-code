using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Kameffee.Scenes
{
    [Serializable]
    public class ScenesLifetimeScope
    {
        [SerializeField]
        private SceneTransitionView _transitionViewPrefab;

        public void Configure(IContainerBuilder builder)
        {
            builder.Register<SceneLoader>(Lifetime.Singleton);
            builder.RegisterComponentInNewPrefab<SceneTransitionView>(_transitionViewPrefab, Lifetime.Singleton)
                .DontDestroyOnLoad();
        }
    }
}