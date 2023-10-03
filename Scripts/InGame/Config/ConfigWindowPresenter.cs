using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Kameffee.Audio;
using Kameffee.Extensions;
using VContainer.Unity;
using UniRx;
using Unity1week202309.InGame.MouseCursor;

namespace Unity1week202309.InGame.Config
{
    public class ConfigWindowPresenter : Presenter, IInitializable
    {
        private readonly ConfigButtonView _configButtonView;
        private readonly CreateConfigWindowViewModelUseCase _createConfigWindowViewModelUseCase;
        private readonly MouseCursorService _mouseCursorService;
        private readonly ChangeMouseCursorUseCase _changeMouseCursorUseCase;
        private readonly Func<ConfigWindowView> _configWindowViewFactory;
        private readonly AudioSettingsService _audioSettingsService;
        private readonly VisibleForStartButtonUseCase _visibleForStartButtonUseCase;
        private ConfigWindowView _configWindowView;

        public ConfigWindowPresenter(
            ConfigButtonView configButtonView,
            Func<ConfigWindowView> configWindowViewFactory,
            CreateConfigWindowViewModelUseCase createConfigWindowViewModelUseCase,
            MouseCursorService mouseCursorService,
            ChangeMouseCursorUseCase changeMouseCursorUseCase,
            AudioSettingsService audioSettingsService,
            VisibleForStartButtonUseCase visibleForStartButtonUseCase)
        {
            _configButtonView = configButtonView;
            _configWindowViewFactory = configWindowViewFactory;
            _createConfigWindowViewModelUseCase = createConfigWindowViewModelUseCase;
            _mouseCursorService = mouseCursorService;
            _changeMouseCursorUseCase = changeMouseCursorUseCase;
            _audioSettingsService = audioSettingsService;
            _visibleForStartButtonUseCase = visibleForStartButtonUseCase;
        }

        public void Initialize()
        {
            _configButtonView.OnClickObservable()
                .Subscribe(_ => OpenAsync().Forget())
                .AddTo(this);
        }

        private ConfigWindowView Create()
        {
            var view = _configWindowViewFactory.Invoke();
            var viewMode = _createConfigWindowViewModelUseCase.Create();
            view.ApplyViewModel(viewMode);

            view.OnClickCloseObservable()
                .Subscribe(_ => CloseAsync().Forget())
                .AddTo(view)
                .AddTo(this);

            view.OnChangeCursorSizeObservable()
                .Skip(1)
                .Select(index => _mouseCursorService.IndexToSize(index))
                .Subscribe(size => _changeMouseCursorUseCase.ChangeSize(size))
                .AddTo(view)
                .AddTo(this);

            _audioSettingsService.BgmVolume
                .Subscribe(volume => view.SetBgmVolume(volume))
                .AddTo(this);

            _audioSettingsService.SeVolume
                .Subscribe(volume => view.SetSeVolume(volume))
                .AddTo(this);

            view.OnChangedBgmVolumeAsObservable()
                .Subscribe(value => _audioSettingsService.SetBgmVolume(value))
                .AddTo(this);

            view.OnChangedSeVolumeAsObservable()
                .Subscribe(value => _audioSettingsService.SetSeVolume(value))
                .AddTo(this);

            view.OnChangeToggleVisibleStartButtonAsObservable()
                .Subscribe(visible => _visibleForStartButtonUseCase.SetVisible(visible))
                .AddTo(this);

            return view;
        }

        public async UniTask OpenAsync(CancellationToken cancellationToken = default)
        {
            if (_configWindowView == null)
            {
                _configWindowView = Create();
            }

            await _configWindowView.OpenAsync(cancellationToken);
        }

        public async UniTask CloseAsync(CancellationToken cancellationToken = default)
        {
            await _configWindowView.CloseAsync(cancellationToken);
        }
    }
}