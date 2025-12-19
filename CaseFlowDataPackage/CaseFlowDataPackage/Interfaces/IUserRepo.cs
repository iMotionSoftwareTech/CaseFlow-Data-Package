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
        /// <returns>The <see cref="Task{T}"/></returns>
        Task<NewUserResult> CreateUserAsync(CreateUserParameter createUserParameter);

        /// <summary>
        /// Gets the user asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>The <see cref="Task{T}"/></returns>
        Task<UserDetailResult> GetUserAsync(string email);

        /// <summary>
        /// Updates the password attempt asynchronous.
        /// </summary>
        /// <param name="caseworkerId">The caseworker identifier.</param>
        /// <param name="maxAttempts">The maximum attempts.</param>
        /// <returns>
        /// The <see cref="Task{T}" />
        /// </returns>
        Task<PasswordAttemptResult> UpdatePasswordAttemptAsync(int caseworkerId, int maxAttempts);
    }
}