// Author: Andrew F. Dabrowski
// Date: 3/20/2014
// copyright© 2014

using Common;
using Common.Repository.MongoDB;

namespace MongoDBImportDataApplication
{
    public class ImportController
    {
        /// <summary>
        /// Gets or sets the dynamic entity
        /// </summary>
        public DynamicEntity entity { get; set; }

        /// <summary>
        /// Gets or sets the repository
        /// </summary>
        public MongoDBRepository repository { get; set; }

        public ImportController()
        {
            this.repository = new MongoDBRepository();
        }

        /// <summary>
        /// Initialize a new instance of the Dynamic Entity class
        /// </summary>
        public void InitializeEntity()
        {
            this.entity = new DynamicEntity();
        }

        /// <summary>
        /// Commit Entity to repository
        /// </summary>
        /// <param name="collectionName"></param>
        public void CommitEntity(string collectionName)
        {
            this.repository.CreateEntity<DynamicEntity>(collectionName, entity);
        }

        /// <summary>
        /// Clear entity data
        /// </summary>
        public void ClearEntity()
        {
            this.entity = null;
        }
    }
}
