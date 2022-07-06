using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Parmezzan.Web.Models;
using Parmezzan.Web.Models.Dto;
using Parmezzan.Web.Services.IServices;
using System.Diagnostics;

namespace Parmezzan.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public HomeController(ILogger<HomeController> logger, IProductService productService, ICartService cartService)
        {
            _logger = logger;
            _productService = productService;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            var result = new List<ProductDto>();
            var response = await _productService.GetAllProductsAsync<ResponseDto>("");
            if (response != null && response.IsSuccess)
            {
                result = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }

            return View(result);
        }

        [Authorize]
        public async Task<IActionResult> Details(int productId)
        {
            ProductDto result = null;
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.GetProductByIdAsync<ResponseDto>(productId, accessToken);
            if (response != null && response.IsSuccess)
            {
                result = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
            }

            return View(result);
        }

        [HttpPost]
        [ActionName("Details")]
        [Authorize]
        public async Task<IActionResult> DetailsPost(ProductDto productDto)
        {
            var cart = new CartDto()
            {
                CartHeader = new CartHeaderDto()
                {
                    UserId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault().Value
                }
            };

            var cartDetails = new CartDetailDto()
            {
                Count = productDto.Count,
                ProductId = productDto.ProductId
            };

            var productResponse = await _productService.GetProductByIdAsync<ResponseDto>(productDto.ProductId, "");
            if (productResponse != null && productResponse.IsSuccess)
            {
                cartDetails.Product = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(productResponse.Result));
            }

            cart.CartDetails = new List<CartDetailDto>() { cartDetails };

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _cartService.AddToCartAsync<ResponseDto>(cart, accessToken);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(productDto);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public IActionResult Login()
        {
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }
    }
}