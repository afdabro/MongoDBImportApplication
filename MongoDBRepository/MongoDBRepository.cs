// Author: Andrew F. Dabrowski
// Date: 3/20/2014
// copyright© 2014

using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Common.Repository.MongoDB
{
    /// <summary>
    /// MongoDB Repository Implementation
    /// </summary>
    public class MongoDBRepository
    {
        /// <summary>
        /// Gets or sets the MongoDB Client
        /// </summary>
        public MongoClient client { get; set; }

        /// <summary>
        /// Gets or sets the MongoDB server
        /// </summary>
        public MongoServer server { get; set; }

        /// <summary>
        /// Gets or sets the MongoDB database
        /// </summary>
        public MongoDatabase database { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoDBRepository"/> class.
        /// </summary>
        public MongoDBRepository()
        {
            client = new MongoClient(ConfigurationManager.AppSettings["connectionString"]);
            server = client.GetServer();
            database = server.GetDatabase(ConfigurationManager.AppSettings["databaseString"]);
        }

        /// <summary>
        /// Get all Entities within the collection
        /// </summary>
        /// <typeparam name="T">Type of Entity</typeparam>
        /// <param name="collectionName">Name of collection</param>
        /// <returns></returns>
        public IEnumerable<T> GetAllEntities<T>(string collectionName)
        {
            var collection = database.GetCollection<T>(collectionName);
            return collection.FindAll().AsEnumerable<T>();
        }

        /// <summary>
        /// Create new Entity
        /// </summary>
        /// <typeparam name="T">Type of entity to create</typeparam>
        /// <param name="collectionName">Collection name</param>
        /// <param name="entity">Entity to create</param>
        /// <returns>Created Entity</returns>
        public T CreateEntity<T>(string collectionName, T entity) where T : DynamicEntity
        {

            var collection = database.GetCollection<T>(collectionName);
            var result = collection.Insert(entity);

            if (result.Ok)
                return entity;
            else
            {
                throw new Exception(result.ErrorMessage);
            }
        }
    }
}
