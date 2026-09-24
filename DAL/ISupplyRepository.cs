using Lager.Models;

namespace Lager.DAL
{
    public interface ISupplyRepository
    {
        Task<IEnumerable<Supply>?> GetAll();
        Task<Supply?> GetSupplyById(int id);
        Task<bool> Create(Supply supply);
        Task<bool> Update(Supply supply);
        Task<bool> Delete(int id);

        Task<IEnumerable<Supply>?> GetLowStock();           // innsatsvarer under minimum
        Task<bool> AdjustStock(int id, decimal change);     // decimal: kan være f.eks. 2,5 kg
    }
}