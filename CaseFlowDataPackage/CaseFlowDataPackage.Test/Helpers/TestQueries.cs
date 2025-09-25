using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseFlowDataPackage.Test.Helpers
{
    /// <summary>
    /// The TestQueries
    /// </summary>
    public static class TestQueries
    {
        /// <summary>
        /// The get role
        /// </summary>
        public static string GetRole = "SELECT TOP 1 * FROM caseFlow.CaseworkerRole WHERE Name = 'Test Role'";

        /// <summary>
        /// The delete role
        /// </summary>
        public static string DeleteRole = "DELETE FROM caseFlow.CaseworkerRole WHERE Name = 'Test Role'";
    }
}
