using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Unity1week202309.Talk
{
    public class LetterBoxTalkUseCase
    {
        private readonly ITalkWindowPresenter _talkWindowPresenter;
        private readonly TalkMasterDataRepository _talkMasterDataRepository;

        public LetterBoxTalkUseCase(ITalkWindowPresenter talkWindowPresenter, TalkMasterDataRepository talkMasterDataRepository)
        {
            _talkWindowPresenter = talkWindowPresenter;
            _talkMasterDataRepository = talkMasterDataRepository;
        }

        public bool IsShow { get; private set; }

        public async UniTask ShowAsync(CancellationToken cancellationToken = default)
        {
            await _talkWindowPresenter.OpenAsync(cancellationToken);
        }

        public async UniTask HideAsync(CancellationToken cancellationToken = default)
        {
            IsShow = false;
            await _talkWindowPresenter.CloseAsync(cancellationToken);
        }

        public async UniTask TalkAsync(int id, CancellationToken cancellationToken = default)
        {
            var talkData = _talkMasterDataRepository.Get(id);
            await TalkAsync(talkData, cancellationToken);
        }

        public async UniTask TalkAsync(TalkData talkData, CancellationToken cancellationToken = default)
        {
            IsShow = true;
            foreach (var messageEvent in talkData.MessageEvents)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(messageEvent.PreDelay), cancellationToken: cancellationToken);
                await _talkWindowPresenter.TalkAsync(messageEvent, cancellationToken);
                await UniTask.Delay(TimeSpan.FromSeconds(messageEvent.PostDelay), cancellationToken: cancellationToken);
            }
        }
    }
}