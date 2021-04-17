using DapperRepo.Core.Data;
using DapperRepo.Core.Domain;
using SqlKata;

namespace DapperRepo.Data
{
    public abstract class RepositoryDataTypeBase
    {
        protected IDbSession DbSession => SessionFactory.CreateSession(DataType, ConnStrKey);

        /// <summary>
        /// Of the current database connection string key
        /// </summary>
        protected abstract string ConnStrKey { get; }

        /// <summary>
        /// Database type（MSSQL,MYSQL...）
        /// </summary>
        protected abstract DatabaseType DataType { get; }

        /// <summary>
        /// Data table name (The default class name, if it is not, it needs to be rewritten in the subclass)
        /// </summary>
        protected abstract string TableName { get; }

        protected abstract SqlResult GetSqlResult(Query query);
    }
}
