namespace IMotionSoftware.CaseFlowDataPackage.DomainObjects
{
    /// <summary>
    /// The UserDetailDto
    /// </summary>
    public class UserDetailDto
    {
        /// <summary>
        /// Gets or sets the caseworker identifier.
        /// </summary>
        /// <value>
        /// The caseworker identifier.
        /// </value>
        public int CaseworkerId { get; set; }

        /// <summary>
        /// Gets or sets the caseworker role identifier.
        /// </summary>
        /// <value>
        /// The caseworker role identifier.
        /// </value>
        public int CaseworkerRoleId { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        public required string Role { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public required string Email { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public required string Username { get; set; }

        /// <summary>
        /// Gets or sets the forename.
        /// </summary>
        /// <value>
        /// The forename.
        /// </value>
        public required string Forename { get; set; }

        /// <summary>
        /// Gets or sets the surname.
        /// </summary>
        /// <value>
        /// The surname.
        /// </value>
        public required string Surname { get; set; }

        /// <summary>
        /// Gets or sets the password attempt.
        /// </summary>
        /// <value>
        /// The password attempt.
        /// </value>
        public int PasswordAttempt { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is locked.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is locked; otherwise, <c>false</c>.
        /// </value>
        public bool IsLocked { get; set; }
    }
}
