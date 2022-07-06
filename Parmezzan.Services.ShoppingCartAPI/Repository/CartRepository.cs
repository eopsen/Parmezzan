using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Parmezzan.Services.ShoppingCartAPI.DbContextes;
using Parmezzan.Services.ShoppingCartAPI.Models;
using Parmezzan.Services.ShoppingCartAPI.Models.Dto;

namespace Parmezzan.Services.ShoppingCartAPI.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public CartRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<bool> ClearCart(string userId)
        {
            var cartHeaderFromDb = await _db.CartHeaders.AsNoTracking()
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cartHeaderFromDb != null)
            {
                _db.CartDetails.RemoveRange(_db.CartDetails.Where(u => u.CartHeaderId == cartHeaderFromDb.CartHeaderId));
                _db.CartHeaders.Remove(cartHeaderFromDb);
                await _db.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<CartDto> CreateUpdateCart(CartDto cartDto)
        {
            Cart cart = _mapper.Map<Cart>(cartDto);

            var prodInDb = _db.Products
                .FirstOrDefault(p => p.ProductId == cart.CartDetails.FirstOrDefault().ProductId);

            if (prodInDb == null)
            {
                _db.Products.Add(cart.CartDetails.FirstOrDefault().Product);
                await _db.SaveChangesAsync();
            }

            var cartHeaderFromDb = await _db.CartHeaders.AsNoTracking()
                .FirstOrDefaultAsync(c => c.UserId == cart.CartHeader.UserId);

            if (cartHeaderFromDb == null)
            {
                _db.CartHeaders.Add(cart.CartHeader);
                await _db.SaveChangesAsync();
                cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.CartHeaderId;
                cart.CartDetails.FirstOrDefault().Product = null;
                _db.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                await _db.SaveChangesAsync();
            }
            else
            {
                var cartDetailsFromDb = await _db.CartDetails
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.ProductId == cart.CartDetails.FirstOrDefault().ProductId
                    && u.CartHeaderId == cartHeaderFromDb.CartHeaderId);

                if (cartDetailsFromDb == null)
                {
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartHeaderFromDb.CartHeaderId;
                    cart.CartDetails.FirstOrDefault().Product = null;
                    _db.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                    await _db.SaveChangesAsync();
                }
                else
                {
                    cart.CartDetails.FirstOrDefault().Count += cartDetailsFromDb.Count;
                    cart.CartDetails.FirstOrDefault().Product = null;
                    cart.CartDetails.FirstOrDefault().CartDetailId = cartDetailsFromDb.CartDetailId;
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartDetailsFromDb.CartHeaderId;
                    _db.CartDetails.Update(cart.CartDetails.FirstOrDefault());
                    await _db.SaveChangesAsync();
                }
            }

            return _mapper.Map<CartDto>(cart);
        }

        public async Task<CartDto> GetCartByUserId(string userId)
        {
            Cart cart = new() { 
                CartHeader = await _db.CartHeaders.FirstOrDefaultAsync(p => p.UserId == userId)
            };
            cart.CartDetails = _db.CartDetails
                .Include(p => p.Product)
                .Where(p => p.CartHeaderId == cart.CartHeader.CartHeaderId);

            return _mapper.Map<CartDto>(cart);
        }

        public async Task<bool> RemoveFromCart(int cartDetailsId)
        {
            try
            {

                var cartDetails = await _db.CartDetails.FirstOrDefaultAsync(p => p.CartDetailId == cartDetailsId);

                int totalCountOfCartItems = _db.CartDetails
                    .Where(p => p.CartHeaderId == cartDetails.CartHeaderId)
                    .Count();

                _db.CartDetails.Remove(cartDetails);

                if (totalCountOfCartItems == 1)
                {
                    var cartHeaderToRemove = await _db.CartHeaders.FirstOrDefaultAsync(u => u.CartHeaderId == cartDetails.CartHeaderId);
                    _db.CartHeaders.Remove(cartHeaderToRemove);
                }

                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
