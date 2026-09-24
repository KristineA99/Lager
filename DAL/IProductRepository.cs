using Lager.Models;

namespace Lager.DAL
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>?> GetAll();
        Task<Product?> GetProductById(int id);
        Task<bool> Create(Product product);
        Task<bool> Update(Product product);
        Task<bool> Delete(int id);

        // Nye metoder som ikke finnes i demoen:
        Task<IEnumerable<Product>?> GetLowStock();         // produkter under minimum
        Task<bool> AdjustStock(int id, int change);        // + legger til, - trekker fra
    }
}