using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DapperRepo.Core.Domain.LitHold;

namespace DapperRepo.Services.BaseInterfaces
{
    public interface ILitHoldService
    {
        Task<Lit_Hold> GetLit_HoldByIdAsync(int id);

        Task<Lit_Hold> GetLit_HoldByAsync(string name, string email);

        Task<IEnumerable<Lit_Hold>> GetAllLit_HoldsAsync();

        Task<Tuple<int, IEnumerable<Lit_Hold>>> GetPagedLit_Holds(string username, string email, int pageIndex, int pageSize);

        Task<int> InsertCustomerAsync(Lit_Hold lit_hold);

        Task<int> InsertCustomerListAsync(List<Lit_Hold> lit_hold);

        Task<bool> UpdateCustomerAsync(Lit_Hold lit_hold);

        Task<bool> DeleteCustomerAsync(Lit_Hold lit_hold);
    }
}
