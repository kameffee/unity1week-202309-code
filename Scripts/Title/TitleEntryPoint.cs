using Cysharp.Threading.Tasks;
using Kameffee.Audio;
using Kameffee.Extensions;
using Kameffee.License;
using Kameffee.Scenes;
using VContainer.Unity;
using UniRx;
using Unity1week202309.InGame;
using UnityEngine;

namespace Unity1week202309.Title
{
    public class TitleEntryPoint : Presenter, IInitializable, IStartable
    {
        private readonly TitleView _titleView;
        private readonly ButtonObjectView _buttonObjectView;
        private readonly SceneLoader _sceneLoader;
        private readonly LicensePresenter _licensePresenter;
        private readonly AudioPlayer _audioPlayer;
        private readonly Camera _mainCamera;

        public TitleEntryPoint(
            TitleView titleView,
            SceneLoader sceneLoader,
            LicensePresenter licensePresenter,
            ButtonObjectView buttonObjectView,
            AudioPlayer audioPlayer)
        {
            _titleView = titleView;
            _sceneLoader = sceneLoader;
            _licensePresenter = licensePresenter;
            _buttonObjectView = buttonObjectView;
            _audioPlayer = audioPlayer;
        }

        public void Initialize()
        {
            _buttonObjectView.OnClickAsObservable()
                .Subscribe(_ => StartGame().Forget())
                .AddTo(this);

            _titleView.OnLicenseButtonClickAsObservable()
                .Subscribe(_ => _licensePresenter.ShowAsync().Forget())
                .AddTo(this);
        }

        private async UniTask StartGame()
        {
            await _sceneLoader.LoadAsync(SceneDefine.InGame);
        }

        public void Start()
        {
            _audioPlayer.PlayBgm("InGame/Main");
        }
    }
}