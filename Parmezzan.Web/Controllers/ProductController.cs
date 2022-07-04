using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Parmezzan.Web.Models.Dto;
using Parmezzan.Web.Services.IServices;
using System.Reflection;

namespace Parmezzan.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> ProductIndex()
        {
            var result = new List<ProductDto>();
            var response = await _productService.GetAllProductsAsync<ResponseDto>();
            if (response != null && response.IsSuccess)
            {
                result = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }

            return View(result);
        }

        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductCreate(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.CreateProductAsync<ResponseDto>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }            

            return View(model);
        }

        public async Task<IActionResult> ProductEdit(int productId)
{
            var response = await _productService.GetProductByIdAsync<ResponseDto>(productId);
            if (response != null && response.IsSuccess)
            {
                var result =JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(result);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductEdit(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.UpdateProductAsync<ResponseDto>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }

            return View(model);
        }

        public async Task<IActionResult> ProductDelete(int productId)
        {
            var response = await _productService.GetProductByIdAsync<ResponseDto>(productId);
            if (response != null && response.IsSuccess)
            {
                var result = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(result);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductDelete(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.DeleteProductAsync<ResponseDto>(model.ProductId);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }

            return View(model);
        }
    }
}
