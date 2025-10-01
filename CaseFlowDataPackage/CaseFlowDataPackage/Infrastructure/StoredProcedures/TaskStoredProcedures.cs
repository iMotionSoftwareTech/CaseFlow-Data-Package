namespace IMotionSoftware.CaseFlowDataPackage.Infrastructure.StoredProcedures
{
    /// <summary>
    /// The TaskStoredProcedures
    /// </summary>
    public static class TaskStoredProcedures
    {
        /// <summary>
        /// The create task sp
        /// </summary>
        public static string CreateTaskSP = "caseFlow.CreateTask";

        /// <summary>
        /// The get all statuses sp
        /// </summary>
        public static string GetAllStatusesSP = "caseFlow.GetAllStatuses";

        /// <summary>
        /// The get all tasks sp
        /// </summary>
        public static string GetAllTasksSP = "caseFlow.GetAllTasks";

        /// <summary>
        /// The get task with statuses by identifier sp
        /// </summary>
        public static string GetTaskWithStatusesByIdSP = "caseFlow.GetTaskWithStatusesById";
    }
}