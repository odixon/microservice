using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Handlers.Commands
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserRequestModel, bool>
    {
        private readonly IUserService _userService;
        public DeleteUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> Handle(DeleteUserRequestModel request, CancellationToken cancellationToken)
        {
            return await _userService.Delete(request.Id);
        }
    }
}
