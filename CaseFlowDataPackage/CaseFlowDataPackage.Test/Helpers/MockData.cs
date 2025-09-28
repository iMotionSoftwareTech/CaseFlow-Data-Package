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
        /// The user forename
        /// </summary>
        public static string Username = "JSmith";

        /// <summary>
        /// The user exception
        /// </summary>
        public static string UserException = "User already exists";

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

        /// <summary>
        /// Gets the create user parameters.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{T}"/></returns>
        public static IEnumerable<CreateUserParameter> GetCreateUserParameters()
        {
            return new List<CreateUserParameter>
            {
                new CreateUserParameter
                {
                    CaseworkerRoleId = 25,
                    Forename = "John",
                    Surname = "Smith",
                    Email = "john.smith@example.com",
                    PasswordHash = new byte[]
                    {
                        0x6E, 0x4C, 0x20, 0x42, 0x21, 0x76, 0x21, 0x4B,
                        0x85, 0x63, 0x53, 0x99, 0x58, 0x74, 0x45, 0x7F
                    },
                    PasswordSalt = new byte[]
                    {
                        0x54, 0x72, 0x85, 0x63, 0x53, 0x99, 0x58, 0x74,
                        0x45, 0x7F
                    },
                    CreatedDateTime = DateTime.Today
                },
                new CreateUserParameter
                {
                    CaseworkerRoleId = 25,
                    Forename = "Bob",
                    Surname = "Wilkins",
                    Email = "bob.wilkins@hmcts.org.uk",
                    PasswordHash = new byte[] 
                    {
                        0x19, 0x5E, 0xF5, 0x0E, 0xEF, 0x7C, 0x03, 0xF2,
                        0x43, 0x89, 0xB6, 0x8B, 0x74, 0xD3, 0xAB, 0x7D,
                        0x19, 0xD7, 0xD5, 0x5B, 0xA6, 0x71, 0xB5, 0x0C,
                        0x4A, 0x33, 0x2F, 0xE1, 0x0F, 0xFB, 0x7E, 0x85,
                        0x04, 0x93, 0x06, 0x85, 0x75, 0xC0, 0xF5, 0x15,
                        0xDA, 0x30, 0x2B, 0xAB, 0xE5, 0x1A, 0x13, 0x4F,
                        0x4C, 0x86, 0x2D, 0x2F, 0x00, 0x25, 0xB9, 0x1B,
                        0x35, 0x45, 0xA9, 0x93, 0x42, 0x0B, 0xCD, 0xB6
                    },
                    PasswordSalt = new byte[]
                    {
                        0x31, 0xB2, 0xEC, 0xFA, 0xC6, 0x03, 0xE1, 0x47,
                        0xAF, 0x72, 0x10, 0x4B, 0xD4, 0x68, 0x84, 0x65
                    },
                    CreatedDateTime = DateTime.Today
                },
            };
        }
    }
}