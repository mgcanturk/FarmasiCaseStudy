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
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IProductService _productService;
        public BasketService(IBasketRepository _basketRepository, IProductService _productService)
        {
            this._basketRepository = _basketRepository;
            this._productService = _productService;
        }
        public async Task<GetOneResult<Basket>> GetBasket(string userSession)
        {
            var result = new GetOneResult<Basket>();
            try
            {
                var basket = await _basketRepository.GetBasket(userSession);
                if (basket == null)
                    basket = new Basket();
                result.Message = "Success";
                result.Entity = basket;
            }
            catch (Exception ex)
            {
                result.Message = $"GetBasket {ex.Message}";
                result.ResultType = false;
            }
            return result;
           
        }
        public async Task<GetOneResult<Basket>> AddToBasket(string userSession, string productId, int quantity)
        {
            var result = new GetOneResult<Basket>();
            try
            {
                var basket = new Basket();
                var getProduct = _productService.GetProductByIdAsync(productId);
                if (getProduct.Result.ResultType)
                {
                    if (getProduct.Result.Entity.Onhand > 0)
                    {
                        basket = GetBasket(userSession).Result.Entity;
                        basket.AddProduct(getProduct.Result.Entity, quantity);
                        result.Message = "Success";
                        result.Entity = await _basketRepository.AddToBasket(userSession, basket);
                    }
                    else
                    {
                        result.Message = "The product is out of stock.";
                        result.ResultType = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Message = $"AddToBasket {ex.Message}";
                result.ResultType = false;
            }
            return result;
           
        }

        public async Task<GetOneResult<Basket>> DeleteBasketProduct(string userSession, string productId)
        {
            var result = new GetOneResult<Basket>();
            try
            {
                var basket = new Basket();
                var getProduct = _productService.GetProductByIdAsync(productId);
                if (getProduct.Result.ResultType)
                {
                    basket = GetBasket(userSession).Result.Entity;
                    basket.DeleteProduct(getProduct.Result.Entity);
                }
                result.Message = "Success";
                result.Entity = await _basketRepository.AddToBasket(userSession, basket);
            }
            catch (Exception ex)
            {
                result.Message = $"DeleteBasketProduct {ex.Message}";
                result.ResultType = false;
            }
            return result;
            
        }

        public async Task<GetOneResult<Basket>> TakeOutBasket(string userSession, string productId, int quantity)
        {
            var result = new GetOneResult<Basket>();
            try
            {
                var basket = new Basket();
                var getProduct = _productService.GetProductByIdAsync(productId);
                if (getProduct.Result.ResultType)
                {
                    basket = GetBasket(userSession).Result.Entity;
                    basket.TakeOutProduct(getProduct.Result.Entity, quantity);
                }
                result.Message = "Success";
                result.Entity = await _basketRepository.AddToBasket(userSession, basket);
            }
            catch (Exception ex)
            {
                result.Message = $"DiscartToBasket {ex.Message}";
                result.ResultType = false;
            }
            return result;
            
        }
    }
}
