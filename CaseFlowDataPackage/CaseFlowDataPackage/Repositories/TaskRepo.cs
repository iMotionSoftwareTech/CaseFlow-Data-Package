using IMotionSoftware.CaseFlowDataPackage.DomainObjects;
using IMotionSoftware.CaseFlowDataPackage.DomainObjects.ParameterObjects;
using IMotionSoftware.CaseFlowDataPackage.Infrastructure.ParameterBuilders;
using IMotionSoftware.CaseFlowDataPackage.Infrastructure.StoredProcedures;
using IMotionSoftware.CaseFlowDataPackage.Interfaces;
using System.Data;

namespace IMotionSoftware.CaseFlowDataPackage.Repositories
{
    /// <summary>
    /// The TaskRepo
    /// </summary>
    /// <seealso cref="IMotionSoftware.CaseFlowDataPackage.Interfaces.ITaskRepo" />
    public class TaskRepo : ITaskRepo
    {
        /// <summary>
        /// The connection factory
        /// </summary>
        private readonly IDbConnectionFactory _connFactory;

        /// <summary>
        /// The SQL
        /// </summary>
        private readonly ISqlRunner _sqlRunner;

        public TaskRepo(IDbConnectionFactory connFactory, ISqlRunner sqlRunner)
        {
            _connFactory = connFactory;
            _sqlRunner = sqlRunner;
        }

        /// <summary>
        /// Creates the task asynchronous.
        /// </summary>
        /// <param name="createTaskParameter">The create task parameter.</param>
        /// <returns>
        /// The <see cref="Task{int}" />
        /// </returns>
        public async Task<int> CreateTaskAsync(CreateTaskParameter createTaskParameter)
        {
            using var conn = _connFactory.Create();
            conn.Open();

            var parameters = createTaskParameter.CreateTaskDynamicParameters();
            return await _sqlRunner.ExecuteAsync(conn, TaskStoredProcedures.CreateTaskSP, parameters);
        }

        /// <summary>
        /// Gets all statuses asynchronous.
        /// </summary>
        /// <returns>
        /// The <see cref="Task{T}" />
        /// </returns>
        public async Task<IEnumerable<StatusDto>> GetAllStatusesAsync()
        {
            using var conn = _connFactory.Create();
            conn.Open();

            return await _sqlRunner.QueryAsync<StatusDto>(conn, TaskStoredProcedures.GetAllStatusesSP, CommandType.StoredProcedure);
        }

        /// <summary>
        /// Gets all tasks asynchronous.
        /// </summary>
        /// <param name="getAllTasksParameter">The get all tasks parameter.</param>
        /// <returns>
        /// The <see cref="Task{T}" />
        /// </returns>
        public async Task<(int totalNoOfRecords, IEnumerable<TaskDto> tasks)> GetAllTasksAsync(GetAllTasksParameter getAllTasksParameter)
        {
            using var conn = _connFactory.Create();
            conn.Open();

            var parameters = getAllTasksParameter.GetAllTasksDynamicParameters();
            await using var multiResults = await _sqlRunner.QueryMultipleAsync(conn, TaskStoredProcedures.GetAllTasksSP, parameters);
            var totalNoOfRecords = await multiResults.ReadSingleAsync<int>();
            var tasks = await multiResults.ReadAsync<TaskDto>();

            return (totalNoOfRecords, tasks);
        }

        /// <summary>
        /// Gets the task with statuses by identifier asynchronous.
        /// </summary>
        /// <param name="taskId">The task identifier.</param>
        /// <returns></returns>
        public async Task<IEnumerable<TaskStatusDto>> GetTaskWithStatusesByIdAsync(int taskId)
        {
            using var conn = _connFactory.Create();
            conn.Open();

            var parameters = taskId.GetAllTaskWithStatusesByIdParameters();
            var result = await _sqlRunner.QueryAsync<TaskStatusDto>(conn, TaskStoredProcedures.GetTaskWithStatusesByIdSP, parameters);
            return result;
        }
    }
}