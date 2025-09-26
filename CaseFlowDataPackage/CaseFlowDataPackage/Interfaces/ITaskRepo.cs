using IMotionSoftware.CaseFlowDataPackage.DomainObjects.ParameterObjects;

namespace IMotionSoftware.CaseFlowDataPackage.Interfaces
{
    /// <summary>
    /// The ITaskRepo
    /// </summary>
    public interface ITaskRepo
    {
        /// <summary>
        /// Creates the task asynchronous.
        /// </summary>
        /// <param name="createTaskParameter">The create task parameter.</param>
        /// <returns>The <see cref="Task{int}"/></returns>
        Task<int> CreateTaskAsync(CreateTaskParameter createTaskParameter);
    }
}