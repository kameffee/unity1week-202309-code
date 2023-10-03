using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Kameffee.Audio;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace Unity1week202309.InGame.Config
{
    public class ConfigWindowView : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _window;

        [SerializeField]
        private CanvasGroup _canvasGroup;

        [SerializeField]
        private Button _closeButton;

        [Space]
        [SerializeField]
        private AudioSettingView _audioSettingView;

        [SerializeField]
        private string _seConfigSeKey;

        [Space]
        [SerializeField]
        private CursorSizeSettingView _cursorSizeSettingView;

        [SerializeField]
        private StartButtonSettingsView _startButtonSettingsView;

        [Inject]
        private readonly AudioPlayer _audioPlayer;

        private void Awake()
        {
            _canvasGroup.alpha = 0f;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        private void Start()
        {
            var lifetimeScope = LifetimeScope.Find<LifetimeScope>();
            lifetimeScope.Container.Inject(this);

            _audioSettingView.OnPointerUpSeVolumeAsObservable()
                .Subscribe(_ => _audioPlayer.PlaySe(_seConfigSeKey))
                .AddTo(this);
        }

        public async UniTask OpenAsync(CancellationToken cancellationToken = default)
        {
            gameObject.SetActive(true);

            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;

            var sequence = DOTween.Sequence();
            sequence.Join(_window.DOAnchorPosY(-48f, 0.16f)
                .From()
                .SetEase(Ease.OutBack));
            sequence.Join(_canvasGroup.DOFade(1f, 0f)
                .SetEase(Ease.Linear));
            sequence.WithCancellation(cancellationToken);
            await sequence.Play();
        }

        public async UniTask CloseAsync(CancellationToken cancellationToken = default)
        {
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;

            await _canvasGroup.DOFade(0f, 0f)
                .SetEase(Ease.Linear)
                .WithCancellation(cancellationToken);

            gameObject.SetActive(false);
        }

        public void ApplyViewModel(ViewModel viewModel)
        {
            _audioSettingView.ApplyViewModel(viewModel.AudioSettingViewModel);
            _cursorSizeSettingView.ApplyViewModel(viewModel.CursorSizeSettingViewModel);
            _startButtonSettingsView.ApplyViewModel(viewModel.StartButtonSettingsViewModel);
        }

        public IObservable<Unit> OnClickCloseObservable() => _closeButton.OnClickAsObservable();

        public IObservable<int> OnChangeCursorSizeObservable() => _cursorSizeSettingView.OnChangeCursorSizeObservable();

        public void SetBgmVolume(AudioVolume volume) => _audioSettingView.SetBgmVolume(volume);

        public void SetSeVolume(AudioVolume volume) => _audioSettingView.SetSfxVolume(volume);

        public IObservable<AudioVolume> OnChangedBgmVolumeAsObservable() =>
            _audioSettingView.OnChangeBgmVolumeAsObservable();

        public IObservable<AudioVolume> OnChangedSeVolumeAsObservable() =>
            _audioSettingView.OnChangeSeVolumeAsObservable();

        public IObservable<bool> OnChangeToggleVisibleStartButtonAsObservable() =>
            _startButtonSettingsView.OnChangeToggleVisibleStartButtonAsObservable();

        public class ViewModel
        {
            public AudioSettingView.ViewModel AudioSettingViewModel { get; }
            public CursorSizeSettingView.ViewModel CursorSizeSettingViewModel { get; }
            public StartButtonSettingsView.ViewModel StartButtonSettingsViewModel { get; }

            public ViewModel(
                AudioSettingView.ViewModel audioSettingViewModel,
                CursorSizeSettingView.ViewModel cursorSizeSettingViewModel,
                StartButtonSettingsView.ViewModel startButtonSettingsViewModel)
            {
                AudioSettingViewModel = audioSettingViewModel;
                CursorSizeSettingViewModel = cursorSizeSettingViewModel;
                StartButtonSettingsViewModel = startButtonSettingsViewModel;
            }
        }
    }
}