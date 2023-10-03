using System;
using UnityEngine;

namespace Unity1week202309.Talk
{
    [Serializable]
    public class MessageEvent
    {
        [SerializeField]
        [TextArea(1, 2)]
        private string _message;

        [SerializeField]
        private float _preDelay = 0;

        [SerializeField]
        private float _postDelay = 2f;

        public string Message => _message;
        public float PreDelay => _preDelay;
        public float PostDelay => _postDelay;

        public MessageEvent(string message)
        {
            _message = message;
        }
    }
}