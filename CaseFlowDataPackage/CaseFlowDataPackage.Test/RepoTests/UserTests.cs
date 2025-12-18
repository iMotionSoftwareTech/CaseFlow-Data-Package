using CaseFlowDataPackage.Test.Helpers;
using Dapper;
using IMotionSoftware.CaseFlowDataPackage.DomainObjects;
using IMotionSoftware.CaseFlowDataPackage.Infrastructure.StoredProcedures;
using IMotionSoftware.CaseFlowDataPackage.Interfaces;
using IMotionSoftware.CaseFlowDataPackage.Repositories;
using Moq;
using System.Data;

namespace IMotionSoftware.CaseFlowDataPackage.Test.RepoTests
{
    /// <summary>
    /// The UserTests
    /// </summary>
    [TestClass]
    public class UserTests
    {
        /// <summary>
        /// The factory
        /// </summary>
        private Mock<IDbConnectionFactory> _factory = null!;

        /// <summary>
        /// The connection
        /// </summary>
        private Mock<IDbConnection> _conn = null!;

        /// <summary>
        /// The SQL
        /// </summary>
        private Mock<ISqlRunner> _sql = null!;

        /// <summary>
        /// The repo
        /// </summary>
        private UserRepo _repo = null!;

        /// <summary>
        /// Tests the initialize.
        /// </summary>
        [TestInitialize]
        public void TestInit()
        {
            _factory = new Mock<IDbConnectionFactory>();
            _conn = new Mock<IDbConnection>();
            _sql = new Mock<ISqlRunner>();

            _factory.Setup(f => f.Create()).Returns(_conn.Object);

            _repo = new UserRepo(_factory.Object, _sql.Object);
        }

        /// <summary>
        /// Tests the cleanup.
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            // If you created any IDisposable fakes/stubs, dispose them here.
            // Moq objects typically don't need disposal, but if you wrapped anything disposable:
            if (_conn?.Object is IDisposable d1) d1.Dispose();

            // Null the fields to avoid accidental cross-test reuse (helps if tests run in parallel)
            _repo = null;
            _sql = null;
            _conn = null;
            _factory = null;
        }

        /// <summary>
        /// Creates the user returns success count test.
        /// </summary>
        [TestMethod, TestCategory("UnitTest")]
        public async Task CreateUser_ReturnsSuccessCount_Test()
        {
            // Arrange
            var createUserParam = MockData.GetCreateUserParameters(1).First();
            var expectedResult = MockData.GetNewUserResult();
            _sql
              .Setup(s => s.ExecuteWithOutputAsync(
                  _conn.Object,
                  UserStoredProcedures.CreateUserSP, It.IsAny<DynamicParameters>(), It.IsAny<Func<DynamicParameters, NewUserResult>>(),
                  It.IsAny<IDbTransaction?>(), It.IsAny<int?>(), It.IsAny<CommandType?>()))
              .ReturnsAsync(expectedResult);

            // Act
            var result = await _repo.CreateUserAsync(createUserParam);

            //Assert
            Assert.AreEqual(expectedResult, result);
            _sql.Verify(s =>
                s.ExecuteWithOutputAsync(
                  _conn.Object,
                  UserStoredProcedures.CreateUserSP, It.IsAny<DynamicParameters>(), It.IsAny<Func<DynamicParameters, NewUserResult>>(),
                  It.IsAny<IDbTransaction?>(), It.IsAny<int?>(), It.IsAny<CommandType?>()),
                Times.Once);
        }

        /// <summary>
        /// Creates the user throws exception test.
        /// </summary>
        [TestMethod, TestCategory("UnitTest")]
        public async Task CreateUser_ThrowsException_Test()
        {
            // Arrange
            var createUserParam = MockData.GetCreateUserParameters(1).ElementAt(1);
            _sql
              .Setup(s => s.ExecuteWithOutputAsync(
                  _conn.Object,
                  UserStoredProcedures.CreateUserSP, It.IsAny<DynamicParameters>(), It.IsAny<Func<DynamicParameters, NewUserResult>>(),
                  It.IsAny<IDbTransaction?>(), It.IsAny<int?>(), It.IsAny<CommandType?>()))
              .ThrowsAsync(new Exception(MockData.TaskException));

            // Act
            var ex = await Assert.ThrowsExceptionAsync<Exception>(() => _repo.CreateUserAsync(createUserParam));

            // Assert
            Assert.AreEqual(MockData.TaskException, ex.Message);

            _sql.Verify(s =>
                s.ExecuteWithOutputAsync(
                  _conn.Object,
                  UserStoredProcedures.CreateUserSP, It.IsAny<DynamicParameters>(), It.IsAny<Func<DynamicParameters, NewUserResult>>(),
                  It.IsAny<IDbTransaction?>(), It.IsAny<int?>(), It.IsAny<CommandType?>()),
                Times.Once);
        }

        /// <summary>
        /// Gets the user returns user test.
        /// </summary>
        public async Task GetUser_ReturnsUser_Test()
        {
            // Arrange
            _sql.Setup(s => s.QuerySingleAsync<UserDetailResult>(_conn.Object,
                  UserStoredProcedures.GetUserSP,
                  It.IsAny<object?>(), It.IsAny<IDbTransaction?>(), It.IsAny<int?>(), It.IsAny<CommandType?>())).ReturnsAsync(MockData.GetUsersDetail());

            // Act
            var result = await _repo.GetUserAsync("john.smith@example.com");

            // Assert
            Assert.IsNotNull(result);
            _sql.Verify(s => s.QuerySingleAsync<UserDetailResult>(_conn.Object,
                  UserStoredProcedures.GetUserSP,
                  It.IsAny<object?>(), It.IsAny<IDbTransaction?>(), It.IsAny<int?>(), It.IsAny<CommandType?>()), Times.Once);
        }

        /// <summary>
        /// Updates the password attempt returns success count test.
        /// </summary>
        [TestMethod, TestCategory("UnitTest")]
        public async Task UpdatePasswordAttempt_ReturnsSuccessCount_Test()
        {
            // Arrange
            var expectedResult = MockData.GetPasswordAttemptResult();
            _sql
              .Setup(s => s.ExecuteWithOutputAsync(
                  _conn.Object,
                  UserStoredProcedures.UpdatePasswordAttemptSP, It.IsAny<DynamicParameters>(), It.IsAny<Func<DynamicParameters, PasswordAttemptResult>>(),
                  It.IsAny<IDbTransaction?>(), It.IsAny<int?>(), It.IsAny<CommandType?>()))
              .ReturnsAsync(expectedResult);

            // Act
            var result = await _repo.UpdatePasswordAttemptAsync(1, 3);

            //Assert
            Assert.AreEqual(expectedResult, result);
            _sql.Verify(s =>
                s.ExecuteWithOutputAsync(
                  _conn.Object,
                  UserStoredProcedures.UpdatePasswordAttemptSP, It.IsAny<DynamicParameters>(), It.IsAny<Func<DynamicParameters, PasswordAttemptResult>>(),
                  It.IsAny<IDbTransaction?>(), It.IsAny<int?>(), It.IsAny<CommandType?>()),
                Times.Once);
        }
    }
}