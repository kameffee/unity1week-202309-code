using System.Collections.Generic;
using UnityEngine;

namespace Unity1week202309.Talk
{
    [CreateAssetMenu(fileName = "TalkMasterData_", menuName = "Unity1week202309/Talk/TalkMasterData", order = 0)]
    public class TalkMasterData : ScriptableObject
    {
        [SerializeField]
        private int _id;

        [SerializeField]
        private List<MessageEvent> _messageEvents;

        public int Id => _id;
        public IReadOnlyList<MessageEvent> MessageEvents => _messageEvents;
    }
}