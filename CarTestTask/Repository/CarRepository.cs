using CarTestTask.Models;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarTestTask.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly IConfiguration configuration;
        public CarRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<int> AddAsync(Car entity)
        {
            var sql = "INSERT INTO Car (Model, Number, Price) VALUES (@Model, @Number, @Price)";
            using (var connection = new SqliteConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM Car WHERE Id = @Id";
            using (var connection = new SqliteConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<Car>> GetAllAsync()
        {
            var sql = "SELECT * FROM Car";
            using (var connection = new SqliteConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Car>(sql);
                return result.ToList();
            }
        }

        public async Task<IReadOnlyList<Car>> GetByRecordNumbersAsync(int first, int last)
        {
            var sql = "SELECT * FROM Car LIMIT @First, @RowsToSelect"; // first arg - skip X rows, last - select Y rows
            using (var connection = new SqliteConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Car>(sql, new { First = first - 1, RowsToSelect = last - first + 1 });
                return result.ToList();
            }
        }

        public async Task<Car> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Car WHERE Id = @Id";
            using (var connection = new SqliteConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Car>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<int> UpdateAsync(Car entity)
        {
            var sql = "UPDATE Car SET Model = @Model, Number = @Number, Price = @Price WHERE Id = @Id";
            using (var connection = new SqliteConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }
    }
}
