using loadTestApi.Model;

namespace loadTestApi.Interface
{
    public interface IDataAccessProvider
    {
         Task InsertRecord(LoadData load);
         Task UpdateRecord(LoadData load);
         Task DeleteRecord(string key); 
         Task<IEnumerable<String>> GetRecordByKey(string key);
         Task<IEnumerable<LoadData>> GetAllRecords();    
    }
}

