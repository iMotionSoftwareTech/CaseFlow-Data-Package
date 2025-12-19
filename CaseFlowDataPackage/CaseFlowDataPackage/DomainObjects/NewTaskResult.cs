namespace IMotionSoftware.CaseFlowDataPackage.DomainObjects
{
    /// <summary>
    /// The NewTaskResult
    /// </summary>
    /// <seealso cref="IMotionSoftware.CaseFlowDataPackage.DomainObjects.BaseResult" />
    public class NewTaskResult : BaseResult
    {
        /// <summary>
        /// Gets or sets the task identifier.
        /// </summary>
        /// <value>
        /// The task identifier.
        /// </value>
        public int TaskId { get; set; }
    }
}