namespace Ecommerce.Infrastructure.Repository
{
    public interface IShoppingCartRepository
    {
        Task CreateShoppingCart(int userAggregateId);
    }
}