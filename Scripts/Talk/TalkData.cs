using System.Collections.Generic;

namespace Unity1week202309.Talk
{
    public class TalkData
    {
        public int Id { get; }
        public IReadOnlyList<MessageEvent> MessageEvents { get; }

        public TalkData(int id, IReadOnlyList<MessageEvent> messageEvents)
        {
            Id = id;
            MessageEvents = messageEvents;
        }
    }
}