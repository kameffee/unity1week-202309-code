using System;
using UnityEngine;

namespace Unity1week202309.InGame.MouseCursor
{
    [Serializable]
    public class CursorData
    {
        [SerializeField]
        private int _id;

        [SerializeField]
        private Texture2D _texture2D;

        public int Id => _id;
        public Texture2D Texture2D => _texture2D;
    }
}