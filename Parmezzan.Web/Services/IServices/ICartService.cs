using Parmezzan.Web.Models.Dto;

namespace Parmezzan.Web.Services.IServices
{
    public interface ICartService
    {
        Task<T> GetCartByUserIdAsync<T>(string userId, string token = null);
        Task<T> AddToCartAsync<T>(CartDto cartDto, string token = null);
        Task<T> UpdateCartAsync<T>(CartDto cartDto, string token = null);
        Task<T> RemoveCartAsync<T>(int cartId, string token = null);
        Task<T>Checkout<T>(CartHeaderDto cartHeader, string token = null);
    }
}
