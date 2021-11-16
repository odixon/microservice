using System.Collections.Generic;
using System.Threading.Tasks;

namespace Test.Repositories
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Models.Movie>> GetAllAsync();
    }
}
