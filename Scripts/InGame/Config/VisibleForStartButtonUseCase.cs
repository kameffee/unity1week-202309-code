using UniRx;

namespace Unity1week202309.InGame.Config
{
    public class VisibleForStartButtonUseCase
    {
        public IReadOnlyReactiveProperty<bool> Visible => _visible;

        private readonly ReactiveProperty<bool> _visible = new(false);

        public void SetVisible(bool visible)
        {
            _visible.Value = visible;
        }
    }
}