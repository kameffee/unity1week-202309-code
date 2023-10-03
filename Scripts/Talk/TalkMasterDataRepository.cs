namespace Unity1week202309.Talk
{
    public class TalkMasterDataRepository
    {
        private readonly TalkMasterDataStore _talkMasterDataStore;

        public TalkMasterDataRepository(TalkMasterDataStore talkMasterDataStore)
        {
            _talkMasterDataStore = talkMasterDataStore;
        }

        public TalkData Get(int id)
        {
            var masterData = _talkMasterDataStore.Find(id);
            return new TalkData(masterData.Id, masterData.MessageEvents);
        }
    }

    public class TalkService
    {
        private readonly TalkMasterDataRepository _talkMasterDataRepository;

        public TalkService(TalkMasterDataRepository talkMasterDataRepository)
        {
            _talkMasterDataRepository = talkMasterDataRepository;
        }

        public TalkData Get(int id) => _talkMasterDataRepository.Get(id);
    }
}