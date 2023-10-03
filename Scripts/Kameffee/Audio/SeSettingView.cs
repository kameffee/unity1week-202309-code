using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace Kameffee.Audio
{
    public class SeSettingView : MonoBehaviour
    {
        [SerializeField]
        private Slider _slider;

        public void SetVolume(AudioVolume volume) => _slider.value = volume.Value;

        public IObservable<float> OnChangeVolumeAsObservable() => _slider.OnValueChangedAsObservable();

        public IObservable<Unit> OnPointerUpAsObservable() => _slider.OnPointerUpAsObservable().AsUnitObservable();
    }
}