using FarmasiCaseStudy.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FarmasiCaseStudy.Core.Repository.Abstract
{
    public interface IDataGenericRepository<TEntity> where TEntity : class, new()
    {
        Task<GetManyResult<TEntity>> GetAllAsync();
        Task<GetManyResult<TEntity>> FilterByAsync(Expression<Func<TEntity, bool>> filter);
        Task<GetOneResult<TEntity>> GetByIdAsync(string id, string type = "object");
        Task<GetOneResult<TEntity>> InsertOneAsync(TEntity entity);
        Task<GetManyResult<TEntity>> InsertManyAsync(ICollection<TEntity> entities);
        Task<GetOneResult<TEntity>> ReplaceOneAsync(TEntity entity, string id, string type = "object");
        Task<GetOneResult<TEntity>> DeleteOneAsync(Expression<Func<TEntity, bool>> filter);
        Task<GetOneResult<TEntity>> DeleteByIdAsync(string id);
        Task DeleteManyAsync(Expression<Func<TEntity, bool>> filter);
    }
}
