using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DapperRepo.Core.Data;
using DapperRepo.Core.Domain.Lit_Hold;
using DapperRepo.Data.Repositories.BaseInterfaces;
using SqlKata;

namespace DapperRepo.Data.Repositories.Mysql.Lit_Holds
{
    public class Lit_HoldRepository : MysqlRepositoryBase<Lit_Hold>, ILit_HoldRepository
    {
        public virtual async Task<Lit_Hold> GetLit_HoldByIdAsync(int id)
        {
            var lit_hold = await GetAsync(id);

            return lit_hold;
        }

        public virtual async Task<Lit_Hold> GetLit_HoldByAsync(string work_order, string case_name)
        {
            //returns ONE entry that matches either the work_order or case_name. I think this can be done better.  I don't like this.  
            var query = new Query(TableName).Select("work_order", "matter_no", "case_name", "begin_date", "end_date", "notes"); //.WhereFalse("Deleted");

            if (!string.IsNullOrEmpty(work_order))
            {
                query = query.WhereContains("case_name", case_name);
            }

            if (!string.IsNullOrEmpty(case_name))
            {
                query = query.WhereContains("work_order", work_order);
            }

            var sqlResult = GetSqlResult(query);

            return await GetFirstOrDefaultAsync(sqlResult.Sql, sqlResult.NamedBindings);
        }

        public virtual async Task<IEnumerable<Lit_Hold>> GetAllLit_HoldAsync()
        {
            var query = new Query(TableName).Select("work_order", "matter_no", "case_name", "begin_date", "end_date", "notes"); //.WhereFalse("Deleted");

            var sqlResult = GetSqlResult(query);

            return await GetListAsync(sqlResult.Sql, sqlResult.NamedBindings);
        }

        public virtual async Task<Tuple<int, IEnumerable<Lit_Hold>>> GetPagedLit_Holds(string work_order, string case_name, int pageIndex, int pageSize)
        {
            #region TotalCount

            IDbSession session = await Task.Run(() => DbSession);

            var totalCountQuery = new Query(TableName).AsCount(); //.WhereFalse("Deleted").AsCount();

            if (!string.IsNullOrEmpty(work_order))
            {
                totalCountQuery = totalCountQuery.WhereStarts("case_name", case_name);
            }
            if (!string.IsNullOrEmpty(case_name))
            {
                totalCountQuery = totalCountQuery.WhereStarts("work_order", work_order);
            }

            SqlResult totalCountResult = GetSqlResult(totalCountQuery);

            int totalCount = await session.Connection.QueryFirstOrDefaultAsync<int>(totalCountResult.Sql, totalCountResult.NamedBindings);

            #endregion

            #region Paged Lit_Holds

            int totalPage = totalCount <= pageSize ? 1 : totalCount > pageSize && totalCount < (pageSize * 2) ? 2 : totalCount / pageSize; // total pages

            int midPage = totalPage / 2 + 1; //The number of middle pages, if more than this number of pages, inverted optimization is used

            bool isLastPage = pageIndex == totalPage; // Whether the last page is the last page, it needs to be modulo to calculate the number of records on the last page（May be less than PageSize）

            int descBound = (totalCount - pageIndex * pageSize); // Recalculate the limit offset

            int lastPageSize = 0; // Count the number of records on the last page

            if (isLastPage)
            {
                lastPageSize = totalCount % pageSize; // Take the modulo to get the number of records on the last page
                descBound -= lastPageSize; // Recalculate the offset of the last page
            }
            else
            {
                descBound -= pageSize; // Normally recalculate the offset except the last page
            }

            bool useDescOrder = pageIndex <= midPage; // Determine whether to adopt inverted optimization

            Query customerQuery = new Query(TableName).Select("Id", "work_order", "matter_no", "case_name", "begin_date", "end_date", "notes"); //.WhereFalse("Deleted");

            if (!string.IsNullOrEmpty(work_order))
            {
                customerQuery = customerQuery.WhereStarts("case_name", case_name);
            }

            if (!string.IsNullOrEmpty(case_name))
            {
                customerQuery = customerQuery.WhereStarts("work_order", work_order);
            }

            customerQuery = customerQuery.Limit(isLastPage ? lastPageSize : pageSize).Offset(useDescOrder ? pageIndex * pageSize : descBound);

            customerQuery = useDescOrder ? customerQuery.OrderByDesc("Id") : customerQuery.OrderBy("Id");

            SqlResult customerResult = GetSqlResult(customerQuery);

            try
            {
                var customers = await session.Connection.QueryAsync<Lit_Hold>(customerResult.Sql, customerResult.NamedBindings);

                return new Tuple<int, IEnumerable<Lit_Hold>>(totalCount, customers);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                session.Dispose();
            }

            #endregion
        }

        public virtual async Task<int> InsertLit_HoldAsync(Lit_Hold lit_hold)
        {
            if (lit_hold == null)
                throw new ArgumentNullException(nameof(lit_hold));

            return await InsertAsync(lit_hold);
        }

        public virtual async Task<int> InsertLit_HoldListAsync(List<Lit_Hold> lit_holds)
        {
            if (lit_holds != null && lit_holds.Any())
            {
                StringBuilder builder = new StringBuilder(50);
                builder.AppendFormat("INSERT INTO `{0}`( work_order,matter_no,case_name, begin_date,end_date,notes ) VALUES ( @work_order,@matter_no,@case_name,@begin_date,@end_date,@notes );", TableName);

                int result = await ExecuteAsync(builder.ToString(), lit_holds);

                return result;
            }

            return 0;
        }

        public virtual async Task<bool> UpdateLit_HoldAsync(Lit_Hold lit_hold)
        {
            if (lit_hold == null)
                throw new ArgumentNullException(nameof(lit_hold));

            return await UpdateAsync(lit_hold);
        }

        public virtual async Task<bool> DeleteLit_HoldAsync(Lit_Hold lit_hold)
        {
            if (lit_hold == null)
                throw new ArgumentNullException(nameof(lit_hold));

            return await DeleteAsync(lit_hold);
        }
    }
}
