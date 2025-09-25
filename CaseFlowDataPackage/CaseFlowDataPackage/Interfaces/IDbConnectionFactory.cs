using System.Data;

namespace IMotionSoftware.CaseFlowDataPackage.Interfaces
{
    /// <summary>
    /// The IDbConnectionFactory
    /// </summary>
    public interface IDbConnectionFactory
    {
        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        IDbConnection Create();

        /// <summary>
        /// Creates the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        IDbConnection Create(string name);
    }
}