using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Handlers.Queries
{
    public class QueryUserByIdHandler : IRequestHandler<QueryUserByIdRequestModel, QueryUserByIdResponseModel>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public QueryUserByIdHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<QueryUserByIdResponseModel> Handle(QueryUserByIdRequestModel request, CancellationToken cancellationToken)
        {
            var entity = await _userService.FindUserById(request.Id);
            if (entity == null)
            {
                return null;
            }

            return new QueryUserByIdResponseModel
            {
                UserInfo = _mapper.Map<UserModel>(entity)
            };
        }
    }
}
