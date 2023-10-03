using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Unity1week202309.InGame.UI
{
    public class UIButtonAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler,
        IPointerExitHandler
    {
        [SerializeField]
        private Button _button;

        [SerializeField]
        private Image _normalImage;

        [SerializeField]
        private Image _hoverImage;

        [SerializeField]
        private Image _pressedImage;

        [SerializeField]
        private Image _disabledImage;

        private bool _isEnter;

        private void Awake()
        {
            _normalImage.gameObject.SetActive(true);
            _hoverImage.gameObject.SetActive(false);
            _pressedImage.gameObject.SetActive(false);
            _disabledImage.gameObject.SetActive(false);

            var onChangeInteractableObservable = _button.ObserveEveryValueChanged(button => button.interactable);

            onChangeInteractableObservable
                .Where(value => !value)
                .Subscribe(_ =>
                {
                    _disabledImage.gameObject.SetActive(true);
                    _normalImage.gameObject.SetActive(false);
                    _hoverImage.gameObject.SetActive(false);
                    _pressedImage.gameObject.SetActive(false);
                })
                .AddTo(this);

            onChangeInteractableObservable
                .Where(value => value)
                .Subscribe(_ =>
                {
                    _normalImage.gameObject.SetActive(!_isEnter);
                    _hoverImage.gameObject.SetActive(_isEnter);
                    _pressedImage.gameObject.SetActive(false);
                    _disabledImage.gameObject.SetActive(false);
                })
                .AddTo(this);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_button.interactable) return;

            _normalImage.gameObject.SetActive(false);
            _hoverImage.gameObject.SetActive(false);
            _pressedImage.gameObject.SetActive(true);
            _disabledImage.gameObject.SetActive(false);
        }


        public void OnPointerUp(PointerEventData eventData)
        {
            if (!_button.interactable) return;

            _normalImage.gameObject.SetActive(_isEnter);
            _hoverImage.gameObject.SetActive(!_isEnter);
            _pressedImage.gameObject.SetActive(false);
            _disabledImage.gameObject.SetActive(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _isEnter = true;
            if (!_button.interactable) return;

            _normalImage.gameObject.SetActive(false);
            _hoverImage.gameObject.SetActive(true);
            _pressedImage.gameObject.SetActive(false);
            _disabledImage.gameObject.SetActive(false);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isEnter = false;
            _normalImage.gameObject.SetActive(_button.interactable);
            _hoverImage.gameObject.SetActive(false);
            _pressedImage.gameObject.SetActive(false);
            _disabledImage.gameObject.SetActive(!_button.interactable);
        }

        private void OnValidate()
        {
            if (_button == null)
            {
                _button = GetComponent<Button>();
            }
        }
    }
}