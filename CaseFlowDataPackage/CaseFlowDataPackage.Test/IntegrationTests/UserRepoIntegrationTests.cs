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
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Development"}.json", optional: true)
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
        public async Task CreateUserAsync_CreatesUser_ReturnsSuccess()
        {
            using var conn = new SqlConnection(connString);
            await conn.OpenAsync();

            var repo = new UserRepo(_factory, _sql);
            await repo.CreateUserAsync(MockData.GetCreateUserParameters().First());

            var inserted = await conn.QuerySingleAsync<UserDto>(TestQueries.GetUser);
            Assert.AreEqual(MockData.Username, inserted.UserName);

            // cleanup (when not using TransactionScope)
            await conn.ExecuteAsync(TestQueries.DeleteUser);
            await conn.ExecuteAsync(TestQueries.DeleteCaseworker);
        }

        /// <summary>
        /// Creates the user asynchronous when duplicate task throws or fails gracefully.
        /// </summary>
        [TestMethod, TestCategory("Integration")]
        public async Task CreateUserAsync_WhenDuplicateTask_ThrowsOrFailsGracefully()
        {
            var param = MockData.GetCreateUserParameters().ElementAt(1);
            var repo = new UserRepo(_factory, _sql);

            // Seed first insert
            await repo.CreateUserAsync(param);

            try
            {
                // Act again with same name
                // If your repo throws, assert throws; if it returns a code, assert that.
                var ex = await Assert.ThrowsExceptionAsync<SqlException>(() => repo.CreateUserAsync(param));
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, MockData.TaskException);
            }
            finally
            {
                await using var conn = new SqlConnection(connString);
                await conn.OpenAsync();
                await conn.ExecuteAsync(TestQueries.DeleteUser);
                await conn.ExecuteAsync(TestQueries.DeleteCaseworker);
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