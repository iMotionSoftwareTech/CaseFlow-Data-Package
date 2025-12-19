namespace IMotionSoftware.CaseFlowDataPackage.DomainObjects
{
    /// <summary>
    /// The PasswordAttemptResult
    /// </summary>
    /// <seealso cref="IMotionSoftware.CaseFlowDataPackage.DomainObjects.BaseResult" />
    public class PasswordAttemptResult : BaseResult
    {
        /// <summary>
        /// Creates new attemptcount.
        /// </summary>
        /// <value>
        /// The new attempt count.
        /// </value>
        public int NewAttemptCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [was locked].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [was locked]; otherwise, <c>false</c>.
        /// </value>
        public bool WasLocked { get; set; }
    }
}