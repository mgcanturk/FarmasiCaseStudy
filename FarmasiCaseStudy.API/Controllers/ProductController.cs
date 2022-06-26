using FarmasiCaseStudy.Business.Abstract;
using FarmasiCaseStudy.Core.Models;
using FarmasiCaseStudy.Entities.Concreate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FarmasiCaseStudy.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<Result> GetProducts()
        {
            var RetVal = await _productService.GetAllProductsAsync();
            return RetVal;
        }
        [HttpPost]
        public async Task<Result> CreateProduct([FromBody] Product model)
        {
            var RetVal = await _productService.CreateProductAsync(model);
            return RetVal;
        }
        [HttpPut("{id:length(24)}")]
        public async Task<Result> UpdateProduct(string id, [FromBody] Product model)
        {
            var RetVal = await _productService.UpdateProductAsync(id, model);
            return RetVal;
        }
        [HttpDelete("{id:length(24)}")]
        public async Task<Result> DeleteProduct(string id)
        {
            var RetVal = await _productService.DeleteProductAsync(id);
            return RetVal;
        }
    }
}
