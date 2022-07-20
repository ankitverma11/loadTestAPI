using loadTestApi.Interface;
using loadTestApi.Model;

namespace loadTestApi.DataAccess
{
    public class DataAccessProvider : IDataAccessProvider
    {
        //private readonly PostgreDBContext _dBContext;
        PostgreDataAccesslayer _dataAccesslayer = new PostgreDataAccesslayer();

        public DataAccessProvider()
        {
        }

        public async Task InsertRecord(LoadData load)
        {
            // _dBContext.loadData.Add(load);
            // await _dBContext.SaveChangesAsync();
            await _dataAccesslayer.InsertRecord(load);
        }

        public async Task<IEnumerable<LoadData>> GetAllRecords()
        {
            return await _dataAccesslayer.GetAllRecords();
        }

        public async Task<IEnumerable<string>> GetRecordByKey(string key)
        {
            return await _dataAccesslayer.GetRecordByKey(key);
        }

        public async Task UpdateRecord(LoadData loadData)
        {
            await _dataAccesslayer.UpdateRecord(loadData);
        }

        public async Task DeleteRecord(string key)
        {
            await _dataAccesslayer.DeleteRecord(key);
        }
    }
}

