using Microsoft.EntityFrameworkCore;
using Lager.Models;

namespace Lager.DAL;

public class ProductRepository : IProductRepository
{
    private readonly LagerDbContext _db;

    public ProductRepository(LagerDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Product>?> GetAll()
    {
        return await _db.Products.ToListAsync();
    }

    public async Task<Product?> GetProductById(int id)
    {
        return await _db.Products.FindAsync(id);
    }

    public async Task<bool> Create(Product product)
    {
        _db.Products.Add(product);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Update(Product product)
    {
        _db.Products.Update(product);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var product = await _db.Products.FindAsync(id);
        if (product == null)
        {
            return false;
        }

        _db.Products.Remove(product);
        await _db.SaveChangesAsync();
        return true;

    }

    public async Task<IEnumerable<Product>?> GetLowStock()
{
    // Henter alle produkter der beholdningen er på eller under minimum
    return await _db.Products
        .Where(p => p.QuantityInStock <= p.MinimumStock)
        .ToListAsync();
}

public async Task<bool> AdjustStock(int id, int change)
{
    var product = await _db.Products.FindAsync(id);
    if (product == null)
        return false;   // produktet finnes ikke

    // Forretningsregel: beholdningen kan aldri bli negativ
    if (product.QuantityInStock + change < 0)
        return false;

    product.QuantityInStock += change;
    await _db.SaveChangesAsync();
    return true;
}

}