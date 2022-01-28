using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _produtRepository;

        public ProductsController(IProductRepository produtRepository)
        {
            _produtRepository = produtRepository;
        }



        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {

            var products = await _produtRepository.GetProductsAsync();
            return Ok(products); 
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id) 
        {
            var product = await _produtRepository.GetProductByIDAsync(id);
            return Ok(product);
        }
    }
}
