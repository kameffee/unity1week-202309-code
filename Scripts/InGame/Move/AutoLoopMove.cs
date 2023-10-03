using DG.Tweening;
using UnityEngine;

namespace Unity1week202309.InGame.Move
{
    public class AutoLoopMove : MonoBehaviour
    {
        public bool IsMoving => _tween != null && _tween.IsActive();

        private MoveRouteData _moveRouteData;
        private Tween _tween;
        private int _index;

        public void Initialize(MoveRouteData moveRouteData)
        {
            _moveRouteData = moveRouteData;
        }

        public void MoveStart()
        {
            _index = 0;
            MoveNext();
        }

        private void MoveNext()
        {
            _index = (_index + 1) % _moveRouteData.Points.Count;
            _tween = transform.DOMove(_moveRouteData.Points[_index].position, 0.2f)
                .SetEase(Ease.Linear)
                .OnComplete(() => MoveNext());
        }

        public void SetSpeed(float speed)
        {
            _tween.timeScale = speed;
        }

        public void MoveStop()
        {
            _tween.Kill();
        }
    }
}