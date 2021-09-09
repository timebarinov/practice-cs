using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Collection.Entities;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Collection.Repositories {
    public class MongoDbItems : IItems
    {

        private const string DATABASE_NAME = "collection";

        private const string COLLECTION_NAME = "items";

        private readonly IMongoCollection<Item> itemsCollection;

        private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;

        public MongoDbItems(IMongoClient mongoClient) {
            IMongoDatabase database = mongoClient.GetDatabase(DATABASE_NAME);
            itemsCollection = database.GetCollection<Item>(COLLECTION_NAME);
        }

        public async Task CreateItemAsync(Item item)
        {
            await itemsCollection.InsertOneAsync(item);
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var filter = filterBuilder.Eq(existingItem => existingItem.Id, id);
            await itemsCollection.DeleteOneAsync(filter);
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            var filter = filterBuilder.Eq(existingItem => existingItem.Id, id);
            return await itemsCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await itemsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateItemAsync(Item item)
        {
            var filter = filterBuilder.Eq(existingItem => existingItem.Id, item.Id);
            await itemsCollection.ReplaceOneAsync(filter, item);
        }
    }
}