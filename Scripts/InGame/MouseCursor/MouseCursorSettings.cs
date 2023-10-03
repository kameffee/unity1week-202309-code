using System.Collections.Generic;
using UnityEngine;

namespace Unity1week202309.InGame.MouseCursor
{
    [CreateAssetMenu(fileName = "MouseCursorSettings", menuName = "Unity1week/MouseCursorSettings", order = 0)]
    public class MouseCursorSettings : ScriptableObject
    {
        [SerializeField]
        private List<CursorData> _dataList;

        public CursorData Get(int id)
        {
            return _dataList.Find(data => data.Id == id);
        }
    }
}