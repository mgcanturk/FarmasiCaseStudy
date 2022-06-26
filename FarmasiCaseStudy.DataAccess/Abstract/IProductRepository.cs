using FarmasiCaseStudy.Core.Models;
using FarmasiCaseStudy.Core.Repository.Abstract;
using FarmasiCaseStudy.Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmasiCaseStudy.DataAccess.Abstract
{
    public interface IProductRepository : IDataGenericRepository<Product>
    {
        Task<GetOneResult<Product>> CreateProductWithCodeControlAsync(Product model);
        Task<GetOneResult<Product>> UpdateProductAsync(string id, Product model);
    }
}
