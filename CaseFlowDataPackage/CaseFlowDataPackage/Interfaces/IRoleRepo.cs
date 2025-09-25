using IMotionSoftware.CaseFlowDataPackage.DomainObjects.ParameterObjects;

namespace IMotionSoftware.CaseFlowDataPackage.Interfaces
{
    /// <summary>
    /// The IRoleRepo
    /// </summary>
    public interface IRoleRepo
    {
        /// <summary>
        /// Creates the role asynchronous.
        /// </summary>
        /// <param name="createRoleParameter">The create role parameter.</param>
        /// <returns>The <see cref="Task{int}"/></returns>
        Task<int> CreateRoleAsync(CreateRoleParameter createRoleParameter);
    }
}