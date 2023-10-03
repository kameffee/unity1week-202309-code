using System.Threading;
using Cysharp.Threading.Tasks;
using Kameffee.Extensions;
using UniRx;
using Unity1week202309.InGame.MouseCursor;
using Unity1week202309.Talk;
using VContainer.Unity;

namespace Unity1week202309.InGame.BigButtonGame
{
    public class GameLoop : Presenter, IInitializable, IAsyncStartable
    {
        private readonly ButtonObjectView _buttonObjectView;
        private readonly SuccessCurrentLevel _successCurrentLevel;
        private readonly MouseCursorService _mouseCursorService;
        private readonly LetterBoxTalkUseCase _letterBoxTalkUseCase;

        public GameLoop(
            ButtonObjectView buttonObjectView,
            SuccessCurrentLevel successCurrentLevel,
            MouseCursorService mouseCursorService,
            LetterBoxTalkUseCase letterBoxTalkUseCase)
        {
            _buttonObjectView = buttonObjectView;
            _successCurrentLevel = successCurrentLevel;
            _mouseCursorService = mouseCursorService;
            _letterBoxTalkUseCase = letterBoxTalkUseCase;
        }

        public void Initialize()
        {
            // カーソルサイズが最大の時のみ有効にする
            _mouseCursorService.CurrentSize
                .Select(_ => _mouseCursorService.IsMaxSize())
                .Subscribe(isClickable => _buttonObjectView.SetClickable(isClickable))
                .AddTo(this);

            _buttonObjectView.OnClickAsObservable()
                .Where(_ => _mouseCursorService.IsMaxSize())
                .Subscribe(_ => GameClear().Forget())
                .AddTo(this);
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            await _letterBoxTalkUseCase.ShowAsync(cancellation);
            await _letterBoxTalkUseCase.TalkAsync(4001, cancellation);
            await _letterBoxTalkUseCase.HideAsync(cancellation);
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