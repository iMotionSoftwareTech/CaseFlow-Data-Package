using CaseFlowDataPackage.Test.Helpers;
using IMotionSoftware.CaseFlowDataPackage.DomainObjects;
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
    /// The multi
    /// </summary>
    private Mock<IMultiReader> _multi = null!;

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
        _multi = new Mock<IMultiReader>();
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
        var createTaskParam = MockData.GetCreateTaskParameters(1).First();
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
        var createTaskParam = MockData.GetCreateTaskParameters(1).ElementAt(1);
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

    /// <summary>
    /// Gets all statuses returns statuses test.
    /// </summary>
    [TestMethod, TestCategory("UnitTest")]
    public async Task GetAllStatuses_ReturnsStatuses_Test()
    {
        // Arrange
        _sql.Setup(s => s.QueryAsync<StatusDto>(_conn.Object,
              TaskStoredProcedures.GetAllStatusesSP,
              It.IsAny<object?>(), It.IsAny<IDbTransaction?>(), It.IsAny<int?>(), It.IsAny<CommandType?>())).ReturnsAsync(MockData.GetStatuses());

        // Act
        var result = await _repo.GetAllStatusesAsync();

        // Assert
        Assert.IsNotNull(result);
        _sql.Verify(s => s.QueryAsync<StatusDto>(_conn.Object,
              TaskStoredProcedures.GetAllStatusesSP,
              It.IsAny<object?>(), It.IsAny<IDbTransaction?>(), It.IsAny<int?>(), It.IsAny<CommandType?>()), Times.Once);
    }

    /// <summary>
    /// Gets all tasks returns tasks test.
    /// </summary>
    [TestMethod, TestCategory("UnitTest")]
    public async Task GetAllTasks_ReturnsTasks_Test()
    {
        // Arrange
        var expectedTotal = 3;
        _multi.Setup(m => m.ReadSingleAsync<int>()).ReturnsAsync(expectedTotal);
        _multi.Setup(m => m.ReadAsync<TaskDto>()).ReturnsAsync(MockData.GetTasks());
        _sql.Setup(s => s.QueryMultipleAsync(_conn.Object,
              TaskStoredProcedures.GetAllTasksSP,
              It.IsAny<object?>(), It.IsAny<IDbTransaction?>(), It.IsAny<int?>(), It.IsAny<CommandType?>())).ReturnsAsync(_multi.Object);

        // Act
        var (totalCount, taskWithStatus) = await _repo.GetAllTasksAsync(MockData.GetAllTasksParameter());

        // Assert
        Assert.AreEqual(expectedTotal, totalCount);
        CollectionAssert.AreEquivalent(MockData.GetTasks().Select(m => m.Id).ToList(), taskWithStatus.Select(x => x.Id).ToList());
        _sql.Verify(s => s.QueryMultipleAsync(
            _conn.Object,
            TaskStoredProcedures.GetAllTasksSP,
            It.IsAny<object?>(),
            null, null, null), Times.Once);
        _multi.Verify(m => m.ReadSingleAsync<int>(), Times.Once);
        _multi.Verify(m => m.ReadAsync<TaskDto>(), Times.Once);
        _conn.Verify(c => c.Open(), Times.Once);
    }

    /// <summary>
    /// Gets the task with statuses by identifier returns task with status test.
    /// </summary>
    public async Task GetTaskWithStatusesById_ReturnsTaskWithStatus_Test()
    {
        // Arrange
        _sql.Setup(s => s.QuerySingleAsync<IEnumerable<TaskStatusDto>>(_conn.Object,
              TaskStoredProcedures.GetTaskWithStatusesByIdSP,
              It.IsAny<object?>(), It.IsAny<IDbTransaction?>(), It.IsAny<int?>(), It.IsAny<CommandType?>())).ReturnsAsync(MockData.GetTaskStatuses());

        // Act
        var result = await _repo.GetTaskWithStatusesByIdAsync(1);

        // Assert
        Assert.IsNotNull(result);
        _sql.Verify(s => s.QuerySingleAsync<IEnumerable<TaskStatusDto>>(_conn.Object,
              TaskStoredProcedures.GetTaskWithStatusesByIdSP,
              It.IsAny<object?>(), It.IsAny<IDbTransaction?>(), It.IsAny<int?>(), It.IsAny<CommandType?>()), Times.Once);
    }
}