// Author: Andrew F. Dabrowski
// Date: 3/20/2014
// copyright© 2014

using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Common
{
    /// <summary>
    /// A Dynamic Entity for users to manipulate at runtime
    /// </summary>
    public class DynamicEntityContainer
    {
        /// <summary>
        /// Gets or sets the dynamic entity data dictionary
        /// </summary>
        public DynamicEntity dataDictionary { get; set; }

        /// <summary>
        /// Initialize a new instance of the <see cref="DynamicEntityContainer"/> class.
        /// </summary>
        public DynamicEntityContainer()
        {
            dataDictionary = new DynamicEntity();
            dataDictionary.data = new Dictionary<string, object>();
            dataDictionary.Id = new Guid();         
        }

        /// <summary>
        /// Add a property to the dynamic object
        /// </summary>
        /// <param name="propertyName">Name of property</param>
        /// <param name="propertyValue">Value of property</param>
        public void AddProperty(string propertyName, object propertyValue)
        {
            // add property
            dataDictionary.data.Add(propertyName, propertyValue);
        }

        /// <summary>
        /// Remove a property from the dynamic object
        /// </summary>
        /// <param name="propertyName">Name of Property</param>
        public void RemoveProperty(string propertyName)
        {
            // remove property
            dataDictionary.data.Remove(propertyName);
        }

        /// <summary>
        /// Get Property Value
        /// </summary>
        /// <param name="propertyName">Name of Property</param>
        /// <returns>Property Value</returns>
        public object GetProperty(string propertyName)
        {
            object value;
            bool success = dataDictionary.data.TryGetValue(propertyName, out value);

            if (success)
                return value;

            throw new Exception(string.Format("Property {0} value could not be extracted", propertyName));
        }

    }
}
