// Author: Andrew F. Dabrowski
// Date: 3/20/2014
// copyright© 2014

using Common;
using Common.Repository.MongoDB;
using System.Collections.Generic;
using System.Dynamic;

namespace MongoDBImportDataApplication
{
    /// <summary>
    /// Import Controller for importing data into MongoDB
    /// </summary>
    public class ImportController
    {
        /// <summary>
        /// Gets or sets the dynamic entity
        /// </summary>
        public DynamicEntityContainer entity { get; set; }

        /// <summary>
        /// Gets or sets the repository
        /// </summary>
        public MongoDBRepository repository { get; set; }

        public ImportController()
        {
            this.repository = new MongoDBRepository();
            this.entity = new DynamicEntityContainer();
        }

        /// <summary>
        /// Commit Entity to repository
        /// </summary>
        /// <param name="collectionName"></param>
        public void CommitEntity(string collectionName)
        {
            this.repository.CreateEntity<DynamicEntity>(collectionName, entity.dataDictionary);
        }

        /// <summary>
        /// Clear entity data
        /// </summary>
        public void ClearEntity()
        {
            this.entity = null;
            this.entity = new DynamicEntityContainer();
        }
    }
}
