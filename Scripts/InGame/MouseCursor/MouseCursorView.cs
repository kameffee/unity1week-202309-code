using UnityEngine;
using UnityEngine.UI;

namespace Unity1week202309.InGame.MouseCursor
{
    public class MouseCursorView : MonoBehaviour
    {
        [SerializeField]
        private Image _mouseCursorImage;

        public void SetPosition(Vector2 anchorPosition)
        {
            _mouseCursorImage.rectTransform.anchoredPosition = anchorPosition;
        }

        public void SetSize(int size)
        {
            _mouseCursorImage.rectTransform.sizeDelta = new Vector2(size, size);
        }
    }
}