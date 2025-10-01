using IMotionSoftware.CaseFlowDataPackage.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace IMotionSoftware.CaseFlowDataPackage.Infrastructure.Data
{
    /// <summary>
    /// The DbConnectionFactory
    /// </summary>
    public sealed class DbConnectionFactory : IDbConnectionFactory
    {
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration _config;
        /// <summary>
        /// The default name
        /// </summary>
        private readonly string _defaultName;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbConnectionFactory"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <param name="defaultName">The default name.</param>
        /// <exception cref="System.ArgumentNullException">config</exception>
        public DbConnectionFactory(IConfiguration config, string defaultName = "Default")
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _defaultName = defaultName;
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public IDbConnection Create() => Create(_defaultName);

        /// <summary>
        /// Creates the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException">Connection string '{name}' not found.</exception>
        public IDbConnection Create(string name)
        {
            var cs = _config.GetConnectionString(name);
            if (string.IsNullOrWhiteSpace(cs))
                throw new InvalidOperationException($"Connection string '{name}' not found.");

            // Swap provider here if you target another DB (e.g., NpgsqlConnection for Postgres)
            return new SqlConnection(cs);
        }
    }
}
