using System.Threading;
using Cysharp.Threading.Tasks;
using Kameffee.Extensions;

namespace Unity1week202309.Talk
{
    public class LetterBoxTalkWindowPresenter : Presenter, ITalkWindowPresenter
    {
        private readonly LetterBoxTalkView _letterBoxTalkView;

        public LetterBoxTalkWindowPresenter(LetterBoxTalkView letterBoxTalkView)
        {
            _letterBoxTalkView = letterBoxTalkView;
        }

        public async UniTask OpenAsync(CancellationToken cancellationToken = default)
        {
            await _letterBoxTalkView.OpenAsync(cancellationToken);
        }

        public async UniTask CloseAsync(CancellationToken cancellationToken = default)
        {
            await _letterBoxTalkView.CloseAsync(cancellationToken);
            _letterBoxTalkView.ClearText();
        }

        public async UniTask TalkAsync(MessageEvent messageEvent, CancellationToken cancellationToken = default)
        {
            await _letterBoxTalkView.ShowSubtitlingAsync(messageEvent.Message, cancellationToken);
        }
    }
}