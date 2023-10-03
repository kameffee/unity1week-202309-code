using System;
using UniRx;
using UnityEngine;

namespace Kameffee.Audio
{
    public class AudioSettingView : MonoBehaviour
    {
        [SerializeField]
        private BgmSettingView _bgmSettingView;

        [SerializeField]
        private SeSettingView _seSettingView;

        public void ApplyViewModel(ViewModel viewModel)
        {
            if (viewModel == null)
            {
                gameObject.SetActive(false);
                return;
            }

            _bgmSettingView.SetVolume(viewModel.BgmVolume);
            _seSettingView.SetVolume(viewModel.SeVolume);
        }

        public void SetBgmVolume(AudioVolume volume) => _bgmSettingView.SetVolume(volume);

        public void SetSfxVolume(AudioVolume volume) => _seSettingView.SetVolume(volume);

        public IObservable<AudioVolume> OnChangeBgmVolumeAsObservable() => _bgmSettingView
            .OnChangeVolumeAsObservable()
            .Select(volume => new AudioVolume(volume));

        public IObservable<AudioVolume> OnChangeSeVolumeAsObservable() => _seSettingView
            .OnChangeVolumeAsObservable()
            .Select(volume => new AudioVolume(volume));

        public IObservable<Unit> OnPointerUpSeVolumeAsObservable() => _seSettingView.OnPointerUpAsObservable();

        public class ViewModel
        {
            public AudioVolume BgmVolume { get; }
            public AudioVolume SeVolume { get; }

            public ViewModel(AudioVolume bgmVolume, AudioVolume seVolume)
            {
                BgmVolume = bgmVolume;
                SeVolume = seVolume;
            }
        }
    }
}