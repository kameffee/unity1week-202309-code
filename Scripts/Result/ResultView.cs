using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Unity1week202309.Result
{
    public class ResultView : MonoBehaviour
    {
        [SerializeField]
        private Button _titleButton;

        [SerializeField]
        private TextMeshProUGUI _thanksText;

        private string _thanksMessage;

        private void Awake()
        {
            _thanksMessage = _thanksText.text;
            _thanksText.text = string.Empty;
            _titleButton.gameObject.SetActive(false);
        }

        public async UniTask ShowAsync(CancellationToken cancellation = default)
        {
            var sequence = DOTween.Sequence();

            sequence.Append(_thanksText
                .DOText(_thanksMessage, _thanksMessage.Length * 0.3f)
                .SetEase(Ease.Linear));

            var toColor = _thanksText.color * new Color(1f, 1f, 1f, 0.2f);
            sequence.Append(_thanksText.DOColor(toColor, 0.3f)
                .SetEase(Ease.Flash, 6));

            sequence.WithCancellation(cancellation);
            await sequence.Play();

            await UniTask.Delay(TimeSpan.FromSeconds(2f), cancellationToken: cancellation);

            _titleButton.gameObject.SetActive(true);
        }

        public IObservable<Unit> OnClickTitleAsObservable() => _titleButton.OnClickAsObservable();
    }
}