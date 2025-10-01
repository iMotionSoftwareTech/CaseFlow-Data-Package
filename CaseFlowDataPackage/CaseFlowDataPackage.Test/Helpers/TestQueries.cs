namespace CaseFlowDataPackage.Test.Helpers
{
    /// <summary>
    /// The TestQueries
    /// </summary>
    public static class TestQueries
    {
        /// <summary>
        /// The get role
        /// </summary>
        public static string GetRole = "SELECT TOP 1 * FROM caseFlow.CaseworkerRole WHERE [Name] = @name";

        /// <summary>
        /// The delete role
        /// </summary>
        public static string DeleteRole = "DELETE FROM caseFlow.CaseworkerRole WHERE [Name] LIKE '%Test Role%'";

        /// <summary>
        /// The get task
        /// </summary>
        public static string GetTask = "SELECT TOP 1 * FROM caseFlow.Task Order BY Id DESC";

        /// <summary>
        /// The delete task status
        /// </summary>
        public static string DeleteTaskStatus = "DELETE FROM caseFlow.TaskStatus";

        /// <summary>
        /// The delete task
        /// </summary>
        public static string DeleteTask = "DELETE FROM caseFlow.Task";

        /// <summary>
        /// The get task
        /// </summary>
        public static string GetUser = "SELECT TOP 1 * FROM caseFlow.[User] WHERE Username = 'JSmith'";

        /// <summary>
        /// The delete task status
        /// </summary>
        public static string DeleteCaseworker = "DELETE FROM caseFlow.Caseworker";

        /// <summary>
        /// The delete task
        /// </summary>
        public static string DeleteUser = "DELETE FROM caseFlow.[User]";

        /// <summary>
        /// The get caseworker
        /// </summary>
        public static string GetCaseworker = "SELECT TOP 1 * FROM caseFlow.Caseworker ORDER BY ID DESC";
    }
}