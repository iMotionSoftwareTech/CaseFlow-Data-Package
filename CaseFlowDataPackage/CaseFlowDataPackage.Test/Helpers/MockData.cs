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
    }
}
