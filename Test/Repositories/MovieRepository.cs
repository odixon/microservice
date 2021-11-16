using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Models;

namespace Test.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        public Task<IEnumerable<Movie>> GetAllAsync()
        {
            var movies = Enumerable.Range(1, 10)
                .Select(t => new Movie
                {
                    Id = t,
                    Name = $"Movie {t}",
                    Type = $"Type{t % 3}"
                });
            return Task.FromResult(movies);
        }
    }
}
