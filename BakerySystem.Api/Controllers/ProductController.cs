using BakerySystem.Domain.Entities;
using BakerySystem.Repositories.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BakerySystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;

        public ProductController(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _repositoryManager.ProductRepository.GetAllProducts();
            return Ok(result);
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct(Product product, CancellationToken token)
        {
            var result = _repositoryManager.ProductRepository.Create(product);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken token)
        {
            var result = await _repositoryManager.ProductRepository.GetByIdAsync(id, token);
            return Ok(result);
        }

        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(Product product, CancellationToken token)
        {
            _repositoryManager.ProductRepository.Update(product);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Product product, CancellationToken token)
        {
            _repositoryManager.ProductRepository.Delete(product);
            return NoContent();
        }
    }
}
