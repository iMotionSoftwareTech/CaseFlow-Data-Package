using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMotionSoftware.CaseFlowDataPackage.DomainObjects.ParameterObjects
{
    /// <summary>
    /// The CreateUserParameter
    /// </summary>
    public class CreateUserParameter
    {
        /// <summary>
        /// Gets or sets the caseworker role identifier.
        /// </summary>
        /// <value>
        /// The caseworker role identifier.
        /// </value>
        public int CaseworkerRoleId { get; set; }

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
        public required string Surname  { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public required string Email  { get; set; }

        /// <summary>
        /// Gets or sets the password hash.
        /// </summary>
        /// <value>
        /// The password hash.
        /// </value>
        public byte[] PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the password salt.
        /// </summary>
        /// <value>
        /// The password salt.
        /// </value>
        public byte[] PasswordSalt { get; set; }

        /// <summary>
        /// Gets or sets the created date time.
        /// </summary>
        /// <value>
        /// The created date time.
        /// </value>
        public DateTime CreatedDateTime {get; set; }
    }
}