using BakerySystem.Domain.Entities;
using BakerySystem.Repositories.Contracts;
using BakerySystem.Services.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakerySystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IOrderService _orderService;

        public OrderController(IRepositoryManager repositoryManager, IOrderService orderService)
        {
            _orderService = orderService;
            _repositoryManager = repositoryManager;
        }

        [HttpGet("GetAllOrders")]
        public async Task<IActionResult> GetAllOrders(CancellationToken token)
        {
            var result = await _repositoryManager.OrderRepository.GetAllOrders();
            return Ok(result);
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder(Order order, CancellationToken token)
        {
            await _orderService.CreateOrderAsync(order);
            return Ok();
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken token)
        {
            var result = await _repositoryManager.OrderRepository.GetByIdAsync(id, token);
            return Ok(result);
        }

        [HttpPut("UpdateOrder")]
        public async Task<IActionResult> UpdateOrder(Order order, CancellationToken token)
        {
            await _repositoryManager.OrderRepository.UpdateOrder(order);
            return NoContent();
        }
    }
}
