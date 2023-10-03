using UnityEngine;

namespace Unity1week202309.InGame.MouseCursor
{
    public class MouseCursorCanvasView : MonoBehaviour
    {
        [SerializeField]
        private MouseCursorView _mouseCursorView;

        public void SetPosition(Vector2 anchorPosition)
        {
            _mouseCursorView.SetPosition(anchorPosition);
        }

        public void SetSize(int size)
        {
            _mouseCursorView.SetSize(size);
        }
    }
}