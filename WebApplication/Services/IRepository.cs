using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Services
{
    public interface IRepository
    {
        Task<IEnumerable<Student>> GetStudentsAsync(string keyword);
    }
}
