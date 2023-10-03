using Kameffee.Extensions;
using Unity1week202309.InGame.Config;
using VContainer.Unity;
using UniRx;

namespace Unity1week202309.InGame.Intro
{
    public class StartButtonPresenter : Presenter, IInitializable
    {
        private readonly ButtonObjectView _buttonObjectView;
        private readonly VisibleForStartButtonUseCase _visibleForStartButtonUseCase;
        
        public StartButtonPresenter(
            ButtonObjectView buttonObjectView,
            VisibleForStartButtonUseCase visibleForStartButtonUseCase)
        {
            _buttonObjectView = buttonObjectView;
            _visibleForStartButtonUseCase = visibleForStartButtonUseCase;
        }
        
        public void Initialize()
        {
            _visibleForStartButtonUseCase.Visible
                .Subscribe(visible => _buttonObjectView.gameObject.SetActive(visible))
                .AddTo(this);
        }
    }
}