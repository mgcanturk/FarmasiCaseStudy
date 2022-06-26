using FarmasiCaseStudy.Core.Models;
using FarmasiCaseStudy.Core.Repository.Abstract;
using FarmasiCaseStudy.Core.Settings;
using FarmasiCaseStudy.DataAccess.Context;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FarmasiCaseStudy.DataAccess.Repository
{
    public class MongoRepositoryBase<TEntity> : IDataGenericRepository<TEntity> where TEntity : class, new()
    {
        private readonly MongoDbContext _context;
        private readonly IMongoCollection<TEntity> _collection;
        public MongoRepositoryBase(IOptions<MongoSettings> settings)
        {
            _context = new MongoDbContext(settings);
            _collection = _context.GetCollection<TEntity>();
        }
        public async Task<GetManyResult<TEntity>> GetAllAsync()
        {
            var result = new GetManyResult<TEntity>();
            try
            {
                var data = await _collection.AsQueryable().ToListAsync();
                if (data != null)
                    result.ResultList = data;
                result.Message = "Success";
            }
            catch (Exception ex)
            {
                result.Message = $"AsQueryable {ex.Message}";
                result.ResultType = false;
            }
            return result;
        }

        public async Task<GetOneResult<TEntity>> DeleteByIdAsync(string id)
        {
            var result = new GetOneResult<TEntity>();
            try
            {
                var objectId = ObjectId.Parse(id);
                var filter = Builders<TEntity>.Filter.Eq("_id", objectId);
                var data = await _collection.FindOneAndDeleteAsync(filter);
                if (data != null)
                    result.Entity = data;
                result.Message = "Success";
            }
            catch (Exception ex)
            {
                result.Message = $"DeleteById {ex.Message}";
                result.ResultType = false;

            }
            return result;
        }
        public async Task DeleteManyAsync(Expression<Func<TEntity, bool>> filter)
        {
            await _collection.DeleteManyAsync(filter);
        }
        public async Task<GetOneResult<TEntity>> DeleteOneAsync(Expression<Func<TEntity, bool>> filter)
        {
            var result = new GetOneResult<TEntity>();
            try
            {
                var deleteDocument = await _collection.FindOneAndDeleteAsync(filter);
                result.Entity = deleteDocument;
            }
            catch (Exception ex)
            {
                result.Message = $"DeleteOneAsync {ex.Message}";
                result.ResultType = false;

            }
            return result;
        }
        public async Task<GetManyResult<TEntity>> FilterByAsync(Expression<Func<TEntity, bool>> filter)
        {
            var result = new GetManyResult<TEntity>();
            try
            {
                var data = await _collection.Find(filter).ToListAsync();
                if (data != null)
                    result.ResultList = data;
                result.Message = "Success";
            }
            catch (Exception ex)
            {
                result.Message = $"FilterBy {ex.Message}";
                result.ResultType = false;

            }
            return result;
        }
        public async Task<GetOneResult<TEntity>> GetByIdAsync(string id, string type = "object")
        {
            var result = new GetOneResult<TEntity>();
            try
            {
                object objectId = null;
                if (type == "guid")
                    objectId = Guid.Parse(id);
                else
                    objectId = ObjectId.Parse(id);

                var filter = Builders<TEntity>.Filter.Eq("_id", objectId);
                var data = await _collection.Find(filter).FirstOrDefaultAsync();
                if (data != null)
                    result.Entity = data;
                result.Message = "Success";
            }
            catch (Exception ex)
            {
                result.Message = $"GetById {ex.Message}";
                result.ResultType = false;

            }
            return result;
        }

        public async Task<GetManyResult<TEntity>> InsertManyAsync(ICollection<TEntity> entities)
        {
            var result = new GetManyResult<TEntity>();
            try
            {
                await _collection.InsertManyAsync(entities);
                result.ResultList = entities;
                result.Message = "Success";
            }
            catch (Exception ex)
            {
                result.Message = $"InsertManyAsync {ex.Message}";
                result.ResultType = false;

            }
            return result;
        }
        public async Task<GetOneResult<TEntity>> InsertOneAsync(TEntity entity)
        {
            var result = new GetOneResult<TEntity>();
            try
            {
                await _collection.InsertOneAsync(entity);
                result.Entity = entity;
                result.Message = "Success";
            }
            catch (Exception ex)
            {
                result.Message = $"InsertOneAsync {ex.Message}";
                result.ResultType = false;

            }
            return result;
        }
        public async Task<GetOneResult<TEntity>> ReplaceOneAsync(TEntity entity, string id, string type = "object")
        {
            var result = new GetOneResult<TEntity>();
            try
            {
                object objectId = null;
                if (type == "guid")
                    objectId = Guid.Parse(id);
                else
                    objectId = ObjectId.Parse(id);

                var filter = Builders<TEntity>.Filter.Eq("_id", objectId);
                var updatedDocument = await _collection.ReplaceOneAsync(filter, entity);
                result.Entity = entity;
                result.Message = "Success";
            }
            catch (Exception ex)
            {
                result.Message = $"GetById {ex.Message}";
                result.ResultType = false;

            }
            return result;
        }
    }
}
