using MediatR;

namespace WebApi.Models
{
    public class DeleteUserRequestModel : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
