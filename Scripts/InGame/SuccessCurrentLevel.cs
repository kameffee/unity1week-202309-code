using Cysharp.Threading.Tasks;
using Kameffee.Scenes;
using UnityEngine.SceneManagement;

namespace Unity1week202309.InGame
{
    public class SuccessCurrentLevel
    {
        private readonly LevelService _levelService;
        private readonly SceneLoader _sceneLoader;

        public SuccessCurrentLevel(LevelService levelService, SceneLoader sceneLoader)
        {
            _levelService = levelService;
            _sceneLoader = sceneLoader;
        }

        public void Success()
        {
            _levelService.Next();
        }
        
        public async UniTask LoadNextScene()
        {
            // TODO: マスタから取得
            var nextSceneBuildIndex = SceneManager.GetActiveScene().buildIndex + 1;
            await _sceneLoader.LoadAsync(nextSceneBuildIndex);
        }
    }
}