using CarTestTask.Models;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarTestTask.Repository
{
    public class UserRepository
    {
        private readonly IConfiguration _configuration;
        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IReadOnlyList<User>> GetAllAsync()
        {
            var sql = "SELECT * FROM User";
            using (var connection = new SqliteConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<User>(sql);
                return result.ToList();
            }
        }

        public string GetPassword(string userName)
        {
            var sql = "SELECT * FROM User WHERE UserName = @UserName";
            using (var connection = new SqliteConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = connection.QuerySingleOrDefault<User>(sql, new { UserName = userName });
                return result?.Password;
            }
        }
    }
}
