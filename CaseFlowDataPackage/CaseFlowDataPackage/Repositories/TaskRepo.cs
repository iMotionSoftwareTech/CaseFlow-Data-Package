using Dapper;
using IMotionSoftware.CaseFlowDataPackage.DomainObjects;
using IMotionSoftware.CaseFlowDataPackage.DomainObjects.ParameterObjects;
using IMotionSoftware.CaseFlowDataPackage.Infrastructure.ParameterBuilders;
using IMotionSoftware.CaseFlowDataPackage.Infrastructure.ResultBuilders;
using IMotionSoftware.CaseFlowDataPackage.Infrastructure.StoredProcedures;
using IMotionSoftware.CaseFlowDataPackage.Interfaces;
using System.Data;
using System.Threading.Tasks;

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
        /// The <see cref="Task{T}" />
        /// </returns>
        public async Task<NewTaskResult> CreateTaskAsync(CreateTaskParameter createTaskParameter)
        {
            using var conn = _connFactory.Create();
            conn.Open();

            var parameters = createTaskParameter.CreateTaskDynamicParameters();
            return await _sqlRunner.ExecuteWithOutputAsync(conn, TaskStoredProcedures.CreateTaskSP, parameters, 
                output => output.ToNewTaskResult(), ct: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Gets all statuses asynchronous.
        /// </summary>
        /// <returns>
        /// The <see cref="Task{T}" />
        /// </returns>
        public async Task<IEnumerable<StatusResult>> GetAllStatusesAsync()
        {
            using var conn = _connFactory.Create();
            conn.Open();

            return await _sqlRunner.QueryAsync<StatusResult>(conn, TaskStoredProcedures.GetAllStatusesSP, CommandType.StoredProcedure);
        }

        /// <summary>
        /// Gets all tasks asynchronous.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>
        /// The <see cref="Task{T}" />
        /// </returns>
        public async Task<(int totalNoOfRecords, IEnumerable<TaskResult> tasks)> GetAllTasksAsync(int pageNumber, int pageSize)
        {
            using var conn = _connFactory.Create();
            conn.Open();

            var parameters = pageNumber.GetAllTasksDynamicParameters(pageSize);
            await using var multiResults = await _sqlRunner.QueryMultipleAsync(conn, TaskStoredProcedures.GetAllTasksSP, parameters);
            var totalNoOfRecords = await multiResults.ReadSingleAsync<int>();
            var tasks = await multiResults.ReadAsync<TaskResult>();

            return (totalNoOfRecords, tasks);
        }

        /// <summary>
        /// Gets the task with statuses by identifier asynchronous.
        /// </summary>
        /// <param name="taskId">The task identifier.</param>
        /// <returns></returns>
        public async Task<IEnumerable<TaskStatusResult>> GetTaskWithStatusesByIdAsync(int taskId)
        {
            using var conn = _connFactory.Create();
            conn.Open();

            var parameters = taskId.GetAllTaskWithStatusesByIdParameters();
            var result = await _sqlRunner.QueryAsync<TaskStatusResult>(conn, TaskStoredProcedures.GetTaskWithStatusesByIdSP, parameters);
            return result;
        }

        /// <summary>
        /// Logs the task status asynchronous.
        /// </summary>
        /// <param name="logTaskStatusParameter">The log task status parameter.</param>
        /// <returns>
        /// The <see cref="Task{T}" />
        /// </returns>
        public async Task<TaskUpdateResult> LogTaskStatusAsync(LogTaskStatusParameter logTaskStatusParameter)
        {
            using var conn = _connFactory.Create();
            conn.Open();

            var parameters = logTaskStatusParameter.GetLogTaskStatusParameters();
             return await _sqlRunner.ExecuteWithOutputAsync(conn, TaskStoredProcedures.LogTaskStatusSP, parameters,
                output => output.ToLogTaskStatusResult(), ct: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Logs the task statuses asynchronous.
        /// </summary>
        /// <param name="logTaskStatusParameters">The log task status parameters.</param>
        /// <returns>
        /// The <see cref="Task{T}" />
        /// </returns>
        public async Task<BulkTaskUpdateResult> LogTaskStatusesAsync(IEnumerable<LogTaskStatusParameter> logTaskStatusParameters)
        {
            using var conn = _connFactory.Create();
            conn.Open();

            var tvp = logTaskStatusParameters.ToTaskUpdateDataTable().AsTableValuedParameter("caseFlow.TaskUpdateList");
            var parameters = tvp.ToLogTaskStatusParameters(logTaskStatusParameters.First().CaseworkerId);

            return await _sqlRunner.ExecuteWithOutputAsync(conn, TaskStoredProcedures.LogTaskStatusesSP, parameters,
                output => output.ToBulkTaskUpdateResult(), ct: CommandType.StoredProcedure);
           
        }
    }
}