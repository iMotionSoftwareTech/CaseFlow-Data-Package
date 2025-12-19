using Dapper;
using IMotionSoftware.CaseFlowDataPackage.DomainObjects;

namespace IMotionSoftware.CaseFlowDataPackage.Infrastructure.ResultBuilders
{
    /// <summary>
    /// The TaskResultBuilders
    /// </summary>
    public static class TaskResultBuilders
    {
        /// <summary>
        /// Converts to newtaskresult.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <returns>The <see cref="NewTaskResult"/></returns>
        public static NewTaskResult ToNewTaskResult(this DynamicParameters p) 
        {
            return new NewTaskResult
            {
                Success = p.Get<bool>("success"),
                TaskId = p.Get<int>("taskId"),
                ErrorMessage = p.Get<string>("errorMessage")
            };
        }

        /// <summary>
        /// Converts to logtaskstatusresult.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <returns>The <see cref="TaskUpdateResult"/></returns>
        public static TaskUpdateResult ToLogTaskStatusResult(this DynamicParameters p)
        {
            return new TaskUpdateResult
            {
                Success = p.Get<bool>("success"),
                TaskStatusId = p.Get<int>("taskStatusId"),
                ErrorMessage = p.Get<string>("errorMessage")
            };
        }

        /// <summary>
        /// Converts to bulktaskupdateresult.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <returns>The <see cref="BulkTaskUpdateResult"/></returns>
        public static BulkTaskUpdateResult ToBulkTaskUpdateResult(this DynamicParameters p) 
        {
            return new BulkTaskUpdateResult
            {
                Success = p.Get<bool>("success"),
                InsertedCount = p.Get<int>("insertedCount"),
                ErrorMessage = p.Get<string>("errorMessage")
            };
        }
    }
}