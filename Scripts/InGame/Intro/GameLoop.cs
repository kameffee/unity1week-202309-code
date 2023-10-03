using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Kameffee.Extensions;
using UniRx;
using Unity1week202309.InGame.Config;
using Unity1week202309.Talk;
using VContainer.Unity;

namespace Unity1week202309.InGame.Intro
{
    public class GameLoop : Presenter, IInitializable, IAsyncStartable
    {
        private readonly ButtonObjectView _buttonObjectView;
        private readonly SuccessCurrentLevel _successCurrentLevel;
        private readonly LetterBoxTalkUseCase _letterBoxTalkUseCase;
        private readonly VisibleForStartButtonUseCase _visibleForStartButtonUseCase;

        public GameLoop(
            ButtonObjectView buttonObjectView,
            SuccessCurrentLevel successCurrentLevel,
            LetterBoxTalkUseCase letterBoxTalkUseCase,
            VisibleForStartButtonUseCase visibleForStartButtonUseCase)
        {
            _buttonObjectView = buttonObjectView;
            _successCurrentLevel = successCurrentLevel;
            _letterBoxTalkUseCase = letterBoxTalkUseCase;
            _visibleForStartButtonUseCase = visibleForStartButtonUseCase;
        }

        public void Initialize()
        {
            _buttonObjectView.SetClickable(false);
            _visibleForStartButtonUseCase.SetVisible(false);
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            await _letterBoxTalkUseCase.ShowAsync(cancellation);
            await _letterBoxTalkUseCase.TalkAsync(1001, cancellation);
            await _letterBoxTalkUseCase.HideAsync(cancellation);

            _buttonObjectView.SetClickable(true);
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