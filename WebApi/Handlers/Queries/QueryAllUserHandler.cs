using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Handlers.Queries
{
    public class QueryAllUserHandler : IRequestHandler<QueryAllUserRequestModel, QueryAllUserResponseModel>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public QueryAllUserHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<QueryAllUserResponseModel> Handle(QueryAllUserRequestModel request, CancellationToken cancellationToken)
        {
            var users = await _userService.Get();
            return new QueryAllUserResponseModel
            {
                Users = _mapper.Map<IEnumerable<UserModel>>(users)
            };
        }
    }
}
