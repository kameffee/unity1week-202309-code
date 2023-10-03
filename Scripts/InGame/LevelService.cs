namespace Unity1week202309.InGame
{
    public class LevelService
    {
        public int CurrentLevel { get; private set; }

        public int Next()
        {
            return CurrentLevel++;
        }
    }
}