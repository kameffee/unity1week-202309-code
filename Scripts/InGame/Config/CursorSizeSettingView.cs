using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Unity1week202309.InGame.Config
{
    public class CursorSizeSettingView : MonoBehaviour
    {
        [SerializeField]
        private Slider _slider;

        public void ApplyViewModel(ViewModel viewModel)
        {
            if (viewModel == null)
            {
                gameObject.SetActive(false);
                return;
            }

            _slider.minValue = 0;
            _slider.maxValue = viewModel.Values.Length - 1;
            _slider.wholeNumbers = true;
            SetIndex(viewModel.Index);
        }

        public void SetIndex(int index) => _slider.value = index;

        public IObservable<int> OnChangeCursorSizeObservable()
        {
            return _slider.OnValueChangedAsObservable()
                .Select(value => (int)value);
        }

        public class ViewModel
        {
            public int[] Values { get; }
            public int Index { get; }

            public ViewModel(int[] values, int index)
            {
                if (values.Length <= index) throw new ArgumentOutOfRangeException(nameof(index));

                Values = values;
                Index = index;
            }
        }
    }
}