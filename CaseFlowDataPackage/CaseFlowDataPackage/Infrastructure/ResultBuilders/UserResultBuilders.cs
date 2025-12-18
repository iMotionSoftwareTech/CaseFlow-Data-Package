using Dapper;
using IMotionSoftware.CaseFlowDataPackage.DomainObjects;

namespace IMotionSoftware.CaseFlowDataPackage.Infrastructure.ResultBuilders
{
    /// <summary>
    /// The UserResultBuilders
    /// </summary>
    public static class UserResultBuilders
    {
        /// <summary>
        /// To the password attempt result.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <returns>The <see cref="PasswordAttemptResult"/></returns>
        public static PasswordAttemptResult ToPasswordAttemptResult(this DynamicParameters p)
        {
            return new PasswordAttemptResult
            {
                Success = p.Get<bool>("success"),
                ErrorMessage = p.Get<string>("errorMessage"),
                NewAttemptCount = p.Get<int>("newAttemptCount"),
                WasLocked = p.Get<bool>("wasLocked")
            };
        }

        /// <summary>
        /// Converts to newuserresult.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <returns>The <see cref="NewUserResult"/></returns>
        public static NewUserResult ToNewUserResult(this DynamicParameters p) 
        {
            return new NewUserResult
            {
                CaseworkerId = p.Get<int>("caseworkerId"),
                Success = p.Get<bool>("success"),
                ErrorMessage = p.Get<string>("errorMessage")
            };
        }
    }
}