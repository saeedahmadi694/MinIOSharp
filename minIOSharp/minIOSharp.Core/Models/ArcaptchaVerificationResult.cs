using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinIOSharpSharp.Core.Models
{

    /// <summary>
    /// Represents the JSON result for MinIOSharp.
    /// </summary>
    public class MinIOSharpVerificationResult
    {
        #region Properties

        /// <summary>
        /// Determines if the MinIOSharp verification was successful.
        /// </summary>
        [JsonProperty("success")]
        public bool Success
        {
            get;
            set;
        }
        #endregion Properties
    }
}
