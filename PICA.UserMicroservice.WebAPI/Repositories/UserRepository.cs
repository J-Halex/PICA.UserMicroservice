using Dapper;
using MySqlConnector;
using PICA.UserMicroservice.WebAPI.Interfaces;
using PICA.UserMicroservice.WebAPI.Models;
using System.Data;

namespace PICA.UserMicroservice.WebAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default")
                ?? throw new ArgumentNullException("ConnectionString can't be null");
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            const string query = "select id, email, name, lastName, date from user;";
            using IDbConnection connection = GetConnection();
            return await connection.QueryAsync<User>(query);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            const string query = "select id, email, name, lastName, date from user where id = @id;";
            using IDbConnection connection = GetConnection();
            return await connection.QueryFirstOrDefaultAsync<User>(query, new { id });
        }

        public async Task CreateAsync(User user)
        {
            using IDbConnection connection = GetConnection();
            var sql = "INSERT INTO user (email, name, lastName, date) VALUES (@Email, @Name, @LastName, @Date)";
            await connection.ExecuteAsync(sql, user);
        }

        private IDbConnection GetConnection()
        {
            IDbConnection connection = new MySqlConnection(_connectionString);
            return connection;
        }
    }
}
