using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Unity1week202309.InGame.Move
{
    public class RandomMoveSequencer : MonoBehaviour
    {
        private MoveRouteData[] _moveRouteDatas;
        private CancellationTokenSource _source;
        private int _currentIndex;

        private Tween _tween;

        public void Initialize(MoveRouteData[] moveRouteDatas)
        {
            _moveRouteDatas = moveRouteDatas;
        }

        public async UniTaskVoid AutoMoveStart()
        {
            _source = new CancellationTokenSource();
            while (!_source.IsCancellationRequested)
            {
                _currentIndex = (_currentIndex + 1) % _moveRouteDatas.Length;
                var moveRouteData = _moveRouteDatas[_currentIndex];
                await AutoMoveAsync(moveRouteData.Points[0].position, moveRouteData.Points[1].position,
                    1);
            }
        }

        public void AutoMoveStop()
        {
            _source.Cancel();
            _tween.Kill();
            _tween = null;
        }

        private async UniTask AutoMoveAsync(Vector3 startPosition, Vector3 endPosition, float duration)
        {
            transform.position = startPosition;
            _tween = transform.DOMove(endPosition, duration)
                .SetEase(Ease.Linear);

            await _tween.WithCancellation(cancellationToken: _source.Token);
        }
    }
}