using System.ComponentModel.DataAnnotations;
using MediatR;

namespace WebApi.Models
{
    public class CreateUserRequestModel : IRequest<CreateUserResponseModel>
    {
        [Required]
        public string Name { get; set; }

        [RegularExpression("(Male)|(Female)")]
        public string Gender { get; set; }

        [Range(1, 90)]
        public int Age { get; set; }
    }
}
