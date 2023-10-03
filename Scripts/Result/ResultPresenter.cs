using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Kameffee.Audio;
using Kameffee.Extensions;
using Kameffee.Scenes;
using VContainer.Unity;
using UniRx;

namespace Unity1week202309.Result
{
    public class ResultPresenter : Presenter, IInitializable, IAsyncStartable
    {
        private readonly ResultView _resultView;
        private readonly SceneLoader _sceneLoader;
        private readonly AudioPlayer _audioPlayer;

        public ResultPresenter(
            ResultView resultView,
            SceneLoader sceneLoader,
            AudioPlayer audioPlayer)
        {
            _resultView = resultView;
            _sceneLoader = sceneLoader;
            _audioPlayer = audioPlayer;
        }

        public void Initialize()
        {
            _resultView.OnClickTitleAsObservable()
                .Subscribe(_ => LoadTitleAsync().Forget())
                .AddTo(this);
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: cancellation);
            await _audioPlayer.PlayBgmAsync("Result/Bgm");
            await UniTask.Delay(TimeSpan.FromSeconds(2f), cancellationToken: cancellation);
            await _resultView.ShowAsync(cancellation);
        }

        private async UniTask LoadTitleAsync()
        {
            await UniTask.WhenAll(
                _audioPlayer.StopBgm(),
                _sceneLoader.LoadAsync(SceneDefine.Title)
            );
        }
    }
}