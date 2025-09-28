using IMotionSoftware.CaseFlowDataPackage.DomainObjects.ParameterObjects;

namespace IMotionSoftware.CaseFlowDataPackage.Interfaces
{
    /// <summary>
    /// The IUserRepo
    /// </summary>
    public interface IUserRepo
    {
        /// <summary>
        /// Creates the user asynchronous.
        /// </summary>
        /// <param name="createUserParameter">The create user parameter.</param>
        /// <returns>The <see cref="Task{int}"/></returns>
        Task<int> CreateUserAsync(CreateUserParameter createUserParameter);
    }
}