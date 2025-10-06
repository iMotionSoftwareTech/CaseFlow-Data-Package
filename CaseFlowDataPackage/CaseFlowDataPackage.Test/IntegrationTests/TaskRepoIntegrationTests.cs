using CaseFlowDataPackage.Test.Helpers;
using Dapper;
using IMotionSoftware.CaseFlowDataPackage.DomainObjects;
using IMotionSoftware.CaseFlowDataPackage.DomainObjects.ParameterObjects;
using IMotionSoftware.CaseFlowDataPackage.Infrastructure.Data;
using IMotionSoftware.CaseFlowDataPackage.Interfaces;
using IMotionSoftware.CaseFlowDataPackage.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace IMotionSoftware.CaseFlowDataPackage.Test.IntegrationTests
{
    /// <summary>
    /// The TaskRepoIntegrationTests
    /// </summary>
    [TestClass]
    public class TaskRepoIntegrationTests
    {
        /// <summary>
        /// The factory
        /// </summary>
        private IDbConnectionFactory _factory = default!;

        /// <summary>
        /// The SQL
        /// </summary>
        private ISqlRunner _sql = default!;

        /// <summary>
        /// The connection string
        /// </summary>
        private static string connString = "";

        /// <summary>
        /// Classes the initialize.
        /// </summary>
        /// <param name="_">The .</param>
        /// <exception cref="InvalidOperationException">Connection string not found</exception>
        [ClassInitialize]
        public static void ClassInit(TestContext _)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddUserSecrets<TaskRepoIntegrationTests>(optional: true)
                .AddEnvironmentVariables()
                .Build();

            connString = config.GetConnectionString("Default") ?? throw new InvalidOperationException("Connection string not found");
        }

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            _factory = new InlineFactory(connString);
            _sql = new DapperSqlRunner();
        }

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            // Not calling Complete() means everything rolls back on Dispose
        }

        /// <summary>
        /// Creates the task asynchronous writes task returns1.
        /// </summary>
        [TestMethod, TestCategory("Integration")]
        public async Task CreateTaskAsync_WritesTask_Returns1()
        {
            using var conn = new SqlConnection(connString);
            await conn.OpenAsync();

            try
            {
                var repo = new TaskRepo(_factory, _sql);

                var roleRepo = new RoleRepo(_factory, _sql);
                var roleParam = MockData.GetCreateRoleParameters().ElementAt(3);
                await roleRepo.CreateRoleAsync(roleParam);
                var role = await conn.QuerySingleAsync<CaseworkerRoleDto>(TestQueries.GetRole, new { name = roleParam.RoleName });

                var userRepo = new UserRepo(_factory, _sql);
                await userRepo.CreateUserAsync(MockData.GetCreateUserParameters(role.Id).ElementAt(4));

                var caseworker = await conn.QuerySingleAsync<UserDto>(TestQueries.GetCaseworker);
                await repo.CreateTaskAsync(MockData.GetCreateTaskParameters(caseworker.Id).First());

                var inserted = await conn.QuerySingleAsync<TaskDto>(TestQueries.GetTask);
                Assert.AreEqual(MockData.TaskTitle, inserted.Title);
            }
            finally
            {
                // cleanup (when not using TransactionScope)
                await conn.ExecuteAsync(TestQueries.DeleteTaskStatus);
                await conn.ExecuteAsync(TestQueries.DeleteTask);
                await conn.ExecuteAsync(TestQueries.DeleteUser);
                await conn.ExecuteAsync(TestQueries.DeleteCaseworker);
                await conn.ExecuteAsync(TestQueries.DeleteRole);
            }

        }

        /// <summary>
        /// Creates the task asynchronous when duplicate task throws or fails gracefully.
        /// </summary>
        [TestMethod, TestCategory("Integration")]
        public async Task CreateTaskAsync_WhenDuplicateTask_ThrowsOrFailsGracefully()
        {
            using var conn = new SqlConnection(connString);
            await conn.OpenAsync();           

            try
            { 
                var roleRepo = new RoleRepo(_factory, _sql);
                var roleParam = MockData.GetCreateRoleParameters().ElementAt(4);
                await roleRepo.CreateRoleAsync(roleParam);
                var role = await conn.QuerySingleAsync<CaseworkerRoleDto>(TestQueries.GetRole, new { name = roleParam.RoleName });

                var userRepo = new UserRepo(_factory, _sql);
                await userRepo.CreateUserAsync(MockData.GetCreateUserParameters(role.Id).ElementAt(2));
                var caseworker = await conn.QuerySingleAsync<UserDto>(TestQueries.GetCaseworker);

                var param = MockData.GetCreateTaskParameters(caseworker.Id).ElementAt(1);
                var repo = new TaskRepo(_factory, _sql);

                // Seed first insert
                await repo.CreateTaskAsync(param);

                // Act again with same name
                // If your repo throws, assert throws; if it returns a code, assert that.
                var ex = await Assert.ThrowsExceptionAsync<SqlException>(() => repo.CreateTaskAsync(param));
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, MockData.TaskException);
            }
            finally
            {
                await conn.ExecuteAsync(TestQueries.DeleteTaskStatus);
                await conn.ExecuteAsync(TestQueries.DeleteTask);
                await conn.ExecuteAsync(TestQueries.DeleteUser);
                await conn.ExecuteAsync(TestQueries.DeleteCaseworker);
                await conn.ExecuteAsync(TestQueries.DeleteRole);
            }
        }

        /// <summary>
        /// Gets all statuses asynchronous returns statuses from database.
        /// </summary>
        [TestMethod, TestCategory("Integration")]
        public async Task GetAllStatusesAsync_ReturnsStatuses_FromDb()
        {
            using var conn = new SqlConnection(connString);
            await conn.OpenAsync();

            var repo = new TaskRepo(_factory, _sql);
            var result = await repo.GetAllStatusesAsync();

            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Gets all tasks asynchronous returns data from database.
        /// </summary>
        [TestMethod, TestCategory("Integration")]
        public async Task GetAllTasksAsync_ReturnsData_FromDb()
        {
            using var conn = new SqlConnection(connString);
            await conn.OpenAsync();

            try
            {
                var param = MockData.GetAllTasksParameter();
                var repo = new TaskRepo(_factory, _sql);

                var roleRepo = new RoleRepo(_factory, _sql);
                var roleParam = MockData.GetCreateRoleParameters().ElementAt(5);
                await roleRepo.CreateRoleAsync(roleParam);
                var role = await conn.QuerySingleAsync<CaseworkerRoleDto>(TestQueries.GetRole, new { name = roleParam.RoleName });

                var userRepo = new UserRepo(_factory, _sql);
                await userRepo.CreateUserAsync(MockData.GetCreateUserParameters(role.Id).ElementAt(3));

                var caseworker = await conn.QuerySingleAsync<UserDto>(TestQueries.GetCaseworker);
                await repo.CreateTaskAsync(MockData.GetCreateTaskParameters(caseworker.Id).ElementAt(2));
                await repo.CreateTaskAsync(MockData.GetCreateTaskParameters(caseworker.Id).ElementAt(3));

                var (total, tasks) = await repo.GetAllTasksAsync(param);

                Assert.IsTrue(total > 0);
                Assert.IsNotNull(tasks);
            }
            finally
            {
                // cleanup (when not using TransactionScope)
                await conn.ExecuteAsync(TestQueries.DeleteTaskStatus);
                await conn.ExecuteAsync(TestQueries.DeleteTask);
                await conn.ExecuteAsync(TestQueries.DeleteUser);
                await conn.ExecuteAsync(TestQueries.DeleteCaseworker);
                await conn.ExecuteAsync(TestQueries.DeleteRole);

            }
        }

        /// <summary>
        /// Gets the task with statuses by identifier asynchronous returns task statuses from database.
        /// </summary>
        [TestMethod, TestCategory("Integration")]
        public async Task GetTaskWithStatusesByIdAsync_ReturnsTaskStatuses_FromDb()
        {
            using var conn = new SqlConnection(connString);
            await conn.OpenAsync();

            try 
                { 
                // Create User and Tasks
                var repo = new TaskRepo(_factory, _sql); 
            
                var roleRepo = new RoleRepo(_factory, _sql);
                var roleParam = MockData.GetCreateRoleParameters().ElementAt(6);
                await roleRepo.CreateRoleAsync(roleParam);
                var role = await conn.QuerySingleAsync<CaseworkerRoleDto>(TestQueries.GetRole, new { name = roleParam.RoleName });

                var userRepo = new UserRepo(_factory, _sql);
                await userRepo.CreateUserAsync(MockData.GetCreateUserParameters(role.Id).ElementAt(1));

                var caseworker = await conn.QuerySingleAsync<UserDto>(TestQueries.GetCaseworker);
                //var taskParams = MockData.GetCreateTaskParameters(caseworker.Id);

                await repo.CreateTaskAsync(MockData.GetCreateTaskParameters(caseworker.Id).ElementAt(4));

                // Execute call to Get Tasks with Statuses SP
                var inserted = await conn.QuerySingleAsync<TaskDto>(TestQueries.GetTask);
                var result = await repo.GetTaskWithStatusesByIdAsync(inserted.Id);

                Assert.IsNotNull(result);
            }
            finally
                {
                // cleanup (when not using TransactionScope)
                await conn.ExecuteAsync(TestQueries.DeleteTaskStatus);
                await conn.ExecuteAsync(TestQueries.DeleteTask);
                await conn.ExecuteAsync(TestQueries.DeleteUser);
                await conn.ExecuteAsync(TestQueries.DeleteCaseworker);
                await conn.ExecuteAsync(TestQueries.DeleteRole);
            }
        }

        /// <summary>
        /// Logs the task status asynchronous writes task status returns1.
        /// </summary>
        [TestMethod, TestCategory("Integration")]
        public async Task LogTaskStatusAsync_WritesTaskStatus_Returns1()
        {
            using var conn = new SqlConnection(connString);
            await conn.OpenAsync();

            try
            {
                var repo = new TaskRepo(_factory, _sql);

                var roleRepo = new RoleRepo(_factory, _sql);
                var roleParam = MockData.GetCreateRoleParameters().ElementAt(11);
                await roleRepo.CreateRoleAsync(roleParam);
                var role = await conn.QuerySingleAsync<CaseworkerRoleDto>(TestQueries.GetRole, new { name = roleParam.RoleName });

                var userRepo = new UserRepo(_factory, _sql);
                await userRepo.CreateUserAsync(MockData.GetCreateUserParameters(role.Id).ElementAt(4));

                var caseworker = await conn.QuerySingleAsync<UserDto>(TestQueries.GetCaseworker);
                await repo.CreateTaskAsync(MockData.GetCreateTaskParameters(caseworker.Id).ElementAt(5));
                var task = await conn.QuerySingleAsync<TaskDto>(TestQueries.GetTask);

                await repo.LogTaskStatusAsync(MockData.GetLogTaskStatusParameter(task.Id, caseworker.Id));
                var inserted = await conn.QuerySingleAsync<TaskStatusDto>(TestQueries.GetTaskStatus, new { taskId = task.Id });

                Assert.AreEqual(2, inserted.StatusId);
            }
            finally
            {
                // cleanup (when not using TransactionScope)
                await conn.ExecuteAsync(TestQueries.DeleteTaskStatus);
                await conn.ExecuteAsync(TestQueries.DeleteTask);
                await conn.ExecuteAsync(TestQueries.DeleteUser);
                await conn.ExecuteAsync(TestQueries.DeleteCaseworker);
                await conn.ExecuteAsync(TestQueries.DeleteRole);
            }

        }

        /// <summary>
        /// Logs the task status asynchronous when duplicate task status throws or fails gracefully.
        /// </summary>
        [TestMethod, TestCategory("Integration")]
        public async Task LogTaskStatusAsync_WhenDuplicateTaskStatus_ThrowsOrFailsGracefully()
        {
            using var conn = new SqlConnection(connString);
            await conn.OpenAsync();

            try
            {
                var roleRepo = new RoleRepo(_factory, _sql);
                var roleParam = MockData.GetCreateRoleParameters().ElementAt(12);
                await roleRepo.CreateRoleAsync(roleParam);
                var role = await conn.QuerySingleAsync<CaseworkerRoleDto>(TestQueries.GetRole, new { name = roleParam.RoleName });

                var userRepo = new UserRepo(_factory, _sql);
                await userRepo.CreateUserAsync(MockData.GetCreateUserParameters(role.Id).ElementAt(2));
                var caseworker = await conn.QuerySingleAsync<UserDto>(TestQueries.GetCaseworker);

                var param = MockData.GetCreateTaskParameters(caseworker.Id).ElementAt(6);
                var repo = new TaskRepo(_factory, _sql);

                // Create Task
                await repo.CreateTaskAsync(param);
                var task = await conn.QuerySingleAsync<TaskDto>(TestQueries.GetTask);

                // Seed first insert
                await repo.LogTaskStatusAsync(MockData.GetLogTaskStatusParameter(task.Id, caseworker.Id));                

                // Act again with same name
                // If your repo throws, assert throws; if it returns a code, assert that.
                var ex = await Assert.ThrowsExceptionAsync<SqlException>(() => repo.LogTaskStatusAsync(MockData.GetLogTaskStatusParameter(task.Id, caseworker.Id)));
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, MockData.TaskUpdatedException);
            }
            finally
            {
                await conn.ExecuteAsync(TestQueries.DeleteTaskStatus);
                await conn.ExecuteAsync(TestQueries.DeleteTask);
                await conn.ExecuteAsync(TestQueries.DeleteUser);
                await conn.ExecuteAsync(TestQueries.DeleteCaseworker);
                await conn.ExecuteAsync(TestQueries.DeleteRole);
            }
        }

        /// <summary>
        /// Logs the task statuses asynchronous writes task statuses returns1.
        /// </summary>
        [TestMethod, TestCategory("Integration")]
        public async Task LogTaskStatusesAsync_WritesTaskStatuses_Returns1()
        {
            using var conn = new SqlConnection(connString);
            await conn.OpenAsync();

            try
            {
                var repo = new TaskRepo(_factory, _sql);

                // Create role
                var roleRepo = new RoleRepo(_factory, _sql);
                var roleParam = MockData.GetCreateRoleParameters().ElementAt(12);
                await roleRepo.CreateRoleAsync(roleParam);
                var role = await conn.QuerySingleAsync<CaseworkerRoleDto>(TestQueries.GetRole, new { name = roleParam.RoleName });

                // Create user
                var userRepo = new UserRepo(_factory, _sql);
                await userRepo.CreateUserAsync(MockData.GetCreateUserParameters(role.Id).ElementAt(4));

                // Create multiple tasks
                var caseworker = await conn.QuerySingleAsync<UserDto>(TestQueries.GetCaseworker);
                await repo.CreateTaskAsync(MockData.GetCreateTaskParameters(caseworker.Id).ElementAt(6));
                var task1 = await conn.QuerySingleAsync<TaskDto>(TestQueries.GetTask);
                await repo.CreateTaskAsync(MockData.GetCreateTaskParameters(caseworker.Id).ElementAt(7));
                var task2 = await conn.QuerySingleAsync<TaskDto>(TestQueries.GetTask);
                var taskIds = new List<int>();
                taskIds.Add(task1.Id);
                taskIds.Add(task2.Id);

                // Update task status for each task
                await repo.LogTaskStatusesAsync(MockData.GetLogTaskStatusParameters(taskIds, caseworker.Id));
                var insertedTaskStatus1 = await conn.QueryAsync<TaskStatusDto>(TestQueries.GetTaskStatus, new { taskId = task1.Id });
                var insertedTaskStatus2 = await conn.QueryAsync<TaskStatusDto>(TestQueries.GetTaskStatus, new { taskId = task2.Id });

                Assert.AreEqual(2, insertedTaskStatus1.First().StatusId);
                Assert.AreEqual(2, insertedTaskStatus2.First().StatusId);
            }
            finally
            {
                // cleanup (when not using TransactionScope)
                await conn.ExecuteAsync(TestQueries.DeleteTaskStatus);
                await conn.ExecuteAsync(TestQueries.DeleteTask);
                await conn.ExecuteAsync(TestQueries.DeleteUser);
                await conn.ExecuteAsync(TestQueries.DeleteCaseworker);
                await conn.ExecuteAsync(TestQueries.DeleteRole);
            }

        }

        // Simple inline factory for tests
        /// <summary>
        /// The InlineFactory
        /// </summary>
        /// <seealso cref="IDbConnectionFactory" />
        private sealed class InlineFactory : IDbConnectionFactory
        {
            private readonly string _cs;
            public InlineFactory(string cs) => _cs = cs;
            public IDbConnection Create() => new SqlConnection(_cs);
            public IDbConnection Create(string name) => Create();
        }
    }
}