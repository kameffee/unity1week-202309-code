using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine.SceneManagement;

namespace Kameffee.Scenes
{
    public class SceneLoader : IDisposable
    {
        private readonly SceneTransitionView _sceneTransitionView;
        private readonly CompositeDisposable _disposable = new();

        private readonly CancellationTokenSource _cancellationTokenSource = new();

        public SceneLoader(SceneTransitionView sceneTransitionView)
        {
            _sceneTransitionView = sceneTransitionView;
        }

        public async UniTask LoadAsync(int nextScene, CancellationToken cancellationToken = default)
        {
            await _sceneTransitionView.Show();

            var currentScene = SceneManager.GetActiveScene();

            await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: cancellationToken);

            // 遷移先のシーンを読み込み
            await SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive)
                .ToUniTask(cancellationToken: cancellationToken);

            // アクティブ化
            var loadedScene = SceneManager.GetSceneByBuildIndex(nextScene);
            SceneManager.SetActiveScene(loadedScene);

            // 前のシーンをアンロード
            await SceneManager.UnloadSceneAsync(currentScene)
                .ToUniTask(cancellationToken: cancellationToken);

            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: cancellationToken);

            await _sceneTransitionView.Hide();
        }

        public void Dispose()
        {
            _disposable.Dispose();
            _cancellationTokenSource.Dispose();
        }
    }
}