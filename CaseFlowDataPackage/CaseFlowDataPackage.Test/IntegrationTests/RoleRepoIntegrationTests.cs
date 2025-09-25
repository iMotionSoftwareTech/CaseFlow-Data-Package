using CaseFlowDataPackage.Test.Helpers;
using Dapper;
using IMotionSoftware.CaseFlowDataPackage.DomainObjects;
using IMotionSoftware.CaseFlowDataPackage.Infrastructure.Data;
using IMotionSoftware.CaseFlowDataPackage.Interfaces;
using IMotionSoftware.CaseFlowDataPackage.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using System.Data;
using System.Transactions;

namespace IMotionSoftware.CaseFlowDataPackage.Test.IntegrationTests
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class RoleRepoIntegrationTests
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
        /// <exception cref="System.InvalidOperationException">Connection string not found</exception>
        [ClassInitialize]
        public static void ClassInit(TestContext _)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Development"}.json", optional: true)
                .AddUserSecrets<RoleRepoIntegrationTests>(optional: true)
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
        /// Creates the role asynchronous writes role returns1.
        /// </summary>
        [TestMethod, TestCategory("Integration")]
        public async Task CreateRoleAsync_WritesRole_Returns1()
        {
            using var conn = new SqlConnection(connString);
            await conn.OpenAsync();

            var repo = new RoleRepo(_factory, _sql);
            await repo.CreateRoleAsync(MockData.GetCreateRoleParameters().First());

            var inserted = await conn.QuerySingleAsync<CaseworkerRoleDto>(TestQueries.GetRole);
            Assert.AreEqual(MockData.RoleName, inserted.Name);

            // cleanup (when not using TransactionScope)
            await conn.ExecuteAsync(TestQueries.DeleteRole);
        }

        /// <summary>
        /// Creates the role asynchronous when duplicate name throws or fails gracefully.
        /// </summary>
        [TestMethod, TestCategory("Integration")]
        public async Task CreateRoleAsync_WhenDuplicateName_ThrowsOrFailsGracefully()
        {
            var param = MockData.GetCreateRoleParameters().ElementAt(1);
            var repo = new RoleRepo(_factory, _sql);

            // Seed first insert
            await repo.CreateRoleAsync(param);

            try
            {
                // Act again with same name
                // If your repo throws, assert throws; if it returns a code, assert that.
                var ex = await Assert.ThrowsExceptionAsync<SqlException>(() => repo.CreateRoleAsync(param));                
            }
            catch(Exception e)
            {
                StringAssert.Contains(e.Message, MockData.RoleException);
            }
            finally
            {
                await using var conn = new SqlConnection(connString);
                await conn.OpenAsync();
                await conn.ExecuteAsync(TestQueries.DeleteRole);
            }
        }

        // Simple inline factory for tests
        /// <summary>
        /// The InlineFactory
        /// </summary>
        /// <seealso cref="IMotionSoftware.CaseFlowDataPackage.Interfaces.IDbConnectionFactory" />
        private sealed class InlineFactory : IDbConnectionFactory
        {
            private readonly string _cs;
            public InlineFactory(string cs) => _cs = cs;
            public IDbConnection Create() => new SqlConnection(_cs);
            public IDbConnection Create(string name) => Create();
        }
    }
}
