using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Collection.Entities;

namespace Collection.Repositories {
    public interface IItems
    {
        Task<Item> GetItemAsync(Guid id);
        Task<IEnumerable<Item>> GetItemsAsync();

        Task CreateItemAsync(Item item);

        Task UpdateItemAsync(Item item);

        Task DeleteItemAsync(Guid id);
    }
}