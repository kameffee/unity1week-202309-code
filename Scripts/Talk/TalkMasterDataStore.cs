using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Unity1week202309.Talk
{
    [CreateAssetMenu(menuName = "Unity1week202309/Talk/TalkMasterDataStore", fileName = "TalkMasterDataStore",
        order = 0)]
    public class TalkMasterDataStore : ScriptableObject
    {
        [SerializeField]
        private List<TalkMasterData> _talkMasterDataList = new();

        public IReadOnlyList<TalkMasterData> All() => _talkMasterDataList;
        public TalkMasterData Find(int id)
        {
            try
            {
                return _talkMasterDataList.Find(x => x.Id == id);
            }
            catch (Exception e)
            {
                Debug.LogError($"id:{id}が見つかりませんでした");
                throw;
            }
        }

        public void Add(TalkMasterData talkMasterData)
        {
            if (_talkMasterDataList.Contains(talkMasterData)) return;
            _talkMasterDataList.Add(talkMasterData);
        }

        public void Remove(TalkMasterData talkMasterData) => _talkMasterDataList.Remove(talkMasterData);

        public void Validate()
        {
            _talkMasterDataList.RemoveAll(data => data == null);
            _talkMasterDataList.Sort((a, b) => a.Id - b.Id);
        }
    }
}