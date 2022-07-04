using Microsoft.AspNetCore.Mvc;
using Parmezzan.Services.ProductAPI.Models.Dto;
using Parmezzan.Services.ProductAPI.Repository;

namespace Parmezzan.Services.ProductAPI.Controllers
{
    [Route("api/products")]
    public class ProductAPIController : ControllerBase
    {
        protected ResponseDto _response;
        private IProductRepository _productRespository;

        public ProductAPIController(IProductRepository productRepository)
        {
            _productRespository = productRepository;
            _response = new ResponseDto();
        }

        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                var productsDtos = await _productRespository.GetProducts();
                _response.Result = productsDtos;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<object> Get(int id)
        {
            try
            {
                var productsDto = await _productRespository.GetProductById(id);
                _response.Result = productsDto;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;
        }

        [HttpPost]
        public async Task<object> Post([FromBody] ProductDto productDto)
        {
            try
            {
                var result = await _productRespository.CreateUpdateProduct(productDto);
                _response.Result = result;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;
        }

        [HttpPut]
        public async Task<object> Put([FromBody] ProductDto productDto)
        {
            try
            {
                var result = await _productRespository.CreateUpdateProduct(productDto);
                _response.Result = result;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<object> Delete(int id)
        {
            try
            {
                var isSUccess = await _productRespository.DeleteProduct(id);
                _response.Result = isSUccess;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;
        }
    }
}
