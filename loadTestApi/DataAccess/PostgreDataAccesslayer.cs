using System.Data;
using loadTestApi.Model;
using Npgsql;

namespace loadTestApi.DataAccess
{
    public class PostgreDataAccesslayer
    {
       
        string connectionstring = "Server=localhost;Port=5432;Database=NexvalDB;User Id=postgres;Password=postgre@11;";

        public async Task<IEnumerable<LoadData>> GetAllRecords()
        {
            List<LoadData> loadDataList = new List<LoadData>();

            using (NpgsqlConnection connection = new NpgsqlConnection())
            {
                connection.ConnectionString = connectionstring;
                connection.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("fetch_alldata", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                NpgsqlDataReader dr =await cmd.ExecuteReaderAsync();


                while (await dr.ReadAsync())
                {
                    var loadData = new LoadData
                    {
                        Key = dr.GetString("key"),
                        Value = dr.GetString("value"),

                    };
                    loadDataList.Add(loadData);
                }

                cmd.Dispose();
                connection.Close();
            }
            return loadDataList;

        }

        public async Task<IEnumerable<string>> GetRecordByKey(string key)
        {
            List<string> lstValue = new List<string>();

            using (NpgsqlConnection connection = new NpgsqlConnection())
            {
                connection.ConnectionString = connectionstring;
                connection.Open();
                // NpgsqlCommand cmd = new NpgsqlCommand("fetch_data()", connection);
                NpgsqlCommand cmd = new NpgsqlCommand("Select value from loaddata where key = @key", connection);
                cmd.CommandType = CommandType.Text;
                var param = cmd.CreateParameter();
                cmd.Parameters.AddWithValue("@key", key);
                NpgsqlDataReader dr = await cmd.ExecuteReaderAsync();
                while (await dr.ReadAsync())
                {
                    lstValue.Add(dr.GetString("value"));

                }

                cmd.Dispose();
                connection.Close();
            }
            return lstValue;

        }

        public async Task InsertRecord(LoadData data)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection())
            {
                connection.ConnectionString = connectionstring;
                connection.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("insert into loaddata (Key,Value) " +
                     "values(@key, @value);",connection);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@key", data.Key);
                cmd.Parameters.AddWithValue("@value", data.Value);
                await cmd.ExecuteNonQueryAsync();
                cmd.Dispose();
                connection.Close();

            }
        }

        public async Task UpdateRecord(LoadData data)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection())
            {
                connection.ConnectionString = connectionstring;
                connection.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("Update loaddata set Value= @value where key = @key;", connection);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@key", data.Key);
                cmd.Parameters.AddWithValue("@value", data.Value);
                await cmd.ExecuteNonQueryAsync();
                cmd.Dispose();
                connection.Close();
            }
        }

        public async Task DeleteRecord(string key)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection())
            {
                connection.ConnectionString = connectionstring;
                connection.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("Delete from loaddata where key = @key;", connection);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@key", key);
                await cmd.ExecuteNonQueryAsync();
                cmd.Dispose();
                connection.Close();
            }
        }
    }
}