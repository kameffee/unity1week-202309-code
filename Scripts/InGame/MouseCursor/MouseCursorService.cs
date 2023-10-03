using UniRx;

namespace Unity1week202309.InGame.MouseCursor
{
    public class MouseCursorService
    {
        public int DefaultSize => _defaultSize;
        public IReadOnlyReactiveProperty<int> CurrentSize => _currentSize;
        public MouseCursorType CurrentType { get; private set; } = MouseCursorType.Normal;
        public int MaxSize => _cursorSizeList[^1];

        private const int _defaultSize = 64;
        private readonly int[] _cursorSizeList = { 16, 32, 64, 92, 128, 256, 512, 1024, 2048 };
        private readonly ReactiveProperty<int> _currentSize = new(_defaultSize); 
        
        public MouseCursorService()
        {
        }

        public void ChangeSize(int size)
        {
            _currentSize.Value = size;
        }

        public void ChangeType(MouseCursorType type)
        {
            CurrentType = type;
        }

        public int IndexToSize(int index)
        {
            return _cursorSizeList[index];
        }

        public int[] GetCursorSizeList()
        {
            return _cursorSizeList;
        }
        
        public bool IsMaxSize() => _currentSize.Value == MaxSize;

        public void Reset()
        {
            _currentSize.Value = DefaultSize;
            CurrentType = MouseCursorType.Normal;
        }
    }
}