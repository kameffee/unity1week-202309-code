using System.Threading;
using Cysharp.Threading.Tasks;

namespace Unity1week202309.Talk
{
    public interface ITalkWindowPresenter
    {
        UniTask OpenAsync(CancellationToken cancellationToken = default);
        UniTask CloseAsync(CancellationToken cancellationToken = default);

        UniTask TalkAsync(MessageEvent messageEvent, CancellationToken cancellationToken = default);
    }
}