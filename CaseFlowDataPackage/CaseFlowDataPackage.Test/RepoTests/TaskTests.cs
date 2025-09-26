using CaseFlowDataPackage.Test.Helpers;
using IMotionSoftware.CaseFlowDataPackage.Infrastructure.StoredProcedures;
using IMotionSoftware.CaseFlowDataPackage.Interfaces;
using IMotionSoftware.CaseFlowDataPackage.Repositories;
using Moq;
using System.Data;

namespace IMotionSoftware.CaseFlowDataPackage.Test.RepoTests;

/// <summary>
/// The TaskTests
/// </summary>
[TestClass]
public class TaskTests
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
    private TaskRepo _repo = null!;

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

        _repo = new TaskRepo(_factory.Object, _sql.Object);
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
    /// Creates the task returns success count test.
    /// </summary>
    [TestMethod, TestCategory("UnitTest")]
    public async Task CreateTask_ReturnsSuccessCount_Test()
    {
        // Arrange
        var createTaskParam = MockData.GetCreateTaskParameters().First();
        _sql
          .Setup(s => s.ExecuteAsync(
              _conn.Object,
              TaskStoredProcedures.CreateTaskSP,
              It.IsAny<object?>(), It.IsAny<IDbTransaction?>(), It.IsAny<int?>(), It.IsAny<CommandType?>()))
          .ReturnsAsync(-1);

        // Act
        var result = await _repo.CreateTaskAsync(createTaskParam);

        //Assert
        Assert.AreEqual(-1, result);
        _sql.Verify(s =>
            s.ExecuteAsync(
              _conn.Object,
              TaskStoredProcedures.CreateTaskSP,
              It.IsAny<object?>(), It.IsAny<IDbTransaction?>(), It.IsAny<int?>(), It.IsAny<CommandType?>()),
            Times.Once);
    }

    /// <summary>
    /// Creates the task throws exception test.
    /// </summary>
    [TestMethod, TestCategory("UnitTest")]
    public async Task CreateTask_ThrowsException_Test()
    {
        // Arrange
        var createTaskParam = MockData.GetCreateTaskParameters().ElementAt(1);
        _sql
          .Setup(s => s.ExecuteAsync(
              _conn.Object,
              TaskStoredProcedures.CreateTaskSP,
              It.IsAny<object?>(), It.IsAny<IDbTransaction?>(), It.IsAny<int?>(), It.IsAny<CommandType?>()))
          .ThrowsAsync(new Exception(MockData.TaskException));

        // Act
        var ex = await Assert.ThrowsExceptionAsync<Exception>(() => _repo.CreateTaskAsync(createTaskParam));

        // Assert
        Assert.AreEqual(MockData.TaskException, ex.Message);

        _sql.Verify(s =>
            s.ExecuteAsync(
               _conn.Object,
              TaskStoredProcedures.CreateTaskSP,
              It.IsAny<object?>(), It.IsAny<IDbTransaction?>(), It.IsAny<int?>(), It.IsAny<CommandType?>()),
            Times.Once);
    }
}
