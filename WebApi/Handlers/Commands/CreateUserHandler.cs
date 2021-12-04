using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Handlers.Commands
{
    public class CreateUserHandler : IRequestHandler<CreateUserRequestModel, CreateUserResponseModel>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public CreateUserHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<CreateUserResponseModel> Handle(CreateUserRequestModel request, CancellationToken cancellationToken)
        {
            var entity = await _userService.Create(_mapper.Map<Entities.User>(request));
            return new CreateUserResponseModel
            {
                UserModel = _mapper.Map<Models.UserModel>(entity)
            };
        }
    }
}
