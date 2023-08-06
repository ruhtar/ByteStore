using AuthenticationService.Domain.Entities;
using AuthenticationService.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repository
{
    public class OrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        //public async Task MakeOrder(List<int> productIds, int shoppingCartId)
        //{
        //    var products = new List<Product>();
        //    foreach (var productId in productIds)
        //    {
        //        var product = await _context.Products.FindAsync(productId);
        //        products.Add(product);
        //    }
        //    var order = new Order
        //    {
        //        ShoppingCartId = shoppingCartId,
        //    };
        //}
    }
}
