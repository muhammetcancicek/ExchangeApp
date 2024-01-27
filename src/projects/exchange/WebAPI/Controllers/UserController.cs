using Microsoft.AspNetCore.Mvc;
using Application.Features.Users.Queries.GetUser;
using Application.Features.Users.Dtos;
using Application.Features.Users.Commands.CreateUser;
using Application.Features.Users.Commands.EditUser;
using Application.Features.Shares.Models;
using Application.Features.Shares.Queries.GetListShare;
using Core.Application.Requests;
using Application.Features.Users.Queries.GetListUser;
using Application.Features.Users.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListUserQuery getListUserQuery = new() { PageRequest = pageRequest };
            UserListModel result = await Mediator.Send(getListUserQuery);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute] GetByIdUserQuery getByIdIdUserQuery)
        {
            UserGetByIdDto userGetByIdDto = await Mediator.Send(getByIdIdUserQuery);
            return Ok(userGetByIdDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand createUserCommand)
        {
            CreateUserDTO result = await Mediator.Send(createUserCommand);
            return Created("", result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] EditUserCommand editUserCommand)
        {
            EditUserDTO result = await Mediator.Send(editUserCommand);
            return Created("", result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] DeleteUserQuery deleteUserQuery)
        {
            await Mediator.Send(deleteUserQuery);
            return Ok("Removed");
        }
    }
}
