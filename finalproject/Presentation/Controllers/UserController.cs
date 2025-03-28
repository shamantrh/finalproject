using finalproject.Application.Users.Commands;
using finalproject.Application.Users.Queries;
using finalproject.Domain.DTOs;
using finalproject.Domain.Entities;
using finalproject.Domain.Enums;
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
        public async Task<IActionResult> GetUser(string? searchTerm, Role? role, bool? status, int pageNumber = 1, int pageSize = 10) {
            var res = await _mediator.Send(new GetUserQuery(searchTerm, role, status, pageNumber, pageSize));

            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] AddUserCommand command) {
            if (!ModelState.IsValid)
            {
                // This will return a 400 Bad Request with the validation errors.
                return BadRequest(ModelState);
            }

            int userId = await _mediator.Send(command);
            return Ok(userId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id) {
            bool res = await _mediator.Send(new DeleteUserCommand(id));

            return Ok(res);
        }

       
        [HttpPut("{id}/activate")]
        public async Task<IActionResult> ActivateUser(int id)
        {
            var res = await _mediator.Send(new ActivateUserCommand(id));
            if (res == false) {
                return Ok(false);
            }
            return Ok(res);
        }

        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> DeactivateUser(int id)
        {
            var res = await _mediator.Send(new DeactivateUserCommand(id));
            if (res == false) {
                return Ok(false);
            }
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id) {
            var res = await _mediator.Send(new GetUserByIdQuery(id));
            return Ok(res);
        }

    }
}
