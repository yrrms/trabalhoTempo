using trabalhoTempo.Models;
using SQLite;

namespace trabalhoTempo.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn;

        public SQLiteDatabaseHelper(string path) //passando o caminho do banco de dados por parametro
        {
            _conn = new SQLiteAsyncConnection(path);//conectando ao sqlite
            _conn.CreateTableAsync<Tempo>().Wait();//criando a tabela produto
        }

        public Task<int> Insert(Tempo p) //inserir previsao, passando o objeto p da classe Previsao
        {
            return _conn.InsertAsync(p);
        }

        public Task<List<Tempo>> GetAll() //lista todas as previsoes
        {
            return _conn.Table<Tempo>().ToListAsync();
        }

        public Task<List<Tempo>> Search(string q) //busca por data, corrigir
        {
            string sql = "SELECT * FROM Tempo WHERE data LIKE '%" + q + "%'";

            return _conn.QueryAsync<Tempo>(sql);
        }
    }
}
