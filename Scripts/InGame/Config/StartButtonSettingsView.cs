using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Unity1week202309.InGame.Config
{
    public class StartButtonSettingsView : MonoBehaviour
    {
        [SerializeField]
        private Toggle _visibleStartButtonToggle;

        public void ApplyViewModel(ViewModel viewModel)
        {
            if (viewModel == null)
            {
                gameObject.SetActive(false);
                return;
            }

            _visibleStartButtonToggle.isOn = viewModel.Visible;
        }

        public IObservable<bool> OnChangeToggleVisibleStartButtonAsObservable()
        {
            return _visibleStartButtonToggle.OnValueChangedAsObservable();
        }

        public class ViewModel
        {
            public bool Visible { get; }

            public ViewModel(bool visible)
            {
                Visible = visible;
            }
        }
    }
}