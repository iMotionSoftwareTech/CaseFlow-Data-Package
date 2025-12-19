using IMotionSoftware.CaseFlowDataPackage.DomainObjects;
using IMotionSoftware.CaseFlowDataPackage.DomainObjects.ParameterObjects;
using IMotionSoftware.CaseFlowDataPackage.Infrastructure.ParameterBuilders;
using IMotionSoftware.CaseFlowDataPackage.Infrastructure.ResultBuilders;
using IMotionSoftware.CaseFlowDataPackage.Infrastructure.StoredProcedures;
using IMotionSoftware.CaseFlowDataPackage.Interfaces;
using System.Data;

namespace IMotionSoftware.CaseFlowDataPackage.Repositories
{
    public class UserRepo : IUserRepo
    {
        /// <summary>
        /// The connection factory
        /// </summary>
        private readonly IDbConnectionFactory _connFactory;

        /// <summary>
        /// The SQL
        /// </summary>
        private readonly ISqlRunner _sqlRunner;

        public UserRepo(IDbConnectionFactory connFactory, ISqlRunner sqlRunner)
        {
            _connFactory = connFactory;
            _sqlRunner = sqlRunner;
        }

        /// <summary>
        /// Creates the user asynchronous.
        /// </summary>
        /// <param name="createUserParameter">The create user parameter.</param>
        /// <returns>
        /// The <see cref="Task{T}" />
        /// </returns>
        public async Task<NewUserResult> CreateUserAsync(CreateUserParameter createUserParameter)
        {
            using var conn = _connFactory.Create();
            conn.Open();

            var parameters = createUserParameter.CreateUserDynamicParameters();
            return await _sqlRunner.ExecuteWithOutputAsync(conn, UserStoredProcedures.CreateUserSP, parameters,
                output => output.ToNewUserResult(), ct: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Gets the user asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>
        /// The <see cref="Task{T}" />
        /// </returns>
        public async Task<UserDetailResult> GetUserAsync(string email)
        {
            using var conn = _connFactory.Create();
            conn.Open();

            var parameters = email.GetUserDynamicParameters();
            return await _sqlRunner.QuerySingleAsync<UserDetailResult>(conn, UserStoredProcedures.GetUserSP, parameters);
        }

        /// <summary>
        /// Updates the password attempt asynchronous.
        /// </summary>
        /// <param name="caseworkerId">The caseworker identifier.</param>
        /// <param name="maxAttempts">The maximum attempts.</param>
        /// <returns>
        /// The <see cref="Task{T}" />
        /// </returns>
        public async Task<PasswordAttemptResult> UpdatePasswordAttemptAsync(int caseworkerId, int maxAttempts)
        {
            using var conn = _connFactory.Create();
            conn.Open();

            var parameters = caseworkerId.UpdatePasswordDynamicParameters(maxAttempts);
            return await _sqlRunner.ExecuteWithOutputAsync(conn, UserStoredProcedures.UpdatePasswordAttemptSP, parameters,
                output => output.ToPasswordAttemptResult(), ct: CommandType.StoredProcedure);
        }
    }
}