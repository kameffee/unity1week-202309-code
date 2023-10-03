using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Unity1week202309.InGame.Config
{
    public class ConfigButtonView : MonoBehaviour
    {
        [SerializeField]
        private Button _button;

        public IObservable<Unit> OnClickObservable() => _button.OnClickAsObservable();
    }
}