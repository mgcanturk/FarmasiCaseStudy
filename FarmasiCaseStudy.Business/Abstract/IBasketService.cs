using FarmasiCaseStudy.Core.Models;
using FarmasiCaseStudy.Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmasiCaseStudy.Business.Abstract
{
    public interface IBasketService
    {
        Task<GetOneResult<Basket>> GetBasket(string userSession);
        Task<GetOneResult<Basket>> AddToBasket(string userSession, string productId, int quantity);
        Task<GetOneResult<Basket>> DeleteBasketProduct(string userSession, string productId);
        Task<GetOneResult<Basket>> TakeOutBasket(string userSession, string productId, int quantity);
    }
}
