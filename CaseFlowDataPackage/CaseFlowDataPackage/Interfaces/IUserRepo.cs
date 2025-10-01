using IMotionSoftware.CaseFlowDataPackage.DomainObjects;
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

        /// <summary>
        /// Gets the user asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>The <see cref="Task{T}"/></returns>
        Task<UserDetailDto> GetUserAsync(string email);
    }
}