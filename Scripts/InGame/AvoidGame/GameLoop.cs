using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Kameffee.Audio;
using Kameffee.Extensions;
using Unity1week202309.Talk;
using VContainer.Unity;
using UniRx;

namespace Unity1week202309.InGame.AvoidGame
{
    public class GameLoop : Presenter, IInitializable, IAsyncStartable
    {
        private readonly SuccessCurrentLevel _successCurrentLevel;
        private readonly ButtonObjectView _buttonObjectView;
        private readonly LetterBoxTalkUseCase _letterBoxTalkUseCase;
        private readonly AudioSettingsService _audioSettingsService;
        private readonly AudioPlayer _audioPlayer;
        private readonly AvoidMove _avoidMove;

        public GameLoop(
            SuccessCurrentLevel successCurrentLevel,
            ButtonObjectView buttonObjectView,
            LetterBoxTalkUseCase letterBoxTalkUseCase,
            AudioPlayer audioPlayer,
            AudioSettingsService audioSettingsService,
            AvoidMove avoidMove)
        {
            _successCurrentLevel = successCurrentLevel;
            _buttonObjectView = buttonObjectView;
            _letterBoxTalkUseCase = letterBoxTalkUseCase;
            _audioSettingsService = audioSettingsService;
            _audioPlayer = audioPlayer;
            _avoidMove = avoidMove;
        }

        public void Initialize()
        {
            _buttonObjectView.gameObject.SetActive(false);
            _buttonObjectView.SetInteractable(true);
            _buttonObjectView.OnClickAsObservable()
                .Subscribe(_ => GameClear().Forget())
                .AddTo(this);

            _audioSettingsService.BgmVolume
                .FirstOrDefault(volume => !volume.IsZero())
                .Subscribe(_ =>
                {
                    _buttonObjectView.gameObject.SetActive(true);
                    _avoidMove.Move();
                })
                .AddTo(this);
        }


        public async UniTask StartAsync(CancellationToken cancellation)
        {
            await _audioPlayer.PlayBgmAsync("InGame/Main");
            await _letterBoxTalkUseCase.ShowAsync(cancellation);
            await _letterBoxTalkUseCase.TalkAsync(6001, cancellation);
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