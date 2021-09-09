using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Collection.Repositories;
using Microsoft.Extensions.Logging;

namespace Collection.Controllers {

    [ApiController]
    [Route("items")] 
    public class ItemsController : ControllerBase {
        private readonly IItems repository;
        private readonly ILogger<ItemsController> logger;

        public ItemsController(IItems repository, ILogger<ItemsController> logger) {
            this.repository = repository;
            this.logger = logger;
        }

        // GET /items
        [HttpGet]
        public async Task<IEnumerable<Dtos.Item>> GetItemsAsync() {

            var items = (await repository.GetItemsAsync()).Select( item => item.asDto());
            logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrieved {items.Count()} items");

            return items;
        }
        // GET /items/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Dtos.Item>> GetItemAsync(Guid id) {
            
            var item = await repository.GetItemAsync(id);

            if (item is null) {
                return NotFound();
            }

            return item.asDto();
        }

        // POST /items
        [HttpPost]
        public async Task<ActionResult<Dtos.Item>> CreateItemAsync(Dtos.CreateItem itemDto) {

            Entities.Item item = new() {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await repository.CreateItemAsync(item);

            return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id}, item.asDto());
        }
        // PUT /items/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemAsync(Guid id, Dtos.UpdateItem itemDto) {

            var existingItem = await repository.GetItemAsync(id);

            if (existingItem is null) {
                return NotFound();
            }

            Entities.Item updatedItem = existingItem with {
                Name = itemDto.Name,
                Price = itemDto.Price
            };

            await repository.UpdateItemAsync(updatedItem);

            return NoContent();
        }

        // DELETE /items/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItemAsync(Guid id) {
            var existingItem = await repository.GetItemAsync(id);

            if (existingItem is null) {
                return NotFound();
            }

            await repository.DeleteItemAsync(id);

            return NoContent();
        }
    }
}