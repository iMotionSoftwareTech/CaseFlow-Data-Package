using IMotionSoftware.CaseFlowDataPackage.DomainObjects;
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
        public static string TaskException = "Task already exists.";

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
                new CreateRoleParameter
                {
                    RoleName = "Test Role 3",
                    Description = "This is a third test role"
                },
                new CreateRoleParameter
                {
                    RoleName = "Test Role 4",
                    Description = "This is a fourth test role"
                },
                new CreateRoleParameter
                {
                    RoleName = "Test Role 5",
                    Description = "This is a fifth test role"
                },
                new CreateRoleParameter
                {
                    RoleName = "Test Role 6",
                    Description = "This is a sixth test role"
                },
                new CreateRoleParameter
                {
                    RoleName = "Test Role 7",
                    Description = "This is a seventh test role"
                },
                new CreateRoleParameter
                {
                    RoleName = "Test Role 8",
                    Description = "This is a eighth test role"
                },
                new CreateRoleParameter
                {
                    RoleName = "Test Role 9",
                    Description = "This is a nineth test role"
                },
                new CreateRoleParameter
                {
                    RoleName = "Test Role 10",
                    Description = "This is a tenth test role"
                },
                new CreateRoleParameter
                {
                    RoleName = "Test Role 11",
                    Description = "This is a eleventh test role"
                }
            };
        }

        /// <summary>
        /// Gets the create task parameters.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{T}"/></returns>
        public static IEnumerable<CreateTaskParameter> GetCreateTaskParameters(int caseworkerId)
        {
            return new List<CreateTaskParameter>
            {
                new CreateTaskParameter
                {
                    CaseworkerId = caseworkerId,
                    Title = "Theft",
                    Description = "A convenience store theft",
                    DueDateTime = DateTime.Today.AddMonths(9)
                },
                new CreateTaskParameter
                {
                    CaseworkerId = caseworkerId,
                    Title = "Assualt and Battery",
                    Description = "Assault on shop owner",
                    DueDateTime = DateTime.Today.AddMonths(12)
                },
                new CreateTaskParameter
                {
                    CaseworkerId = caseworkerId,
                    Title = "Wreckless Driving",
                    Description = "Perpetrator causing environmental damage due to wreckless driving",
                    DueDateTime = DateTime.Today.AddMonths(15)
                },
                new CreateTaskParameter
                {
                    CaseworkerId = caseworkerId,
                    Title = "DUI",
                    Description = "Driver caught drunk driving",
                    DueDateTime = DateTime.Today.AddMonths(18)
                },
                new CreateTaskParameter
                {
                    CaseworkerId = caseworkerId,
                    Title = "Drug Posesssion",
                    Description = "Perpetrator caught with posession of Illegal substances",
                    DueDateTime = DateTime.Today.AddMonths(24)
                }
            };
        }

        /// <summary>
        /// Gets the create user parameters.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{T}"/></returns>
        public static IEnumerable<CreateUserParameter> GetCreateUserParameters(int caseworkerRoleId)
        {
            return new List<CreateUserParameter>
            {
                new CreateUserParameter
                {
                    CaseworkerRoleId = caseworkerRoleId,
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
                    CaseworkerRoleId = caseworkerRoleId,
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
                new CreateUserParameter
                {
                    CaseworkerRoleId = caseworkerRoleId,
                    Forename = "Wilma",
                    Surname = "Burke",
                    Email = "wilma.burke@hmcts.org.uk",
                    PasswordHash = new byte[]
                    {
                        0xE2, 0xE8, 0x69, 0x4B, 0x5D, 0x94, 0xDD, 0x5A,
                        0x0B, 0x9C, 0x1B, 0xD7, 0x45, 0x80, 0xED, 0x39,
                        0x4A, 0xCD, 0x8D, 0xF9, 0x23, 0x28, 0x4D, 0x56,
                        0x98, 0xBC, 0x63, 0x26, 0xB4, 0x6B, 0x6E, 0x41,
                        0xFA, 0xA9, 0x44, 0x51, 0xD6, 0xD2, 0xAD, 0xD3,
                        0x33, 0x66, 0xCF, 0xD0, 0x7D, 0x1A, 0x67, 0x0C,
                        0x07, 0xD6, 0x1D, 0x3D, 0xA9, 0x27, 0x75, 0x51,
                        0xF1, 0x12, 0x9A, 0x2B, 0x38, 0xBF, 0x5D, 0x0D
                    },
                    PasswordSalt = new byte[]
                    {
                        0x06, 0x71, 0xB7, 0x0C, 0x4C, 0xFC, 0xFF, 0x51,
                        0x73, 0xFD, 0xEC, 0xD7, 0xFA, 0x59, 0x58, 0xBE
                    },
                    CreatedDateTime = DateTime.Today
                },
                new CreateUserParameter
                {
                    CaseworkerRoleId = caseworkerRoleId,
                    Forename = "Dan",
                    Surname = "Smithey",
                    Email = "dan.smithey@hmcts.org.uk",
                    PasswordHash = new byte[]
                    {
                        0xA8, 0x15, 0xEB, 0x7C, 0x0B, 0xC4, 0x78, 0x19,
                        0x94, 0xC0, 0x8C, 0xA4, 0x4C, 0x01, 0x54, 0x3E,
                        0xB2, 0xCC, 0x54, 0x90, 0xEB, 0xD6, 0x52, 0x72,
                        0xF4, 0xFB, 0x3F, 0x69, 0x77, 0xE4, 0xCC, 0x50,
                        0x5C, 0x82, 0xB9, 0x9C, 0x5D, 0xF8, 0xED, 0xD2,
                        0xE4, 0xB0, 0xE2, 0x86, 0xA6, 0x83, 0x77, 0x19,
                        0xA7, 0xE7, 0x42, 0x9F, 0x89, 0xBE, 0x1C, 0x09,
                        0xE8, 0x9C, 0x2F, 0xDE, 0x6B, 0x6F, 0x7E, 0x14
                    },
                    PasswordSalt = new byte[]
                    {
                        0x0B, 0xFE, 0xFB, 0xA4, 0x08, 0x9C, 0x37, 0x21,
                        0xC3, 0x33, 0xD4, 0x65, 0xE8, 0x9F, 0x23, 0x65
                    },
                    CreatedDateTime = DateTime.Today
                },
                new CreateUserParameter
                {
                    CaseworkerRoleId = caseworkerRoleId,
                    Forename = "Francesco",
                    Surname = "Almeida",
                    Email = "francesco.almeida@hmcts.org.uk",
                    PasswordHash = new byte[]
                    {
                        0x96, 0xB5, 0x07, 0xF3, 0x54, 0x2E, 0x6F, 0xEE,
                        0x49, 0x96, 0xD9, 0x13, 0x58, 0x35, 0xB4, 0xEB,
                        0xCE, 0xD2, 0xAC, 0xEC, 0xF7, 0x6B, 0xD1, 0x11,
                        0x4B, 0x90, 0x0D, 0x8C, 0x39, 0x54, 0x11, 0x08,
                        0x4B, 0xBA, 0x8B, 0xE2, 0xCC, 0x93, 0xCB, 0xFB,
                        0x9A, 0xE2, 0x6B, 0xC8, 0xE2, 0xF3, 0x33, 0x9A,
                        0x3E, 0x3E, 0xF9, 0x11, 0x51, 0x3B, 0xA0, 0x2F,
                        0x0D, 0xDA, 0xBD, 0xB8, 0xE7, 0xC7, 0x44, 0x5C
                    },
                    PasswordSalt = new byte[]
                    {
                        0x74, 0x9E, 0xDB, 0xC3, 0xC4, 0x70, 0x15, 0x8A,
                        0xF4, 0x56, 0x90, 0x93, 0x05, 0xCA, 0xCA, 0xD1
                    },
                    CreatedDateTime = DateTime.Today
                },
            };
        }

        /// <summary>
        /// Gets the caseworker roles.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{T}"/></returns>
        public static IEnumerable<CaseworkerRoleDto> GetCaseworkerRoles()
        {
            return new List<CaseworkerRoleDto>
            {
                new CaseworkerRoleDto
                {
                    Id = 1,
                    Name = "Case Auditor",
                    Description = string.Empty
                },
                new CaseworkerRoleDto
                {
                    Id = 2,
                    Name = "Legal Officer",
                    Description = "The officer running the case"
                },
                new CaseworkerRoleDto
                {
                    Id = 3,
                    Name = "Case Manager",
                    Description = "The manager of the case"
                }
            }.ToArray();
        }

        /// <summary>
        /// Gets the statuses.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{T}"/></returns>
        public static IEnumerable<StatusDto> GetStatuses()
        {
            return new List<StatusDto>()
            {
                new StatusDto
                {
                    Id = 1,
                    Title = "Case Created"
                },
                new StatusDto
                {
                    Id = 2,
                    Title = "Under Review"
                },
                new StatusDto
                {
                    Id = 3,
                    Title = "Hearing Scheduled"
                },
                new StatusDto
                {
                    Id = 4,
                    Title = "Hearing Held"
                },
                new StatusDto
                {
                    Id = 5,
                    Title = "Awaiting Judgement"
                },
                new StatusDto
                {
                    Id = 6,
                    Title = "Order Made"
                },
                new StatusDto
                {
                    Id = 7,
                    Title = "Case Stayed"
                },
                new StatusDto
                {
                    Id = 8,
                    Title = "Case Discontinued"
                },
                new StatusDto
                {
                    Id = 9,
                    Title = "Concluded"
                },
                new StatusDto
                {
                    Id = 10,
                    Title = "Transferred"
                },
            };            
        }

        /// <summary>
        /// Gets the tasks.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{T}"/></returns>
        public static IEnumerable<TaskDto> GetTasks()
        {
            return new List<TaskDto>
            {
                new TaskDto
                {
                    Id = 1,
                    Title = "Assault and Battery",
                    Description = "Assult and battery case",
                    DueDate = DateTime.Now.AddYears(1)
                },
                new TaskDto
                {
                    Id = 2,
                    Title = "Theft",
                    Description = "Theft at supermarket case",
                    DueDate = DateTime.Now.AddYears(1)
                },
                new TaskDto
                {
                    Id = 3,
                    Title = "Kidnapping",
                    Description = "Kidnapping case",
                    DueDate = DateTime.Now.AddYears(1)
                },
            };
        }

        /// <summary>
        /// Gets the task statuses.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{T}"/></returns>
        public static IEnumerable<TaskStatusDto> GetTaskStatuses()
        {
            return new List<TaskStatusDto>
            {
                new TaskStatusDto
                {
                    Id = 1,
                    TaskId = 1,
                    StatusId = 1,
                    Status = "Case Created",
                    CaseWorker = "Jayne Smith",
                    LogDateTime = DateTime.Now,
                    Notes = "Case registered for assualt and battery"
                },
                new TaskStatusDto
                {
                    Id = 2,
                    TaskId = 1,
                    StatusId = 2,
                    Status = "Under Review",
                    CaseWorker = "Jayne Smith",
                    LogDateTime = DateTime.Now.AddDays(2),
                    Notes = "Case is currently under review"
                },
                new TaskStatusDto
                {
                    Id = 3,
                    TaskId = 1,
                    StatusId = 3,
                    Status = "Hearing Scheduled",
                    CaseWorker = "Bob Wilkinson",
                    LogDateTime = DateTime.Now.AddDays(5),
                    Notes = "Hearing scheduled for the case"
                },
            };
        }

        /// <summary>
        /// Gets all tasks parameter.
        /// </summary>
        /// <returns>The <see cref="GetAllTasksParameter"/></returns>
        public static GetAllTasksParameter GetAllTasksParameter()
        {
            return new GetAllTasksParameter
            {
                PageNumber = 1,
                PageSize = 10
            };
        }

        /// <summary>
        /// Gets the users detail.
        /// </summary>
        /// <returns>THe <see cref="UserDetailDto"/></returns>
        public static UserDetailDto GetUsersDetail()
        {
            return new UserDetailDto
            {
                CaseworkerId = 1,
                CaseworkerRoleId = 25,
                Role = "Caseworker Auditor",
                Forename = "John",
                Surname = "Smith",
                Email = "john.smith@example.com",
                Username = "JSmith",
                PasswordAttempt = 1,
                IsLocked = false
            };
        }
    }                  
}