using Dapper;
using IMotionSoftware.CaseFlowDataPackage.DomainObjects.ParameterObjects;
using System.Data;

namespace IMotionSoftware.CaseFlowDataPackage.Infrastructure.ParameterBuilders
{
    /// <summary>
    /// The RoleParameterBuilder
    /// </summary>
    public static class RoleParameterBuilder
    {
        /// <summary>
        /// Creates the role dynamic parameters.
        /// </summary>
        /// <param name="createRoleParameter">The create role parameter.</param>
        /// <returns></returns>
        public static DynamicParameters CreateRoleDynamicParameters(this CreateRoleParameter createRoleParameter)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@roleName", createRoleParameter?.RoleName, DbType.String, ParameterDirection.Input);
            parameters.Add("@description", createRoleParameter?.Description, DbType.String, ParameterDirection.Input);
            return parameters;
        }
    }
}
