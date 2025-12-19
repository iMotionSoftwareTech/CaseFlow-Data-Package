using Dapper;
using IMotionSoftware.CaseFlowDataPackage.DomainObjects;

namespace IMotionSoftware.CaseFlowDataPackage.Infrastructure.ResultBuilders
{
    /// <summary>
    /// The RoleResultBuilders
    /// </summary>
    public static class RoleResultBuilders
    {
        /// <summary>
        /// Converts to newroleresult.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <returns>The <see cref="NewRoleResult"/></returns>
        public static NewRoleResult ToNewRoleResult(this DynamicParameters p)
        {
            return new NewRoleResult
            {
                Success = p.Get<bool>("success"),
                RoleId = p.Get<int>("roleId"),
                ErrorMessage = p.Get<string>("errorMessage")
            };
        }
    }
}