using Dapper;
using IMotionSoftware.CaseFlowDataPackage.DomainObjects.ParameterObjects;
using System.Data;
using System.Runtime.CompilerServices;

namespace IMotionSoftware.CaseFlowDataPackage.Infrastructure.ParameterBuilders
{
    /// <summary>
    /// The TaskParameterBuilder
    /// </summary>
    public static class TaskParameterBuilder
    {
        /// <summary>
        /// Creates the task dynamic parameters.
        /// </summary>
        /// <param name="createTaskParameter">The create task parameter.</param>
        /// <returns>The <see cref="DynamicParameters"/></returns>
        public static DynamicParameters CreateTaskDynamicParameters(this CreateTaskParameter createTaskParameter)
        {
            var parameters = new DynamicParameters();
            parameters.Add("caseworkerId", createTaskParameter.CaseworkerId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("title", createTaskParameter.Title, DbType.String, ParameterDirection.Input);
            parameters.Add("description", createTaskParameter.Description, DbType.String, ParameterDirection.Input);
            parameters.Add("dueDateTime", createTaskParameter.DueDateTime, DbType.DateTime2, ParameterDirection.Input);

            return parameters;
        }

        /// <summary>
        /// Gets all tasks dynamic parameters.
        /// </summary>
        /// <param name="getAllTasksParameter">The get all tasks parameter.</param>
        /// <returns>The <see cref="DynamicParameters"/></returns>
        public static DynamicParameters GetAllTasksDynamicParameters(this GetAllTasksParameter getAllTasksParameter)
        {
            var parameters = new DynamicParameters();
            parameters.Add("pageNumber", getAllTasksParameter.PageNumber, DbType.Int32, ParameterDirection.Input);
            parameters.Add("pageSize", getAllTasksParameter.PageSize, DbType.Int32, ParameterDirection.Input);
            return parameters;
        }

        /// <summary>
        /// Gets all task with statuses by identifier parameters.
        /// </summary>
        /// <param name="taskId">The task identifier.</param>
        /// <returns></returns>
        public static DynamicParameters GetAllTaskWithStatusesByIdParameters(this int taskId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("taskId", taskId, DbType.Int32, ParameterDirection.Input);

            return parameters;
        }
    }
}