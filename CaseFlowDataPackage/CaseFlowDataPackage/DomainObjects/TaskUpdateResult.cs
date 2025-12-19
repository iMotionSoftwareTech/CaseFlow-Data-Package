namespace IMotionSoftware.CaseFlowDataPackage.DomainObjects
{
    /// <summary>
    /// The TaskUpdateResult
    /// </summary>
    /// <seealso cref="IMotionSoftware.CaseFlowDataPackage.DomainObjects.BaseResult" />
    public class TaskUpdateResult : BaseResult
    {
        /// <summary>
        /// Gets or sets the task status identifier.
        /// </summary>
        /// <value>
        /// The task status identifier.
        /// </value>
        public int TaskStatusId { get; set; }
    }
}