using IMotionSoftware.CaseFlowDataPackage.DomainObjects;
using IMotionSoftware.CaseFlowDataPackage.DomainObjects.ParameterObjects;
using IMotionSoftware.CaseFlowDataPackage.Infrastructure.ParameterBuilders;
using IMotionSoftware.CaseFlowDataPackage.Infrastructure.StoredProcedures;
using IMotionSoftware.CaseFlowDataPackage.Interfaces;

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
        /// The <see cref="Task{int}" />
        /// </returns>
        public async Task<int> CreateUserAsync(CreateUserParameter createUserParameter)
        {
            using var conn = _connFactory.Create();
            conn.Open();

            var parameters = createUserParameter.CreateUserDynamicParameters();
            return await _sqlRunner.ExecuteAsync(conn, UserStoredProcedures.CreateUserSP, parameters);
        }

        /// <summary>
        /// Gets the user asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>
        /// The <see cref="Task{T}" />
        /// </returns>
        public async Task<UserDetailDto> GetUserAsync(string email)
        {
            using var conn = _connFactory.Create();
            conn.Open();

            var parameters = email.GetUserDynamicParameters();
            return await _sqlRunner.QuerySingleAsync<UserDetailDto>(conn, UserStoredProcedures.GetUserSP, parameters);
        }
    }
}