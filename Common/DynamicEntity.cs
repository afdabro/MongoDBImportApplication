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
    public class DynamicEntity : IUniqueId
    {
        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the data dictionary
        /// </summary>
        public IDictionary<string, object> dataDictionary { get; set; }

        /// <summary>
        /// Gets or sets an Expando Object
        /// </summary>
        public dynamic data { get; set; }

        /// <summary>
        /// Initialize a new instance of the <see cref="DynamicEntity"/> class.
        /// </summary>
        public DynamicEntity()
        {
            data = new ExpandoObject();
            dataDictionary = (IDictionary<string, object>)data;
            dataDictionary.Add("Id", null);
        }

        /// <summary>
        /// Add a property to the dynamic object
        /// </summary>
        /// <param name="propertyName">Name of property</param>
        /// <param name="propertyValue">Value of property</param>
        public void AddProperty(string propertyName, object propertyValue)
        {
            // add property
            dataDictionary.Add(propertyName, propertyValue);
        }

        /// <summary>
        /// Remove a property from the dynamic object
        /// </summary>
        /// <param name="propertyName">Name of Property</param>
        public void RemoveProperty(string propertyName)
        {
            // remove property
            dataDictionary.Remove(propertyName);
        }

        /// <summary>
        /// Get Property Value
        /// </summary>
        /// <param name="propertyName">Name of Property</param>
        /// <returns>Property Value</returns>
        public object GetProperty(string propertyName)
        {
            object value;
            bool success = dataDictionary.TryGetValue(propertyName, out value);

            if (success)
                return value;

            throw new Exception(string.Format("Property {0} value could not be extracted", propertyName));
        }

    }
}
