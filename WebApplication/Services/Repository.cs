using System.Collections.Generic;
using System.Threading.Tasks;
using DapperExtensions;
using DapperExtensions.Predicate;
using Microsoft.Extensions.Configuration;
using WebApplication.Models;

namespace WebApplication.Services
{
    public class Repository : IRepository
    {
        private readonly string _connStr;

        public Repository(IConfiguration configuration)
        {
            _connStr = configuration.GetConnectionString("mysql");
        }

        public async Task<IEnumerable<Student>> GetStudentsAsync(string keyword)
        {
            using (var conn = new MySql.Data.MySqlClient.MySqlConnection(_connStr))
            {
                var predicates = new List<IPredicate>();
                predicates.Add(Predicates.Field<Student>(t => t.Name, Operator.Like, keyword ?? string.Empty));
                predicates.Add(Predicates.Field<Student>(t => t.Gender, Operator.Like, keyword ?? string.Empty));
                var predicatesGroup = Predicates.Group(GroupOperator.Or, predicates.ToArray());
                return await conn.GetListAsync<Student>(predicatesGroup);
            }
        }
    }
}
