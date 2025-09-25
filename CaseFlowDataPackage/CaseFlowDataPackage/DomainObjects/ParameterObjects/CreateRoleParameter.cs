namespace IMotionSoftware.CaseFlowDataPackage.DomainObjects.ParameterObjects
{
    /// <summary>
    /// The CreateRoleParameter
    /// </summary>
    public class CreateRoleParameter
    {
        /// <summary>
        /// Gets or sets the name of the role.
        /// </summary>
        /// <value>
        /// The name of the role.
        /// </value>
        public required string RoleName { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string? Description { get; set; }
    }
}
