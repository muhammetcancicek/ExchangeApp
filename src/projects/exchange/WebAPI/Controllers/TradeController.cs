using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Application.Services.Repositories;
using Microsoft.AspNetCore.Components.Forms;
using Core.Application.Requests;
using MediatR;
using Application.Features.Trades.Queries.GetListTrade;
using Application.Features.Trades.Queries.GetTrade;
using Application.Features.Trades.Dtos;
using Application.Features.Trades.Models;
using Application.Features.Trades.Commands.CreateTrade;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradeController : BaseController
    {
        [HttpGet("ByPortfolioId")]
        public async Task<IActionResult> GetListByPortfolioId([FromQuery] PageRequest pageRequest, int portfolioId)
        {
            GetListTradeByPortfolioIdQuery getListTradeByPortfolioIdQuery = new() { PageRequest = pageRequest, PortfolioId = portfolioId};
            TradeListModel result = await Mediator.Send(getListTradeByPortfolioIdQuery);
            return Ok(result);
        }
        [HttpGet("ByShareId")]
        public async Task<IActionResult> GetListByShareId([FromQuery] PageRequest pageRequest, int shareId)
        {
            GetListTradeByShareIdQuery getListTradeByShareIdQuery = new() { PageRequest = pageRequest, ShareId = shareId };
            TradeListModel result = await Mediator.Send(getListTradeByShareIdQuery);
            return Ok(result);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute] GetByIdTradeQuery getByIdIdTradeQuery)
        {
            TradeGetByIdDto tradeGetByIdDto = await Mediator.Send(getByIdIdTradeQuery);
            return Ok(tradeGetByIdDto);
        }
        [HttpPost("Buy")]
        public async Task<IActionResult> Buy([FromBody] BuyShareCommand createTradeCommand)
        {
            BuyAndSellShareDTO result = await Mediator.Send(createTradeCommand);
            return Created("", result);
        }
        [HttpPost("Sell")]
        public async Task<IActionResult> Sell([FromBody] SellShareCommand sellTradeCommand)
        {
            BuyAndSellShareDTO result = await Mediator.Send(sellTradeCommand);
            return Created("", result);
        }







    }
}
