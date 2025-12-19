using IMotionSoftware.CaseFlowDataPackage.DomainObjects;
using IMotionSoftware.CaseFlowDataPackage.DomainObjects.ParameterObjects;
using IMotionSoftware.CaseFlowDataPackage.Infrastructure.ParameterBuilders;
using IMotionSoftware.CaseFlowDataPackage.Infrastructure.ResultBuilders;
using IMotionSoftware.CaseFlowDataPackage.Infrastructure.StoredProcedures;
using IMotionSoftware.CaseFlowDataPackage.Interfaces;
using System.Data;

namespace IMotionSoftware.CaseFlowDataPackage.Repositories
{
    /// <summary>
    /// The RoleRepo
    /// </summary>
    /// <seealso cref="IMotionSoftware.CaseFlowDataPackage.Interfaces.IRoleRepo" />
    public class RoleRepo : IRoleRepo
    {
        /// <summary>
        /// The connection factory
        /// </summary>
        private readonly IDbConnectionFactory _connFactory;

        /// <summary>
        /// The SQL
        /// </summary>
        private readonly ISqlRunner _sqlRunner;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleRepo"/> class.
        /// </summary>
        /// <param name="userRepo">The user repo.</param>
        /// <param name="connFactory">The connection factory.</param>
        public RoleRepo(IDbConnectionFactory connFactory, ISqlRunner sqlRunner)
        {
            _connFactory = connFactory;
            _sqlRunner = sqlRunner;
        }

        /// <summary>
        /// Creates the role asynchronous.
        /// </summary>
        /// <param name="createRoleParameter">The create role parameter.</param>
        /// <returns>
        /// The <see cref="Task{T}" />
        /// </returns>
        public async Task<NewRoleResult> CreateRoleAsync(CreateRoleParameter createRoleParameter)
        {
            using var conn = _connFactory.Create();
            conn.Open();

            var parameters = createRoleParameter.CreateRoleDynamicParameters();
            return await _sqlRunner.ExecuteWithOutputAsync(conn, RoleStoredProcedures.CreateRoleSP, parameters,
                output => output.ToNewRoleResult(), ct: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Gets all roles.
        /// </summary>
        /// <returns>The <see cref="Task{T}"/></returns>
        public async Task<IEnumerable<CaseworkerRoleResult>> GetAllRolesAsync() 
        {
            using var conn = _connFactory.Create();
            conn.Open();

            return await _sqlRunner.QueryAsync<CaseworkerRoleResult>(conn, RoleStoredProcedures.GetAllRolesSP);
        }
    }
}