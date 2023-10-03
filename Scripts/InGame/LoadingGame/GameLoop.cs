using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Kameffee.Extensions;
using UniRx;
using Unity1week202309.Talk;
using VContainer.Unity;

namespace Unity1week202309.InGame.LoadingGame
{
    public class GameLoop : Presenter, IInitializable, IAsyncStartable
    {
        private readonly ButtonObjectView _buttonObjectView;
        private readonly SuccessCurrentLevel _successCurrentLevel;
        private readonly LoadingProgressView _loadingProgressView;
        private readonly LetterBoxTalkUseCase _letterBoxTalkUseCase;

        private float _progress;

        public GameLoop(ButtonObjectView buttonObjectView,
            SuccessCurrentLevel successCurrentLevel,
            LoadingProgressView loadingProgressView,
            LetterBoxTalkUseCase letterBoxTalkUseCase)
        {
            _buttonObjectView = buttonObjectView;
            _successCurrentLevel = successCurrentLevel;
            _loadingProgressView = loadingProgressView;
            _letterBoxTalkUseCase = letterBoxTalkUseCase;
        }

        public void Initialize()
        {
            _loadingProgressView.SetProgress(0);

            _buttonObjectView.Hide();
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            await _letterBoxTalkUseCase.ShowAsync(cancellation);
            await _letterBoxTalkUseCase.TalkAsync(2001, cancellation);

            _buttonObjectView.Show();

            await _letterBoxTalkUseCase.TalkAsync(2002, cancellation);
            await _letterBoxTalkUseCase.HideAsync(cancellation);

            using (var compositeDisposable = new CompositeDisposable().AddTo(this))
            {
                _buttonObjectView.OnClickAsObservable()
                    .Subscribe(_ =>
                    {
                        _progress += 0.02f;
                        _loadingProgressView.SetProgress(_progress);
                    })
                    .AddTo(compositeDisposable);

                await UniTask.WaitUntil(() => _progress >= 1, cancellationToken: cancellation);
            }

            await UniTask.Delay(TimeSpan.FromSeconds(0.1f), cancellationToken: cancellation);

            _buttonObjectView.OnClickAsObservable()
                .Take(1)
                .Subscribe(_ => GameClear().Forget())
                .AddTo(this);
        }

        private async UniTask GameClear()
        {
            _successCurrentLevel.Success();
            
            if (_letterBoxTalkUseCase.IsShow)
            {
                await _letterBoxTalkUseCase.HideAsync();
            }

            await _successCurrentLevel.LoadNextScene();
        }
    }
}