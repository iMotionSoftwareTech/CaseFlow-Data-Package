using Dapper;
using IMotionSoftware.CaseFlowDataPackage.DomainObjects.ParameterObjects;
using System.Data;

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
        /// <returns>The <see cref="DynamicParameters"/></returns>
        public static DynamicParameters GetAllTaskWithStatusesByIdParameters(this int taskId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("taskId", taskId, DbType.Int32, ParameterDirection.Input);

            return parameters;
        }

        /// <summary>
        /// Gets the log task status parameters.
        /// </summary>
        /// <param name="logTaskStatusParameter">The log task status parameter.</param>
        /// <returns>The <see cref="DynamicParameters"/></returns>
        public static DynamicParameters GetLogTaskStatusParameters(this LogTaskStatusParameter logTaskStatusParameter)
        {
            var parameters = new DynamicParameters();
            parameters.Add("taskId", logTaskStatusParameter.TaskId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("statusId", logTaskStatusParameter.StatusId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("caseworkerId", logTaskStatusParameter.CaseworkerId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("notes", logTaskStatusParameter.Notes, DbType.String, ParameterDirection.Input);
            parameters.Add("logDateTime", logTaskStatusParameter.LogDateTime, DbType.DateTime2, ParameterDirection.Input);

            return parameters;
        }

        /// <summary>
        /// Tasks the update list TVP.
        /// </summary>
        /// <param name="rows">The rows.</param>
        /// <returns>The <see cref="DataTable"/></returns>
        public static DataTable ToTaskUpdateDataTable(this IEnumerable<LogTaskStatusParameter> rows)
        {
            var dt = new DataTable();
            dt.Columns.Add("TaskId", typeof(int));
            dt.Columns.Add("StatusId", typeof(int));
            dt.Columns.Add("Notes", typeof(string));
            dt.Columns.Add("LogDateTime", typeof(DateTime));

            foreach (var r in rows)
                dt.Rows.Add(r.TaskId, r.StatusId, r.Notes, r.LogDateTime);

            return dt;
        }
    }
}