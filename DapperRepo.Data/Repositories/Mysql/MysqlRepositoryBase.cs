using DapperRepo.Core.Constants;
using DapperRepo.Core.Domain;
using SqlKata;
using SqlKata.Compilers;

namespace DapperRepo.Data.Repositories.Mysql
{
    public class MysqlRepositoryBase<T> : RepositoryBase<T> where T : BaseEntity
    {
        protected sealed override DatabaseType DataType => DatabaseType.Mysql;

        /// <inheritdoc />
        /// <summary>
        /// Of the current database connection string key(Default master database key)
        /// </summary>
        protected override string ConnStrKey => ConnKeyConstants.MysqlMasterKey;

        /// <inheritdoc />
        /// <summary>
        /// Data table name (The default class name, if it is not, it needs to be rewritten in the subclass)
        /// </summary>
        protected override string TableName => typeof(T).Name;

        protected override SqlResult GetSqlResult(Query query)
        {
            var compiler = new MySqlCompiler();

            SqlResult sqlResult = compiler.Compile(query);

            return sqlResult;
        }
    }
}
