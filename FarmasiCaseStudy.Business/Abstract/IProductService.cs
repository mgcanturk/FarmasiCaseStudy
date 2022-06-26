using FarmasiCaseStudy.Core.Models;
using FarmasiCaseStudy.Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmasiCaseStudy.Business.Abstract
{
    public interface IProductService
    {
        Task<GetManyResult<Product>> GetAllProductsAsync();
        Task<GetOneResult<Product>> GetProductByIdAsync(string id);
        Task<GetOneResult<Product>> CreateProductAsync(Product model);
        Task<GetOneResult<Product>> UpdateProductAsync(string id, Product model);
        Task<GetOneResult<Product>> DeleteProductAsync(string id);
    }
}
