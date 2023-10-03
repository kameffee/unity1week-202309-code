using System;
using Kameffee.Audio;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;
using VContainer.Unity;

namespace Unity1week202309.InGame
{
    public class ButtonObjectView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler,
        IPointerDownHandler, IPointerClickHandler
    {
        [SerializeField]
        private GameObject _normal;

        [SerializeField]
        private GameObject _hover;

        [SerializeField]
        private GameObject _pressed;

        [Header("Sound")]
        [SerializeField]
        private string _onClickSeKey;

        public bool Interactable { get; private set; } = true;
        public bool IsClickable { private set; get; } = true;

        private readonly Subject<Unit> _onClicked = new();
        private bool _isEnter;

        [Inject]
        private readonly AudioPlayer _audioPlayer;

        private void Awake()
        {
            _normal.SetActive(true);
            _hover.SetActive(false);
            _pressed.SetActive(false);
        }

        public IObservable<Unit> OnClickAsObservable() => _onClicked;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!Interactable) return;

            _isEnter = true;
            _hover.SetActive(true);
            _normal.SetActive(false);
            _pressed.SetActive(false);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!Interactable) return;

            _isEnter = false;
            _normal.SetActive(true);
            _hover.SetActive(false);
            _pressed.SetActive(false);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!Interactable) return;
            if (!IsClickable) return;

            var isHover = _isEnter;
            _normal.SetActive(!isHover);
            _hover.SetActive(isHover);
            _pressed.SetActive(false);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!Interactable) return;
            if (!IsClickable) return;

            _pressed.SetActive(true);
            _hover.SetActive(false);
            _normal.SetActive(false);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!Interactable) return;
            if (!IsClickable) return;

            if (!string.IsNullOrEmpty(_onClickSeKey))
            {
                _audioPlayer?.PlaySe(_onClickSeKey);
            }

            _onClicked.OnNext(Unit.Default);
        }

        public void SetInteractable(bool interactable)
        {
            Interactable = interactable;

            _normal.SetActive(interactable);
            _hover.SetActive(false);
            _pressed.SetActive(!interactable);
        }

        public void SetClickable(bool isClickable)
        {
            IsClickable = isClickable;
        }

        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);
    }
}