using Dapper;
using IMotionSoftware.CaseFlowDataPackage.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMotionSoftware.CaseFlowDataPackage.Infrastructure.Data
{
    /// <summary>
    /// The DapperSqlRunner
    /// </summary>
    /// <seealso cref="IMotionSoftware.CaseFlowDataPackage.Interfaces.ISqlRunner" />
    public sealed class DapperSqlRunner : ISqlRunner
    {
        /// <summary>
        /// Executes the asynchronous.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="tx"></param>
        /// <param name="timeout"></param>
        /// <param name="ct"></param>
        /// <returns>
        /// The <see cref="Task{int}" />
        /// </returns>
        public Task<int> ExecuteAsync(IDbConnection connection, string sql, object? param = null,
                                  IDbTransaction? tx = null, int? timeout = null, CommandType? ct = null)
                        => connection.ExecuteAsync(sql, param, tx, timeout, ct);

        /// <summary>
        /// Queries the single asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection">The connection.</param>
        /// <param name="sql">The SQL.</param>
        /// <param name="param">The parameter.</param>
        /// <param name="tx">The tx.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="ct">The ct.</param>
        /// <returns></returns>
        public Task<T> QuerySingleAsync<T>(IDbConnection connection, string sql, object? param = null,
                                           IDbTransaction? tx = null, int? timeout = null, CommandType? ct = null)
                        => connection.QuerySingleAsync<T>(sql, param, tx, timeout, ct);

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
        /// <returns></returns>
        public Task<T?> QuerySingleOrDefaultAsync<T>(IDbConnection connection, string sql, object? param = null,
                                IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null)
                        => connection.QuerySingleOrDefaultAsync<T>(sql, param, transaction, commandTimeout, commandType);
    }
}