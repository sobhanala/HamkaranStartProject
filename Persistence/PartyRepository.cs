//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Threading.Tasks;
//using Domain.Partys;

//namespace Persistence
//{
//    public class PartyRepository : BaseRepository
//    {
//        private const string SelectAll = "SELECT Id, PartyName FROM Parties";
//        private const string SelectById = "SELECT Id, PartyName FROM Parties WHERE Id = @Id";

//        private const string InsertParty = @"
//            INSERT INTO Parties (PartyName) 
//            VALUES (@PartyName);
//            SELECT CAST(SCOPE_IDENTITY() AS INT)";

//        private const string UpdateParty = "UPDATE Parties SET PartyName = @PartyName WHERE Id = @Id";
//        private const string DeleteParty = "DELETE FROM Parties WHERE Id = @Id";

//        public PartyRepository(DbConnectionFactory connectionFactory)
//            : base(connectionFactory)
//        {
//        }

//        public async Task<Party> GetByIdAsync(int id)
//        {
//            var parameter = CreateParameter("@Id", id, DbType.Int32);
//            var parties = await ExecuteReaderQueryAsync(SelectById, CommandType.Text, MapPartyFromReader, parameter);
//            return parties.FirstOrDefault();
//        }

//        public async Task<IEnumerable<Party>> GetAllAsync()
//        {
//            return await ExecuteReaderQueryAsync(SelectAll, CommandType.Text, MapPartyFromReader);
//        }

//        public async Task<int> InsertAsync(Party party)
//        {
//            var parameters = new[]
//            {
//                CreateParameter("@PartyName", party.Name, DbType.String)
//            };

//            return await ExecuteScalarAsync<int>(InsertParty, CommandType.Text, parameters);
//        }

//        public async Task<bool> UpdateAsync(Party party)
//        {
//            var parameters = new[]
//            {
//                CreateParameter("@Id", party.Id, DbType.Int32),
//                CreateParameter("@PartyName", party.Name, DbType.String)
//            };

//            var rowsAffected = await ExecuteWriterCommandAsync(UpdateParty, CommandType.Text, parameters);
//            return rowsAffected > 0;
//        }

//        public async Task<bool> DeleteAsync(int id)
//        {
//            var parameter = CreateParameter("@Id", id, DbType.Int32);
//            var rowsAffected = await ExecuteWriterCommandAsync(DeleteParty, CommandType.Text, parameter);
//            return rowsAffected > 0;
//        }

//        private Party MapPartyFromReader(IDataReader reader)
//        {
//            return new Party
//            {
//                Id = reader.GetInt32(reader.GetOrdinal("Id")),
//                Name = reader.GetString(reader.GetOrdinal("PartyName"))
//            };
//        }
//    }
//}

