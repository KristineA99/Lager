using Microsoft.EntityFrameworkCore;
using Lager.Models;

namespace Lager.DAL;

public class SupplyRepository : ISupplyRepository
{
    private readonly LagerDbContext _db;

    public SupplyRepository(LagerDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Supply>?> GetAll()
    {
        return await _db.Supplies.ToListAsync();
    }

    public async Task<Supply?> GetSupplyById(int id)
    {
        return await _db.Supplies.FindAsync(id);
    }

    public async Task<bool> Create(Supply supply)
    {
        _db.Supplies.Add(supply);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Update(Supply supply)
    {
        _db.Supplies.Update(supply);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var supply = await _db.Supplies.FindAsync(id);
        if (supply == null)
        {
            return false;
        }

        _db.Supplies.Remove(supply);
        await _db.SaveChangesAsync();
        return true;

    }

    public async Task<IEnumerable<Supply>?> GetLowStock()
{
    // Henter alle innsatsvarer der beholdningen er på eller under minimum
    return await _db.Supplies
        .Where(p => p.QuantityInStock <= p.MinimumStock)
        .ToListAsync();
}

public async Task<bool> AdjustStock(int id, decimal change)
{
    var supply = await _db.Supplies.FindAsync(id);
    if (supply == null)
        return false;   // innsatsvare finnes ikke

    // Forretningsregel: beholdningen kan aldri bli negativ
    if (supply.QuantityInStock + change < 0)
        return false;

    supply.QuantityInStock += change;
    await _db.SaveChangesAsync();
    return true;
}

}