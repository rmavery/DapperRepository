using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DapperRepo.Core.Data;
using DapperRepo.Core.Domain.Lit_Hold;

namespace DapperRepo.Data.Repositories.BaseInterfaces
{
    public interface ILit_HoldRepository : IRepository<Lit_Hold>
    {
        Task<Lit_Hold> GetLit_HoldByIdAsync(int id);

        Task<Lit_Hold> GetLit_HoldByAsync(string work_order, string case_name);

        Task<IEnumerable<Lit_Hold>> GetAllLit_HoldAsync();

        Task<Tuple<int, IEnumerable<Lit_Hold>>> GetPagedLit_Holds(string work_order, string case_name, int pageIndex, int pageSize);

        Task<int> InsertLit_HoldAsync(Lit_Hold lit_holds);

        Task<int> InsertLit_HoldListAsync(List<Lit_Hold> lit_holds);

        Task<bool> UpdateLit_HoldAsync(Lit_Hold lit_holds);

        Task<bool> DeleteLit_HoldAsync(Lit_Hold lit_holds);
    }
}


//--public class lit_hold_users
//--{
//--    public int id { get; set; }
//--    public string display_name { get; set; }
//--    public string email_address { get; set; }
//--}

//--public class lit_holds
//--{
//--    public int Id { get; set; }
//--    public string work_order { get; set; }
//--    public DateTime? begin_date { get; set; }
//--    public DateTime? end_date { get; set; }
//--    public string case_name { get; set; }
//--    public string notes { get; set; }
//--}