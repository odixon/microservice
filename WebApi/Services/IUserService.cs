using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> Get();

        Task<User> Create(User user);

        Task<User> FindUserById(int id);
    }
}
