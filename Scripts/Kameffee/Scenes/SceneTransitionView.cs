using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Kameffee.Scenes
{
    public class SceneTransitionView : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        public async UniTask Show()
        {
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;

            await _canvasGroup.DOFade(1f, 1f)
                .SetLink(gameObject)
                .Play();
        }

        public async UniTask Hide()
        {
            await _canvasGroup.DOFade(0, 1f)
                .SetLink(gameObject)
                .Play();

            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}