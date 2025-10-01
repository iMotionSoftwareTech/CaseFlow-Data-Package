using Dapper;
using IMotionSoftware.CaseFlowDataPackage.DomainObjects.ParameterObjects;
using System.Data;

namespace IMotionSoftware.CaseFlowDataPackage.Infrastructure.ParameterBuilders
{
    /// <summary>
    /// The UserParameterBuilder
    /// </summary>
    public static class UserParameterBuilder
    {
        /// <summary>
        /// Creates the user dynamic parameters.
        /// </summary>
        /// <param name="createUserParameter">The create user parameter.</param>
        /// <returns>The <see cref="DynamicParameters"/></returns>
        public static DynamicParameters CreateUserDynamicParameters(this CreateUserParameter createUserParameter)
        {
            var parameters = new DynamicParameters();
            parameters.Add("caseworkerRoleId", createUserParameter.CaseworkerRoleId, DbType.Int32, ParameterDirection.Input);
            parameters.Add("forename", createUserParameter.Forename, DbType.String, ParameterDirection.Input);
            parameters.Add("surname", createUserParameter.Surname, DbType.String, ParameterDirection.Input);
            parameters.Add("email", createUserParameter.Email, DbType.String, ParameterDirection.Input);
            parameters.Add("passwordHash", createUserParameter.PasswordHash, DbType.Binary, ParameterDirection.Input);
            parameters.Add("passwordSalt", createUserParameter.PasswordSalt, DbType.Binary, ParameterDirection.Input);
            parameters.Add("createdDateTime", createUserParameter.CreatedDateTime, DbType.DateTime2, ParameterDirection.Input);

            return parameters;
        }

        /// <summary>
        /// Gets the user dynamic parameters.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>The <see cref="DynamicParameters"/></returns>
        public static DynamicParameters GetUserDynamicParameters(this string email)
        {
            var parameters = new DynamicParameters();
            parameters.Add("email", email, DbType.String, ParameterDirection.Input);

            return parameters;
        }
    }
}