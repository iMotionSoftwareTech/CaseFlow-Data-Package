using CaseFlowDataPackage.Test.Helpers;
using Dapper;
using IMotionSoftware.CaseFlowDataPackage.DomainObjects;
using IMotionSoftware.CaseFlowDataPackage.Infrastructure.Data;
using IMotionSoftware.CaseFlowDataPackage.Interfaces;
using IMotionSoftware.CaseFlowDataPackage.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace IMotionSoftware.CaseFlowDataPackage.Test.IntegrationTests
{
    /// <summary>
    /// The UserRepoIntegrationTests
    /// </summary>
    [TestClass]
    public class UserRepoIntegrationTests
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
        /// Creates the user asynchronous creates user returns success.
        /// </summary>
        [TestMethod, TestCategory("Integration")]
        [Ignore]
        public async Task CreateUserAsync_CreatesUser_ReturnsSuccess()
        {
            using var conn = new SqlConnection(connString);
            await conn.OpenAsync();
            try
            {
                var roleRepo = new RoleRepo(_factory, _sql);
                var roleParam = MockData.GetCreateRoleParameters().ElementAt(7);
                await roleRepo.CreateRoleAsync(roleParam);
                var role = await conn.QuerySingleAsync<CaseworkerRoleResult>(TestQueries.GetRole, new { name = roleParam.RoleName });

                var repo = new UserRepo(_factory, _sql);
                await repo.CreateUserAsync(MockData.GetCreateUserParameters(role.Id).First());

                var inserted = await conn.QuerySingleAsync<UserResult>(TestQueries.GetUser);
                Assert.AreEqual(MockData.Username, inserted.UserName);

            }
            finally
            {
                // cleanup (when not using TransactionScope)
                await conn.ExecuteAsync(TestQueries.DeleteUser);
                await conn.ExecuteAsync(TestQueries.DeleteCaseworker);
                await conn.ExecuteAsync(TestQueries.DeleteRole);
            }
        }

        /// <summary>
        /// Creates the user asynchronous when duplicate task throws or fails gracefully.
        /// </summary>
        [TestMethod, TestCategory("Integration")]
        [Ignore]
        public async Task CreateUserAsync_WhenDuplicateTask_ThrowsOrFailsGracefully()
        {
            await using var conn = new SqlConnection(connString);
            await conn.OpenAsync();
            try
            {
                var roleRepo = new RoleRepo(_factory, _sql);
                var roleParam = MockData.GetCreateRoleParameters().ElementAt(8);
                await roleRepo.CreateRoleAsync(roleParam);
                var role = await conn.QuerySingleAsync<CaseworkerRoleResult>(TestQueries.GetRole, new { name = roleParam.RoleName });

                var param = MockData.GetCreateUserParameters(role.Id).ElementAt(1);
                var repo = new UserRepo(_factory, _sql);

                // Seed first insert
                await repo.CreateUserAsync(param);

                // Act again with same name
                // If your repo throws, assert throws; if it returns a code, assert that.
                var ex = await Assert.ThrowsExceptionAsync<ApplicationException>(() => repo.CreateUserAsync(param));
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, MockData.TaskException);
            }
            finally
            {
                await conn.ExecuteAsync(TestQueries.DeleteUser);
                await conn.ExecuteAsync(TestQueries.DeleteCaseworker);
                await conn.ExecuteAsync(TestQueries.DeleteRole);
            }
        }

        /// <summary>
        /// Gets the user asynchronous returns user from database.
        /// </summary>
        [TestMethod, TestCategory("Integration")]
        [Ignore]
        public async Task GetUserAsync_ReturnsUser_FromDb()
        {
            using var conn = new SqlConnection(connString);
            await conn.OpenAsync();

            try
            {
                var roleRepo = new RoleRepo(_factory, _sql);
                var roleParam = MockData.GetCreateRoleParameters().ElementAt(9);
                await roleRepo.CreateRoleAsync(roleParam);
                var role = await conn.QuerySingleAsync<CaseworkerRoleResult>(TestQueries.GetRole, new { name = roleParam.RoleName });

                var repo = new UserRepo(_factory, _sql);
                await repo.CreateUserAsync(MockData.GetCreateUserParameters(role.Id).First());

                var result = await repo.GetUserAsync(MockData.GetCreateUserParameters(role.Id).First().Email);

                Assert.IsNotNull(result);
            }
            finally
            {
                // cleanup (when not using TransactionScope)
                await conn.ExecuteAsync(TestQueries.DeleteUser);
                await conn.ExecuteAsync(TestQueries.DeleteCaseworker);
                await conn.ExecuteAsync(TestQueries.DeleteRole);
            }
        }

        /// <summary>
        /// Updates the password attempt asynchronous updates password returns success.
        /// </summary>
        [TestMethod, TestCategory("Integration")]
        [Ignore]
        public async Task UpdatePasswordAttemptAsync_UpdatesPassword_ReturnsSuccess()
        {
            using var conn = new SqlConnection(connString);
            await conn.OpenAsync();
            try
            {
                var roleRepo = new RoleRepo(_factory, _sql);
                var roleParam = MockData.GetCreateRoleParameters().ElementAt(10);
                await roleRepo.CreateRoleAsync(roleParam);
                var role = await conn.QuerySingleAsync<CaseworkerRoleResult>(TestQueries.GetRole, new { name = roleParam.RoleName });

                var repo = new UserRepo(_factory, _sql);
                await repo.CreateUserAsync(MockData.GetCreateUserParameters(role.Id).ElementAt(2));

                var caseworker = await conn.QuerySingleAsync<UserResult>(TestQueries.GetCaseworker);
                await repo.UpdatePasswordAttemptAsync(caseworker.Id, 3);

                var inserted = await conn.QuerySingleAsync<UserResult>(TestQueries.GetUserUpdatedPasswordAttempt);
                Assert.AreEqual(1, inserted.PasswordAttempt);
            }
            finally
            {
                // cleanup (when not using TransactionScope)
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