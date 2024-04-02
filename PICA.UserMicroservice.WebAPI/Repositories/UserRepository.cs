using Dapper;
using MySqlConnector;
using PICA.UserMicroservice.WebAPI.Interfaces;
using PICA.UserMicroservice.WebAPI.Models;
using System.Data;
using System.Text.Json;

namespace PICA.UserMicroservice.WebAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        private readonly ILogger _logger;

        public UserRepository(IConfiguration configuration, ILogger<UserRepository> logger)
        {
            _connectionString = configuration.GetConnectionString("Default")
                ?? throw new ArgumentNullException("ConnectionString can't be null");
            _logger = logger;
            _logger.LogInformation($"Cadena de conexion MySQL {_connectionString}");
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            _logger.LogInformation("UserRepository - Call {0} without params", nameof(GetAllAsync));

            const string query = "select id, email, name, lastName, date from user;";
            using IDbConnection connection = GetConnection();
            return await connection.QueryAsync<User>(query);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            _logger.LogInformation("UserRepository - Call {0} with param: {1}", nameof(GetByIdAsync), id);

            const string query = "select id, email, name, lastName, date from user where id = @id;";
            using IDbConnection connection = GetConnection();
            return await connection.QueryFirstOrDefaultAsync<User>(query, new { id });
        }

        public async Task CreateAsync(User user)
        {
            _logger.LogInformation("UserRepository - Call {0} with param: {1}", nameof(CreateAsync), JsonSerializer.Serialize(user));

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
