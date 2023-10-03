using System.Linq;
using Unity1week202309.Talk;
using UnityEditor;
using UnityEngine.Assertions;

namespace Unity1week202309.Editor
{
    public class TalkMasterDataAssetProcess
    {
        private const string FolderPath = "Assets/Application/MasterData/Talk/";
        private const string DataStorePath = "Assets/Application/MasterData/TalkMasterDataStore.asset";

        public void OnPostprocessAllAssets(
            string[] importedAssets,
            string[] deletedAssets,
            string[] movedAssets,
            string[] movedFromAssetPaths)
        {
            var dataStore = AssetDatabase.LoadAssetAtPath<TalkMasterDataStore>(DataStorePath);
            Assert.IsNotNull(dataStore);

            var targetImportedAssets = importedAssets
                .Where(path => path.StartsWith(FolderPath))
                .ToArray();
            foreach (var importedAsset in targetImportedAssets)
            {
                var masterData = AssetDatabase.LoadAssetAtPath<TalkMasterData>(importedAsset);
                Assert.IsNotNull(masterData);
                dataStore.Add(masterData);
            }


            if (targetImportedAssets.Any())
            {
                dataStore.Validate();
                EditorUtility.SetDirty(dataStore);
            }
        }
    }
}