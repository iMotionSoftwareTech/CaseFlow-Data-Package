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
        /// <returns></returns>
        public static DynamicParameters CreateTaskDynamicParameters(this CreateTaskParameter createTaskParameter)
        {
            var parameters = new DynamicParameters();
            parameters.Add("caseWorkerId", createTaskParameter.CaseworkerId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("title", createTaskParameter.Title, DbType.String, ParameterDirection.Input);
            parameters.Add("description", createTaskParameter.Description, DbType.String, ParameterDirection.Input);
            parameters.Add("dueDateTime", createTaskParameter.DueDateTime, DbType.DateTime2, ParameterDirection.Input);

            return parameters;
        }
    }
}