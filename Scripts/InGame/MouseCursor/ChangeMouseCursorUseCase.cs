namespace Unity1week202309.InGame.MouseCursor
{
    public class ChangeMouseCursorUseCase
    {
        private readonly MouseCursorSettings _mouseCursorSettings;
        private readonly MouseCursorService _mouseCursorService;

        public ChangeMouseCursorUseCase(
            MouseCursorSettings mouseCursorSettings,
            MouseCursorService mouseCursorService)
        {
            _mouseCursorSettings = mouseCursorSettings;
            _mouseCursorService = mouseCursorService;
        }

        public void ChangeTo(MouseCursorType type, int? toSize = null)
        {
            var toChangeSize = toSize ?? _mouseCursorService.CurrentSize.Value;
            _mouseCursorService.ChangeSize(toChangeSize);
            _mouseCursorService.ChangeType(type);
        }

        public void ChangeSize(int size)
        {
            ChangeTo(_mouseCursorService.CurrentType, size);
        }
    }
}