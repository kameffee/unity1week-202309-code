using System;
using Unity1week202309.InGame.MouseCursor;

namespace Unity1week202309.InGame.Config
{
    using ViewModel = CursorSizeSettingView.ViewModel;

    public class CreateCursorSizeSettingsViewModelUseCase
    {
        private readonly MouseCursorService _mouseCursorService;

        public CreateCursorSizeSettingsViewModelUseCase(MouseCursorService mouseCursorService)
        {
            _mouseCursorService = mouseCursorService;
        }

        public ViewModel Create()
        {
            var cursorSizeList = _mouseCursorService.GetCursorSizeList();
            var currentSize = _mouseCursorService.CurrentSize.Value;
            var index = Array.IndexOf(cursorSizeList, currentSize);
            return new ViewModel(cursorSizeList, index);
        }
    }
}