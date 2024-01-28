using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Application.Services.Repositories;
using Microsoft.AspNetCore.Components.Forms;
using Core.Application.Requests;
using MediatR;
using Application.Features.Portfolios.Queries.GetListPortfolio;
using Application.Features.Portfolios.Queries.GetPortfolio;
using Application.Features.Portfolios.Dtos;
using Application.Features.Portfolios.Models;
using Application.Features.Portfolios.Commands.CreatePortfolio;
using Application.Features.Portfolios.Commands.EditPortfolio;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListPortfolioQuery getListPortfolioQuery = new() { PageRequest = pageRequest };
            PortfolioListModel result = await Mediator.Send(getListPortfolioQuery);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute] GetByIdPortfolioQuery getByIdIdPortfolioQuery)
        {
            PortfolioGetByIdDto portfolioGetByIdDto = await Mediator.Send(getByIdIdPortfolioQuery);
            return Ok(portfolioGetByIdDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreatePortfolioCommand createPortfolioCommand)
        {
            CreatePortfolioDTO result = await Mediator.Send(createPortfolioCommand);
            return Created("", result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] EditPortfolioCommand editPortfolioCommand)
        {
            EditPortfolioDTO result = await Mediator.Send(editPortfolioCommand);
            return Created("", result);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeletePortfolio([FromRoute] DeletePortfolioQuery deletePortfolioQuery)
        {
            await Mediator.Send(deletePortfolioQuery);
            return Ok("Removed");
        }
    }
}
