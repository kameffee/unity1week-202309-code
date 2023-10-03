using System.Collections.Generic;
using System.Linq;
using Kameffee.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer.Unity;

namespace Unity1week202309.InGame
{
    public class InputPresenter : Presenter, ITickable
    {
        private readonly Camera _mainCamera;
        private readonly List<RaycastResult> _raycastResults = new();

        public InputPresenter(Camera mainCamera)
        {
            _mainCamera = mainCamera;
        }

        public void Tick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (IsPointerOverUIObject(Input.mousePosition))
                {
                    return;
                }

                var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                var raycast = Physics2D.Raycast(
                    ray.origin,
                    ray.direction,
                    float.MaxValue);

                if (raycast.collider != null)
                {
                    if (raycast.collider.gameObject.TryGetComponent<IAttackReceivable>(out var attackReceivable))
                    {
                        attackReceivable.OnReceiveAttack();
                    }
                }
            }
        }

        /// <summary>
        /// UIに被っているかどうかを判定
        /// </summary>
        private bool IsPointerOverUIObject(Vector2 screenPosition)
        {
            var eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = screenPosition;

            EventSystem.current.RaycastAll(eventDataCurrentPosition, _raycastResults);
            var over = _raycastResults.Any(result => result.gameObject.transform is RectTransform);
            _raycastResults.Clear();
            return over;
        }
    }
}