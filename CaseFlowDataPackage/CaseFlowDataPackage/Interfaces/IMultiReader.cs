namespace IMotionSoftware.CaseFlowDataPackage.Interfaces
{
    /// <summary>
    /// The IMultiReader
    /// </summary>
    /// <seealso cref="System.IAsyncDisposable" />
    /// <seealso cref="System.IDisposable" />
    public interface IMultiReader : IAsyncDisposable, IDisposable
    {
        /// <summary>
        /// Reads the single asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>The <see cref="Task{T}"/>/returns>
        Task<T> ReadSingleAsync<T>();

        /// <summary>
        /// Reads the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>The <see cref="Task{T}"/></returns>
        Task<IEnumerable<T>> ReadAsync<T>();
    }
}