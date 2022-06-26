using FarmasiCaseStudy.Core.Models;
using FarmasiCaseStudy.Core.Settings;
using FarmasiCaseStudy.DataAccess.Abstract;
using FarmasiCaseStudy.DataAccess.Context;
using FarmasiCaseStudy.DataAccess.Repository;
using FarmasiCaseStudy.Entities.Concreate;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmasiCaseStudy.DataAccess.Concrete
{
    public class ProductRepository : MongoRepositoryBase<Product>, IProductRepository
    {
        private readonly MongoDbContext _context;
        private readonly IMongoCollection<Product> _collection;
        public ProductRepository(IOptions<MongoSettings> settings) : base(settings)
        {
            _context = new MongoDbContext(settings);
            _collection = _context.GetCollection<Product>();
        }

        public async Task<GetOneResult<Product>> CreateProductWithCodeControlAsync(Product model)
        {
            var result = new GetOneResult<Product>();
            try
            {
                var filter = Builders<Product>.Filter.Eq("Code", model.Code);
                var data = await _collection.Find(filter).FirstOrDefaultAsync();

                if (data == null)
                {
                    await _collection.InsertOneAsync(model);
                    result.Message = "Success";
                }
                else
                {
                    result.Message = $"This product has already been added.";
                    result.ResultType = false;
                }
                result.Entity = model;
            }
            catch (Exception ex)
            {
                result.Message = $"CreateProductWithCodeControl {ex.Message}";
                result.ResultType = false;
            }
            return result;
        }

        public async Task<GetOneResult<Product>> UpdateProductAsync(string id, Product model)
        {
            var result = new GetOneResult<Product>();
            try
            {
                var filter = Builders<Product>.Filter.Eq("_id", ObjectId.Parse(id));
                var update = Builders<Product>.Update.Set("LastUpdatedTime", DateTime.Now);
                var type = model.GetType();
                foreach (var property in type.GetProperties())
                {
                    var GetValue = property.GetValue(model, null) != null ? property.GetValue(model, null).ToString() : "";
                    if (!string.IsNullOrWhiteSpace(GetValue))
                        update = update.Set(property.Name, GetValue);
                }
                //if (!string.IsNullOrWhiteSpace(model.Name))
                //    update = update.Set("Name", model.Name);
                //if (!string.IsNullOrWhiteSpace(model.Code))
                //    update = update.Set("Code", model.Code);
                //if (!string.IsNullOrWhiteSpace(model.Category))
                //    update = update.Set("Category", model.Category);
                //if (model.Onhand.HasValue)
                //    update = update.Set("Onhand", model.Onhand);
                //if (model.Price.HasValue)
                //    update = update.Set("Price", model.Price);
                await _collection.FindOneAndUpdateAsync(filter, update);
                result.Entity = model;
                result.Message = "Success";
            }
            catch (Exception ex)
            {
                result.Message = $"UpdateProduct {ex.Message}";
                result.ResultType = false;
            }
            return result;
        }
    }
}
