using System.Threading;
using Cysharp.Threading.Tasks;
using Kameffee.Extensions;
using UniRx;
using VContainer.Unity;

namespace Kameffee.License
{
    public class LicensePresenter : Presenter, IInitializable
    {
        private readonly LicenseView _view;
        private readonly GetLicenseTextUseCase _getLicenseTextUseCase;

        public LicensePresenter(
            LicenseView view,
            GetLicenseTextUseCase getLicenseTextUseCase)
        {
            _view = view;
            _getLicenseTextUseCase = getLicenseTextUseCase;
        }

        public void Initialize()
        {
            _getLicenseTextUseCase.Get().ToObservable()
                .Subscribe(licenseText => _view.SetLicenseText(licenseText))
                .AddTo(this);

            _view.OnCloseAsObservable()
                .Subscribe(_ => _view.HideAsync().Forget())
                .AddTo(this);
        }

        public async UniTask ShowAsync(CancellationToken cancellationToken = default)
        {
            await _view.ShowAsync(cancellationToken);
        }

        public async UniTask HideAsync(CancellationToken cancellationToken = default)
        {
            await _view.HideAsync(cancellationToken);
        }
    }
}