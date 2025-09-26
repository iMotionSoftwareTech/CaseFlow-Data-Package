using IMotionSoftware.CaseFlowDataPackage.DomainObjects.ParameterObjects;
using IMotionSoftware.CaseFlowDataPackage.Infrastructure.ParameterBuilders;
using IMotionSoftware.CaseFlowDataPackage.Infrastructure.StoredProcedures;
using IMotionSoftware.CaseFlowDataPackage.Interfaces;

namespace IMotionSoftware.CaseFlowDataPackage.Repositories
{
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
    }
}