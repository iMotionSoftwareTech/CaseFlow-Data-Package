namespace IMotionSoftware.CaseFlowDataPackage.DomainObjects.ParameterObjects
{
    /// <summary>
    /// The LogTaskStatusParameter
    /// </summary>
    public class LogTaskStatusParameter
    {
        /// <summary>
        /// Gets or sets the task identifier.
        /// </summary>
        /// <value>
        /// The task identifier.
        /// </value>
        public int TaskId { get; set; }

        /// <summary>
        /// Gets or sets the status identifier.
        /// </summary>
        /// <value>
        /// The status identifier.
        /// </value>
        public int StatusId { get; set; }

        /// <summary>
        /// Gets or sets the caseworker identifier.
        /// </summary>
        /// <value>
        /// The caseworker identifier.
        /// </value>
        public int CaseworkerId { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>
        public string? Notes { get; set; }

        /// <summary>
        /// Gets or sets the log date time.
        /// </summary>
        /// <value>
        /// The log date time.
        /// </value>
        public DateTime LogDateTime { get; set; }
    }
}