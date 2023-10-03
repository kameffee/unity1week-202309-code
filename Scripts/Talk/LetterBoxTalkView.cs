using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Unity1week202309.Talk
{
    public class LetterBoxTalkView : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;

        [SerializeField]
        private RectTransform _top;

        [SerializeField]
        private RectTransform _bottom;

        [SerializeField]
        private TextMeshProUGUI _topText;

        private void Awake()
        {
            _topText.text = string.Empty;
            _top.DOAnchorPosY(0, 0);
            _bottom.DOAnchorPosY(0, 0);
        }

        public async UniTask OpenAsync(CancellationToken cancellationToken = default)
        {
            var sequence = DOTween.Sequence();
            sequence.Append(_top.DOAnchorPosY(-_top.sizeDelta.y, 1));
            sequence.Join(_bottom.DOAnchorPosY(_bottom.sizeDelta.y, 1));
            sequence.WithCancellation(cancellationToken);
            await sequence.Play();
        }

        public async UniTask CloseAsync(CancellationToken cancellationToken = default)
        {
            var sequence = DOTween.Sequence();
            sequence.Append(_top.DOAnchorPosY(0, 1));
            sequence.Join(_bottom.DOAnchorPosY(0, 1));
            sequence.WithCancellation(cancellationToken);
            await sequence.Play();
        }

        public void ClearText()
        {
            _topText.text = string.Empty;
        }

        public async UniTask ShowSubtitlingAsync(string text, CancellationToken cancellationToken = default)
        {
            _topText.text = string.Empty;

            var tween = _topText.DOText(text, text.Length * 0.1f)
                .SetEase(Ease.Linear);

            tween.WithCancellation(cancellationToken);
            tween.SetLink(gameObject);

            await tween.Play();
        }
    }
}