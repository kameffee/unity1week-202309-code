using System.Threading;
using Cysharp.Threading.Tasks;
using Kameffee.Audio;
using Kameffee.Extensions;
using UniRx;
using Unity1week202309.InGame.Move;
using Unity1week202309.Talk;
using VContainer.Unity;

namespace Unity1week202309.InGame.FastMoveGame
{
    public class GameLoop : Presenter, IInitializable, IAsyncStartable
    {
        private readonly SuccessCurrentLevel _successCurrentLevel;
        private readonly ButtonObjectView _buttonObjectView;
        private readonly AutoLoopMove _autoLoopMove;
        private readonly MoveRouteData _moveRouteData;
        private readonly AudioSettingsService _audioSettingsService;
        private readonly AudioPlayer _audioPlayer;
        private readonly LetterBoxTalkUseCase _letterBoxTalkUseCase;

        public GameLoop(
            ButtonObjectView buttonObjectView,
            AutoLoopMove autoLoopMove,
            MoveRouteData moveRouteData,
            AudioSettingsService audioSettingsService,
            SuccessCurrentLevel successCurrentLevel,
            AudioPlayer audioPlayer,
            LetterBoxTalkUseCase letterBoxTalkUseCase)
        {
            _buttonObjectView = buttonObjectView;
            _autoLoopMove = autoLoopMove;
            _moveRouteData = moveRouteData;
            _audioSettingsService = audioSettingsService;
            _successCurrentLevel = successCurrentLevel;
            _audioPlayer = audioPlayer;
            _letterBoxTalkUseCase = letterBoxTalkUseCase;
        }

        public void Initialize()
        {
            _audioPlayer.PlayBgm("InGame/DanceMusic");
            _buttonObjectView.SetClickable(false);

            _autoLoopMove.Initialize(_moveRouteData);
            _autoLoopMove.MoveStart();

            _buttonObjectView.OnClickAsObservable()
                .Subscribe(_ => GameClear().Forget())
                .AddTo(this);

            _audioSettingsService.BgmVolume
                .Select(volume => volume.IsZero())
                .Subscribe(isZero =>
                {
                    // BGMの音量が0のときは動きを止める
                    _autoLoopMove.SetSpeed(isZero ? 0f : 1f);

                    // BGMが鳴っていないときはボタンを押せるようにする
                    _buttonObjectView.SetClickable(isZero);
                })
                .AddTo(this);
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            await _letterBoxTalkUseCase.ShowAsync(cancellation);

            if (_autoLoopMove.IsMoving)
            {
                await _letterBoxTalkUseCase.TalkAsync(5001, cancellation);
            }
            else
            {
                await _letterBoxTalkUseCase.TalkAsync(5101, cancellation);
                await UniTask.WaitUntil(() => _autoLoopMove.IsMoving, cancellationToken: cancellation);
                await _letterBoxTalkUseCase.TalkAsync(5102, cancellation);
            }

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