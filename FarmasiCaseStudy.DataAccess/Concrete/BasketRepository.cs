using FarmasiCaseStudy.Core.Repository.Abstract;
using FarmasiCaseStudy.DataAccess.Abstract;
using FarmasiCaseStudy.Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmasiCaseStudy.DataAccess.Concrete
{
    public class BasketRepository : IBasketRepository
    {
        private readonly ICacheGenericRepository _redisCache;

        public BasketRepository(ICacheGenericRepository cache)
        {
            _redisCache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task<Basket> GetBasket(string userSession)
        {
            return await _redisCache.GetAsync<Basket>(userSession);
        }

        public async Task<Basket> AddToBasket(string userSession, Basket basket)
        {
            await _redisCache.SetAsync(userSession, basket);
            return await GetBasket(userSession);
        }

        public async Task DeleteBasket(string userSession)
        {
            await _redisCache.RemoveAsync(userSession);
        }
    }
}
