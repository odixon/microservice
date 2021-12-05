using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using WebApi.Entities;

namespace WebApi.Services
{
    public class UserService : IUserService
    {
        private readonly Settings _settings;
        private readonly ILogger<UserService> _logger;

        public UserService(Settings settings, ILogger<UserService> logger)
        {
            _settings = settings;
            _logger = logger;
        }

        public async Task<User> Create(User user)
        {
            var sql = @"INSERT INTO Users(Id, Name, Gender, Birthday)
                        SELECT IFNULL(MAX(Id), 0) + 1, @Name, @Gender, @Birthday FROM Users;";
            var result = await Execute(async conn =>
            {
                await conn.ExecuteAsync(sql, user);
                return await conn.QuerySingleOrDefaultAsync<User>("SELECT Id, Name, Gender, Birthday FROM Users ORDER BY Id DESC LIMIT 1");
            });
            return result;
        }

        public Task<User> FindUserById(int id)
        {
            return Execute(conn => conn.QuerySingleOrDefaultAsync<User>("SELECT Id, Name, Gender, Birthday FROM Users WHERE Id = @Id", new { Id = id }));
        }

        public Task<IEnumerable<User>> Get()
        {
            _logger.LogInformation("Query all users from database...");
            return Execute(conn => conn.QueryAsync<User>("SELECT Id, Name, Gender, Birthday FROM Users ORDER BY Id"));
        }

        public async Task<bool> Delete(int id)
        {
            var result = await Execute(conn => conn.ExecuteAsync("DELETE FROM Users WHERE Id = @Id", new { Id = id }));
            return result > 0;
        }

        private T Execute<T>(Func<DbConnection, T> func)
        {
            using(var conn = new MySqlConnection(_settings.DbConnectionString))
            {
                return func(conn);
            }
        }
    }
}
