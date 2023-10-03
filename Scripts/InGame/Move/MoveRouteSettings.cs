using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity1week202309.InGame.Move
{
    [Serializable]
    public class MoveRouteSettings
    {
        [SerializeField]
        private List<MoveRouteData> _moveRouteDataList;
        
        public IReadOnlyList<MoveRouteData> MoveRouteDataList => _moveRouteDataList;
    }

    [Serializable]
    public class MoveRouteData
    {
        [SerializeField]
        private List<Transform> _points;

        public IReadOnlyList<Transform> Points => _points;
    }
}