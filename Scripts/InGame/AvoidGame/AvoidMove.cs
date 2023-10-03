using System;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using Range = UnityEngine.Random;

namespace Unity1week202309.InGame.AvoidGame
{
    public class AvoidMove : MonoBehaviour
    {
        [SerializeField]
        private GameObject _moveTarget;

        [SerializeField]
        private Transform[] _avoidToTargets;

        [SerializeField]
        private float _movableTargetsRadius = 5f;

        [SerializeField]
        private float _avoidDistance = 5f;

        public bool IsTargetEnter { get; private set; }

        private AvoidTarget _enteredTarget;
        private Tween _tween;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<AvoidTarget>(out var target))
            {
                IsTargetEnter = true;
                _enteredTarget = target;

                if (_tween != null && _tween.IsPlaying())
                {
                    _tween.Kill();
                    _tween = null;
                }

                if (_enteredTarget.MoveSpeed > 0f)
                {
                    Move();
                }
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.TryGetComponent<AvoidTarget>(out var target))
            {
                IsTargetEnter = true;
                _enteredTarget = target;

                if (_tween != null && _tween.IsPlaying())
                {
                    return;
                }

                if (_enteredTarget.MoveSpeed > 0f)
                {
                    Move();
                }
            }
            else
            {
                IsTargetEnter = false;
                _enteredTarget = null;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent<AvoidTarget>(out _))
            {
                IsTargetEnter = false;
                _enteredTarget = null;
            }
        }

        public void Move()
        {
            if (TryGetNextPosition(out var nextPosition))
            {
                _tween = _moveTarget.transform.DOMove(nextPosition, 0.2f)
                    .SetLink(gameObject);
            }
        }

        private bool TryGetNextPosition(out Vector3 nextPosition)
        {
            // 遠いポイントを選択する
            var array = _avoidToTargets
                // 移動可能範囲内のみ
                .Where(x => Vector3.Distance(x.position, transform.position) <= _movableTargetsRadius)
                // 避ける対象の範囲に入るものは除く
                .Where(x => !_enteredTarget || Vector3.Distance(x.position, _enteredTarget.transform.position ) > _avoidDistance)
                .ToArray();

            if (array.Any())
            {
                nextPosition = array[Range.Range(0, array.Length)].position;
                return true;
            }

            nextPosition = Vector3.zero;
            return false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _movableTargetsRadius);
        }
    }
}