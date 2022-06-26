using FarmasiCaseStudy.Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmasiCaseStudy.DataAccess.Abstract
{
    public interface IBasketRepository
    {
        Task<Basket> GetBasket(string userSession);
        Task<Basket> AddToBasket(string userSession, Basket basket);
        Task DeleteBasket(string userSession);
    }
}
