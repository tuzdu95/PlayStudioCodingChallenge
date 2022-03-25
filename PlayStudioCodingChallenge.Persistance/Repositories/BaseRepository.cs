using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PlayStudioCodingChallenge.Application.AbstractRepository;
using PlayStudioCodingChallenge.Persistence.DbConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayStudioCodingChallenge.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly IMongoCollection<T> _entityCollection;
        public BaseRepository(IOptions<MongoConfiguration> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _entityCollection = mongoDatabase.GetCollection<T>(typeof(T).Name);
        }
        public async Task<T> AddAsync(T entity)
        {
            await _entityCollection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> AddManyAsync(IEnumerable<T> entities)
        {
            await _entityCollection.InsertManyAsync(entities);
            return entities;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var result = await _entityCollection.Find(Builders<T>.Filter.Empty).ToListAsync();
            return result;
        }

        public async Task<T> GetByIdAsync(string id)
        {
            var result = await _entityCollection.Find(FilterById(id)).FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> RemoveAsync(string id)
        {
            var result = await _entityCollection.DeleteOneAsync(FilterById(id));
            return result.IsAcknowledged;
        }

        public async Task<T> UpdateAsync(string id, T entity)
        {
            await _entityCollection.ReplaceOneAsync(FilterById(id), entity);
            return entity;
        }
        public async Task<bool> CheckDocumentExists()
        {
            var result = await _entityCollection.Find(Builders<T>.Filter.Empty).ToListAsync();
            return result.Any();
        }
        private static FilterDefinition<T> FilterById(string key)
        {
            return Builders<T>.Filter.Eq("Id", key);
        }

    }
}
