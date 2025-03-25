using finalproject.Application.Users.Commands;
using finalproject.Application.Users.Queries;
using finalproject.Domain.DTOs;
using finalproject.Domain.Entities;
using finalproject.Domain.Interface;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace finalproject.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetUser(string? searchTerm, int pageNumber = 1, int pageSize = 10) {
            var res = await _mediator.Send(new GetUserQuery(searchTerm, pageNumber, pageSize));

            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] AddUserCommand command) {
            int userId = await _mediator.Send(command);
            return Ok(userId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id) {
            bool res = await _mediator.Send(new DeleteUserCommand(id));

            return Ok(res);
        }

        [HttpPut("{id}/toggle-status")]
        public async Task<IActionResult> UpdateStatus(int id) {
           var res = await _mediator.Send(new UpdateUserStatusCommand(id));
            return Ok(res);
        }
            
    }
}
