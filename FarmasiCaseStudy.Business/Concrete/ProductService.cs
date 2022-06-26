using FarmasiCaseStudy.Business.Abstract;
using FarmasiCaseStudy.Core.Models;
using FarmasiCaseStudy.DataAccess.Abstract;
using FarmasiCaseStudy.Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmasiCaseStudy.Business.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository _productRepository)
        {
            this._productRepository = _productRepository;
        }
        public async Task<GetManyResult<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }
        public async Task<GetOneResult<Product>> GetProductByIdAsync(string id)
        {
            return await _productRepository.GetByIdAsync(id);
        }
        public async Task<GetOneResult<Product>> CreateProductAsync(Product model)
        {
            return await _productRepository.CreateProductWithCodeControlAsync(model);
        }

        public async Task<GetOneResult<Product>> UpdateProductAsync(string id, Product model)
        {
            return await _productRepository.UpdateProductAsync(id, model);
        }

        public async Task<GetOneResult<Product>> DeleteProductAsync(string id)
        {
            return await _productRepository.DeleteByIdAsync(id);
        }
    }
}
