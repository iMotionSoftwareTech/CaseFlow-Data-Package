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
        public static string GetRole = "SELECT TOP 1 * FROM caseFlow.CaseworkerRole WHERE Name = 'Test Role'";

        /// <summary>
        /// The delete role
        /// </summary>
        public static string DeleteRole = "DELETE FROM caseFlow.CaseworkerRole WHERE Name = 'Test Role'";


        /// <summary>
        /// The get task
        /// </summary>
        public static string GetTask = "SELECT TOP 1 * FROM caseFlow.Task WHERE Title = 'Theft'";

        /// <summary>
        /// The delete task status
        /// </summary>
        public static string DeleteTaskStatus = "DELETE FROM caseFlow.TaskStatus";

        /// <summary>
        /// The delete task
        /// </summary>
        public static string DeleteTask = "DELETE FROM caseFlow.Task";
    }
}