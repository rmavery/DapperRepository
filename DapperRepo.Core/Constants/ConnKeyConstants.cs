namespace DapperRepo.Core.Constants
{
    /// <summary>
    /// </summary>
    public static class ConnKeyConstants
    {
        #region MSSQL

        public const string Mssql = "Mssql"; // Identifies the currently used database as >> mssql

        public const string MssqlMasterKey = "MssqlMasterKey";

        // More than one can be defined according to specific projects
        // public const string MssqlSecondConnKey = "MssqlSecondConnKey";

        #endregion

        #region MYSQL

        public const string Mysql = "Mysql";

        public const string MysqlMasterKey = "MysqlMasterKey";

        #endregion

        #region ORACLE

        public const string Oracle = "Oracle";

        public const string OracleMasterKey = "OracleMasterKey";

        #endregion
    }
}
