using IMotionSoftware.CaseFlowDataPackage.DomainObjects;
using IMotionSoftware.CaseFlowDataPackage.DomainObjects.ParameterObjects;

namespace IMotionSoftware.CaseFlowDataPackage.Interfaces
{
    /// <summary>
    /// The ITaskRepo
    /// </summary>
    public interface ITaskRepo
    {
        /// <summary>
        /// Creates the task asynchronous.
        /// </summary>
        /// <param name="createTaskParameter">The create task parameter.</param>
        /// <returns>The <see cref="Task{int}"/></returns>
        Task<int> CreateTaskAsync(CreateTaskParameter createTaskParameter);

        /// <summary>
        /// Gets all statuses asynchronous.
        /// </summary>
        /// <returns>The <see cref="Task{T}"/></returns>
        Task<IEnumerable<StatusDto>> GetAllStatusesAsync();

        /// <summary>
        /// Gets all tasks asynchronous.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>
        /// The <see cref="Task{T}" />
        /// </returns>
        Task<(int totalNoOfRecords, IEnumerable<TaskDto> tasks)> GetAllTasksAsync(int pageNumber, int pageSize);

        /// <summary>
        /// Gets the task with statuses by identifier.
        /// </summary>
        /// <param name="taskId">The task identifier.</param>
        /// <returns>The <see cref="Task{T}"/></returns>
        Task<IEnumerable<TaskStatusDto>> GetTaskWithStatusesByIdAsync(int taskId);

        /// <summary>
        /// Logs the task status asynchronous.
        /// </summary>
        /// <param name="logTaskStatusParameter">The log task status parameter.</param>
        /// <returns>The <see cref="Task{int}"/></returns>
        Task<int> LogTaskStatusAsync(LogTaskStatusParameter logTaskStatusParameter);

        /// <summary>
        /// Logs the task statuses asynchronous.
        /// </summary>
        /// <param name="logTaskStatusParameters">The log task status parameters.</param>
        /// <returns>The <see cref="Task{int}"/></returns>
        Task<int> LogTaskStatusesAsync(IEnumerable<LogTaskStatusParameter> logTaskStatusParameters);
    }
}