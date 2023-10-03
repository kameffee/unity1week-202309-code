using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Unity1week202309.Title
{
    public class TitleView : MonoBehaviour
    {
        [SerializeField]
        private Button _licenseButton;

        public IObservable<Unit> OnLicenseButtonClickAsObservable() => _licenseButton.OnClickAsObservable();
    }
}