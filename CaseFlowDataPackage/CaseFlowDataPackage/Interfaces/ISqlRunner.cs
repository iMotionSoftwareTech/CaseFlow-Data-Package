using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMotionSoftware.CaseFlowDataPackage.Interfaces
{
    /// <summary>
    /// The ISqlRunner
    /// </summary>
    public interface ISqlRunner
    {
        /// <summary>
        /// Executes the asynchronous.
        /// </summary>
        /// <param name="conn">The connection.</param>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The <see cref="Task{int}"/></returns>
        Task<int> ExecuteAsync(IDbConnection connection, string sql, object? param = null,
                                  IDbTransaction? tx = null, int? timeout = null, CommandType? ct = null);

        /// <summary>
        /// Queries the single asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conn">The connection.</param>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The <see cref="Task{T}"</returns>
        Task<T> QuerySingleAsync<T>(IDbConnection connection, string sql, object? param = null,
                                           IDbTransaction? tx = null, int? timeout = null, CommandType? ct = null);

        /// <summary>
        /// Queries the single or default asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection">The connection.</param>
        /// <param name="sql">The SQL.</param>
        /// <param name="param">The parameter.</param>
        /// <param name="transaction">The transaction.</param>
        /// <param name="commandTimeout">The command timeout.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>The <see cref="Task{T}"</returns>
        Task<T?> QuerySingleOrDefaultAsync<T>(IDbConnection connection, string sql, object? param = null,
                                IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null);
    }
}
