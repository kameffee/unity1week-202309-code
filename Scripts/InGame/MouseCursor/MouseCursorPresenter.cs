using Kameffee.Extensions;
using UnityEngine;
using VContainer.Unity;
using UniRx;

namespace Unity1week202309.InGame.MouseCursor
{
    public class MouseCursorPresenter : Presenter, IInitializable, ITickable
    {
        private readonly MouseCursorService _mouseCursorService;
        private readonly MouseCursorCanvasView _mouseCursorCanvasView;
        private readonly RectTransform _parent;

        public MouseCursorPresenter(
            MouseCursorService mouseCursorService,
            MouseCursorCanvasView mouseCursorCanvasView)
        {
            _mouseCursorService = mouseCursorService;
            _mouseCursorCanvasView = mouseCursorCanvasView;
            _parent = mouseCursorCanvasView.transform as RectTransform;
        }

        public void Initialize()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
            _mouseCursorService.CurrentSize
                .Subscribe(size => _mouseCursorCanvasView.SetSize(size))
                .AddTo(this);
        }

        void ITickable.Tick()
        {
            // スクリーン座標からUI座標に変換
            var mousePosition = Input.mousePosition;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    _parent,
                    mousePosition,
                    null,
                    out var uiLocalPos
                ))
            {
                _mouseCursorCanvasView.SetPosition(uiLocalPos);
            }
        }
    }
}