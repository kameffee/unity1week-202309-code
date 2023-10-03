using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;

namespace Kameffee.License
{
    public class GetLicenseTextUseCase
    {
        private readonly string _resourceLoadPath;

        public GetLicenseTextUseCase(string resourceLoadPath)
        {
            _resourceLoadPath = resourceLoadPath;
        }

        public async UniTask<string> Get()
        {
            var textAsset = await Resources.LoadAsync<TextAsset>(_resourceLoadPath) as TextAsset;
            Assert.IsNotNull(textAsset);

            return textAsset.text;
        }
    }
}