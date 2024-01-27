using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Application.Services.Repositories;
using Microsoft.AspNetCore.Components.Forms;
using Core.Application.Requests;
using MediatR;
using Application.Features.Shares.Queries.GetListShare;
using Application.Features.Shares.Queries.GetShare;
using Application.Features.Shares.Dtos;
using Application.Features.Shares.Models;
using Application.Features.Shares.Commands.CreateShare;
using Application.Features.Shares.Commands.EditShare;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShareController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListShareQuery getListShareQuery = new() { PageRequest = pageRequest };
            ShareListModel result = await Mediator.Send(getListShareQuery);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute] GetByIdShareQuery getByIdIdShareQuery)
        {
            ShareGetByIdDto shareGetByIdDto = await Mediator.Send(getByIdIdShareQuery);
            return Ok(shareGetByIdDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateShareCommand createShareCommand)
        {
            CreateShareDTO result = await Mediator.Send(createShareCommand);
            return Created("", result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] EditShareCommand editShareCommand)
        {
            EditShareDTO result = await Mediator.Send(editShareCommand);
            return Created("", result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteShare([FromRoute] DeleteShareQuery deleteShareQuery)
        {
            await Mediator.Send(deleteShareQuery);
            return Ok("Removed");
        }
    }
}
