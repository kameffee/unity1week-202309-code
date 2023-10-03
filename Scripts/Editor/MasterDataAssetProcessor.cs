namespace Unity1week202309.Editor
{
    public class MasterDataAssetProcessor : UnityEditor.AssetPostprocessor
    {
        private static TalkMasterDataAssetProcess _talkMasterDataAssetProcess;
        
        public static void OnPostprocessAllAssets(
            string[] importedAssets,
            string[] deletedAssets,
            string[] movedAssets,
            string[] movedFromAssetPaths)
        {
            _talkMasterDataAssetProcess = new TalkMasterDataAssetProcess();
            _talkMasterDataAssetProcess.OnPostprocessAllAssets(importedAssets, deletedAssets, movedAssets, movedFromAssetPaths);
        }
    }
}