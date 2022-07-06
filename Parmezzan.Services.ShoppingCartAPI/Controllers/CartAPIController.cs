using Microsoft.AspNetCore.Mvc;
using Parmezzan.Services.ShoppingCartAPI.Messages;
using Parmezzan.Services.ShoppingCartAPI.Models.Dto;
using Parmezzan.Services.ShoppingCartAPI.RabbitMQSender;
using Parmezzan.Services.ShoppingCartAPI.Repository;

namespace Parmezzan.Services.ShoppingCartAPI.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartAPIController : Controller
    {
        private readonly ICartRepository _cartRepository;
        protected ResponseDto _response;
        private readonly IRabbitMQCartMessageSender _rabbitMQCartMessageSender;

        public CartAPIController(ICartRepository cartRepository, IRabbitMQCartMessageSender rabbitMQCartMessageSender)
        {
            _cartRepository = cartRepository;
            _response = new ResponseDto();
            _rabbitMQCartMessageSender = rabbitMQCartMessageSender;
        }

        [HttpGet("GetCart/{userId}")]
        public async Task<object> GetCart(string userId)
        {
            try
            {
                _response.Result = await _cartRepository.GetCartByUserId(userId);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost("AddCart")]
        public async Task<object> AddCart(CartDto cartDto)
        {
            try
            {
                _response.Result = await _cartRepository.CreateUpdateCart(cartDto);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost("UpdateCart")]
        public async Task<object> UpdateCart(CartDto cartDto)
        {
            try
            {
                _response.Result = await _cartRepository.CreateUpdateCart(cartDto);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost("RemoveCart")]
        public async Task<object> RemoveCart([FromBody] int cartId)
        {
            try
            {
                _response.Result = await _cartRepository.RemoveFromCart(cartId);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost("ClearCart")]
        public async Task<object>ClearCart([FromBody] string userId)
        {
            try
            {
                _response.Result = await _cartRepository.ClearCart(userId);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }



        [HttpPost("Checkout")]
        public async Task<object> Checkout(CheckoutHeaderDto checkoutHeader)
        {
            try
            {
                var cartDto = await _cartRepository.GetCartByUserId(checkoutHeader.UserId);

                if (cartDto == null)
                {
                    return BadRequest();
                }

                checkoutHeader.CartDetails = cartDto.CartDetails;

                _rabbitMQCartMessageSender.SendMessage(checkoutHeader, "checkoutqueue");
                await _cartRepository.ClearCart(checkoutHeader.UserId);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
