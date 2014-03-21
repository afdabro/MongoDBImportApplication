// Author: Andrew F. Dabrowski
// Date: 3/20/2014
// copyright© 2014

using System;

namespace Common
{

    /// <summary>
    /// Common Interface for Entities
    /// </summary>
    public interface IUniqueId
    {
        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        Guid Id { get; set; }
    }
}
