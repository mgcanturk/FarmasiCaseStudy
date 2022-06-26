using FarmasiCaseStudy.Business.Abstract;
using FarmasiCaseStudy.Core.Models;
using FarmasiCaseStudy.Entities.Concreate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FarmasiCaseStudy.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IBasketService _basketService;
        public BasketController(IBasketService basketService, IHttpContextAccessor contextAccessor)
        {
            _basketService = basketService;
            _contextAccessor = contextAccessor;
            _contextAccessor.HttpContext.Session.SetString("Session", "FarmasiStore");
        }
        [HttpGet]
        public async Task<Result> GetBasket()
        {
            var RetVal = await _basketService.GetBasket("Basket_" + _contextAccessor.HttpContext.Session.Id.ToString());
            return RetVal;
        }
        [HttpPut]
        public async Task<Result> AddToBasket(string productId, int quantity)
        {
            var RetVal = await _basketService.AddToBasket("Basket_" + _contextAccessor.HttpContext.Session.Id.ToString(), productId, quantity);
            return RetVal;
        }

        [HttpPut]
        public async Task<Result> TakeOutBasket(string productId, int quantity)
        {
            var RetVal = await _basketService.TakeOutBasket("Basket_" + _contextAccessor.HttpContext.Session.Id.ToString(), productId, quantity);
            return RetVal;
        }
        [HttpDelete]
        public async Task<Result> DeleteProduct(string productId)
        {
            var RetVal = await _basketService.DeleteBasketProduct("Basket_" + _contextAccessor.HttpContext.Session.Id.ToString(), productId);
            return RetVal;
        }
    }
}
