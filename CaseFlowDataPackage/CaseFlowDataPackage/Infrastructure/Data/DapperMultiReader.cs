using Dapper;
using IMotionSoftware.CaseFlowDataPackage.Interfaces;

namespace IMotionSoftware.CaseFlowDataPackage.Infrastructure.Data
{
    /// <summary>
    /// The DapperMultiReader
    /// </summary>
    /// <seealso cref="IMotionSoftware.CaseFlowDataPackage.Interfaces.IMultiReader" />
    public sealed class DapperMultiReader : IMultiReader
    {
        /// <summary>
        /// The inner
        /// </summary>
        private readonly SqlMapper.GridReader _inner;
        /// <summary>
        /// Initializes a new instance of the <see cref="DapperMultiReader"/> class.
        /// </summary>
        /// <param name="inner">The inner.</param>
        public DapperMultiReader(SqlMapper.GridReader inner) => _inner = inner;

        /// <summary>
        /// Reads the single asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> ReadSingleAsync<T>() => await _inner.ReadSingleAsync<T>();
        /// <summary>
        /// Reads the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>
        /// The <see cref="Task{T}" />
        /// </returns>
        public async Task<IEnumerable<T>> ReadAsync<T>() => await _inner.ReadAsync<T>();

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or
        /// resetting unmanaged resources asynchronously.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous dispose operation.
        /// </returns>
        public ValueTask DisposeAsync()
        {
            _inner.Dispose();
            return ValueTask.CompletedTask;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() => _inner.Dispose();
    }
}