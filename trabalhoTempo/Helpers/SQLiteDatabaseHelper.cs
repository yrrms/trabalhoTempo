using trabalhoTempo.Models;
using SQLite;

namespace trabalhoTempo.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn;

        public Task<int> Insert(Tempo p) //inserir previsao, passando o objeto p da classe Previsao
        {
            return _conn.InsertAsync(p);
        }

        public Task<List<Tempo>> GetAll() //lista todas as previsoes
        {
            return _conn.Table<Tempo>().ToListAsync();
        }

        public Task<List<Tempo>> Search(string q) //busca da previsao
        {
            string sql = "SELECT * FROM Produto WHERE descricao LIKE '%" + q + "%'";

            return _conn.QueryAsync<Tempo>(sql);
        }
    }
}
