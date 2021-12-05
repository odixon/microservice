using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Filters.AddServerToHeader]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public Task<QueryAllUserResponseModel> Get()
        {
            return _mediator.Send(new QueryAllUserRequestModel());
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateUserRequestModel createUserRequestModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _mediator.Send(createUserRequestModel);
            return CreatedAtRoute("GetUserById", new { id = response.UserModel.Id }, response.UserModel);
        }

        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _mediator.Send(new QueryUserByIdRequestModel { Id = id });
            if (response == null)
            {
                return NotFound(); 
            }

            return Ok(response.UserInfo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteUserRequestModel { Id = id });
            return Ok(result);
        }
    }
}
