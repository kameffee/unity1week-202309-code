using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Kameffee.Extensions;
using Unity1week202309.InGame.Move;
using Unity1week202309.Talk;
using VContainer.Unity;

namespace Unity1week202309.InGame.LinearMoveGame
{
    public class GameLoop : Presenter, IInitializable, IAsyncStartable
    {
        private readonly ButtonObjectView _buttonObjectView;
        private readonly MoveRouteSettings _moveRouteSettings;
        private readonly SuccessCurrentLevel _successCurrentLevel;
        private readonly RandomMoveSequencer _randomMoveSequencer;
        private readonly LetterBoxTalkUseCase _letterBoxTalkUseCase;

        public GameLoop(
            ButtonObjectView buttonObjectView,
            SuccessCurrentLevel successCurrentLevel,
            RandomMoveSequencer randomMoveSequencer,
            MoveRouteSettings moveRouteSettings,
            LetterBoxTalkUseCase letterBoxTalkUseCase)
        {
            _buttonObjectView = buttonObjectView;
            _successCurrentLevel = successCurrentLevel;
            _randomMoveSequencer = randomMoveSequencer;
            _moveRouteSettings = moveRouteSettings;
            _letterBoxTalkUseCase = letterBoxTalkUseCase;
        }

        public void Initialize()
        {
            _randomMoveSequencer.Initialize(_moveRouteSettings.MoveRouteDataList.ToArray());
            _randomMoveSequencer.AutoMoveStart().Forget();

            _buttonObjectView.SetInteractable(false);
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            await _letterBoxTalkUseCase.ShowAsync(cancellation);
            await _letterBoxTalkUseCase.TalkAsync(3001, cancellation);
            await _letterBoxTalkUseCase.HideAsync(cancellation);

            _buttonObjectView.SetInteractable(true);

            await _buttonObjectView.OnClickAsObservable()
                .ToUniTask(true, cancellationToken: cancellation);

            _randomMoveSequencer.AutoMoveStop();

            await _buttonObjectView.OnClickAsObservable()
                .ToUniTask(true, cancellationToken: cancellation);

            await GameClear();
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