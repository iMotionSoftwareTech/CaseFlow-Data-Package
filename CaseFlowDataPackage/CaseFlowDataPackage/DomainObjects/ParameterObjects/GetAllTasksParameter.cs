namespace IMotionSoftware.CaseFlowDataPackage.DomainObjects.ParameterObjects
{
    /// <summary>
    /// The GetAllTasksParameter
    /// </summary>
    public class GetAllTasksParameter
    {
        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        /// <value>
        /// The page number.
        /// </value>
        public int PageNumber { get; set; }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>
        /// The size of the page.
        /// </value>
        public int PageSize { get; set; }
    }
}