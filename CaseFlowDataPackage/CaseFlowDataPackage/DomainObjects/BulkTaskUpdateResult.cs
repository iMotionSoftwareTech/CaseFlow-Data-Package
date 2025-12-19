namespace IMotionSoftware.CaseFlowDataPackage.DomainObjects
{
    /// <summary>
    /// The BulkTaskUpdateResult
    /// </summary>
    /// <seealso cref="IMotionSoftware.CaseFlowDataPackage.DomainObjects.BaseResult" />
    public class BulkTaskUpdateResult : BaseResult
    {
        /// <summary>
        /// Gets or sets the inserted count.
        /// </summary>
        /// <value>
        /// The inserted count.
        /// </value>
        public int InsertedCount { get; set; }
    }
}