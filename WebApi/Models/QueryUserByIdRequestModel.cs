using MediatR;

namespace WebApi.Models
{
    public class QueryUserByIdRequestModel : IRequest<QueryUserByIdResponseModel>
    {
        public int Id { get; set; }
    }
}
