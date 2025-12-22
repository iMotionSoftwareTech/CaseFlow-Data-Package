namespace IMotionSoftware.CaseFlowDataPackage.DomainObjects.ParameterObjects
{
    /// <summary>
    /// The PasswordAttemptParameter
    /// </summary>
    public class PasswordAttemptParameter
    {
        /// <summary>
        /// Gets or sets the caseworker identifier.
        /// </summary>
        /// <value>
        /// The caseworker identifier.
        /// </value>
        public int CaseworkerId { get; set; }

        /// <summary>
        /// Gets or sets the maximum attempts.
        /// </summary>
        /// <value>
        /// The maximum attempts.
        /// </value>
        public int MaxAttempts { get; set; }
    }
}