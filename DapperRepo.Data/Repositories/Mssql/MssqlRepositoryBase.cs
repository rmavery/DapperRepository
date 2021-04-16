using DapperRepo.Core.Constants;
using DapperRepo.Core.Domain;
using SqlKata;
using SqlKata.Compilers;

namespace DapperRepo.Data.Repositories.Mssql
{
    public class MssqlRepositoryBase<T> : RepositoryBase<T> where T : BaseEntity
    {
        protected sealed override DatabaseType DataType => DatabaseType.Mssql;

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        protected override string ConnStrKey => ConnKeyConstants.MssqlMasterKey;

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        protected override string TableName => typeof(T).Name;

        protected override SqlResult GetSqlResult(Query query)
        {
            var compiler = new SqlServerCompiler();

            SqlResult sqlResult = compiler.Compile(query);

            return sqlResult;
        }
    }
}
