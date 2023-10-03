using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Kameffee.Scenes
{
    public enum SceneDefine
    {
        Title = 0,
        InGame = 1,
    }

    public static class SceneLoaderExtensions
    {
        public static async UniTask LoadAsync(this SceneLoader sceneLoader, SceneDefine nextScene, CancellationToken cancellationToken = default)
        {
            if (!Enum.IsDefined(typeof(SceneDefine), nextScene))
            {
                throw new OperationCanceledException();
            }

            await sceneLoader.LoadAsync((int)nextScene, cancellationToken);
        }
    }
}