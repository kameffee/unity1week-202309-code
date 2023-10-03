using UnityEngine;
using UnityEngine.UI;

namespace Unity1week202309.InGame
{
    public class LoadingProgressView : MonoBehaviour
    {
        [SerializeField]
        private Slider _progressBar;

        public float Progress
        {
            set => _progressBar.value = value;
        }

        public void SetProgress(float progress)
        {
            _progressBar.value = progress;
        }
    }
}