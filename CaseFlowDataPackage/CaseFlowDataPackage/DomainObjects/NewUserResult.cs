namespace IMotionSoftware.CaseFlowDataPackage.DomainObjects
{
    /// <summary>
    /// The NewUserResult
    /// </summary>
    /// <seealso cref="IMotionSoftware.CaseFlowDataPackage.DomainObjects.BaseResult" />
    public class NewUserResult : BaseResult
    {
        /// <summary>
        /// Gets or sets the caseworker identifier.
        /// </summary>
        /// <value>
        /// The caseworker identifier.
        /// </value>
        public int CaseworkerId { get; set; }
    }
}