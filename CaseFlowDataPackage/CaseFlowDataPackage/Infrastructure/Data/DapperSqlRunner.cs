using Dapper;
using IMotionSoftware.CaseFlowDataPackage.Interfaces;
using System.Data;
using static Dapper.SqlMapper;

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
        public async Task<int> ExecuteAsync(IDbConnection connection, string sql, object? param = null,
                                  IDbTransaction? tx = null, int? timeout = null, CommandType? ct = null)
                        => await connection.ExecuteAsync(sql, param, tx, timeout, ct);

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
        public async Task<T> QuerySingleAsync<T>(IDbConnection connection, string sql, object? param = null,
                                           IDbTransaction? tx = null, int? timeout = null, CommandType? ct = null)
                        => await connection.QuerySingleAsync<T>(sql, param, tx, timeout);

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
        public async Task<T?> QuerySingleOrDefaultAsync<T>(IDbConnection connection, string sql, object? param = null,
                                IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null)
                        => await connection.QuerySingleOrDefaultAsync<T>(sql, param, transaction, commandTimeout, commandType);

        /// <summary>
        /// Queries the multiple asynchronous.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="sql">The SQL.</param>
        /// <param name="param">The parameter.</param>
        /// <param name="transaction">The transaction.</param>
        /// <param name="commandTimeout">The command timeout.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns></returns>
        public async Task<IMultiReader> QueryMultipleAsync(IDbConnection connection, string sql, object? param = null,
                                IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var grid = await connection.QueryMultipleAsync(sql, param, transaction, commandTimeout, commandType);
            return new DapperMultiReader(grid);
        }

        /// <summary>
        /// Queries the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection">The connection.</param>
        /// <param name="sql">The SQL.</param>
        /// <param name="param">The parameter.</param>
        /// <param name="transaction">The transaction.</param>
        /// <param name="commandTimeout">The command timeout.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <returns>
        /// THe <see cref="Task{T}" />
        /// </returns>
        public async Task<IEnumerable<T>> QueryAsync<T>(IDbConnection connection, string sql, object? param = null,
                                IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null)
                    => await connection.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
    }
}