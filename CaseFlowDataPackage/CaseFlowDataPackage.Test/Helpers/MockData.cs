using IMotionSoftware.CaseFlowDataPackage.DomainObjects.ParameterObjects;

namespace CaseFlowDataPackage.Test.Helpers
{
    /// <summary>
    /// The MockData
    /// </summary>
    public static class MockData
    {
        /// <summary>
        /// The role name
        /// </summary>
        public static string RoleName = "Test Role";

        /// <summary>
        /// The role exception
        /// </summary>
        public static string RoleException = "Role already exists";

        /// <summary>
        /// The task name
        /// </summary>
        public static string TaskTitle = "Theft";

        /// <summary>
        /// The task exception
        /// </summary>
        public static string TaskException = "Task already exists";

        /// <summary>
        /// Gets the create role parameters.
        /// </summary>
        /// <returns>THe <see cref="IEnumerable{T}"/></returns>
        public static IEnumerable<CreateRoleParameter> GetCreateRoleParameters()
        {
            return new List<CreateRoleParameter> 
            {
                new CreateRoleParameter
                {
                    RoleName = "Test Role",
                    Description = "This is a test role"
                },
                new CreateRoleParameter
                {
                    RoleName = "Test Role 2",
                    Description = "This is a second test role"
                },
            };
        }

        /// <summary>
        /// Gets the create task parameters.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{T}"/></returns>
        public static IEnumerable<CreateTaskParameter> GetCreateTaskParameters()
        {
            return new List<CreateTaskParameter>
            {
                new CreateTaskParameter
                {
                    CaseworkerId = 1002,
                    Title = "Theft",
                    Description = "A convenience store theft",
                    DueDateTime = DateTime.Today.AddMonths(9)
                },
                new CreateTaskParameter
                {
                    CaseworkerId = 1002,
                    Title = "Assualt and Batteru",
                    Description = "Assault on shop owner",
                    DueDateTime = DateTime.Today.AddMonths(12)
                }
            };
        }
    }
}