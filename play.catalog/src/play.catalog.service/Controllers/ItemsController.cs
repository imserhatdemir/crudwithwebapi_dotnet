using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using play.catalog.service.Dtos;
using play.catalog.service.Entities;
using play.catalog.service.Repositories;

namespace play.catalog.service.Controllers
{
    // https://localhost:5001/items
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly ItemsRepository itemsRepository = new();

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetAsync()
        {
            var items = (await itemsRepository.GetAllAsync()).Select(item => item.AsDto());
            return items;
        }

        //Get /items/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetByIdAsync(Guid id)
        {
            var item = await itemsRepository.GetAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> PostAsync(CreateItemDto createItemDto)
        {
            var item = new Item
            {
                Name = createItemDto.Name,
                Description = createItemDto.Description,
                Price = createItemDto.Price,
                CreateDate = DateTimeOffset.UtcNow
            };
            await itemsRepository.CreateAsync(item);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = item.Id }, item);
        }

        //Put /items/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateItemDto updateItemDto)
        {
            var existingitem = await itemsRepository.GetAsync(id);

            if (existingitem == null)
            {
                return NotFound();
            }
            existingitem.Name = updateItemDto.Name;
            existingitem.Description = updateItemDto.Description;
            existingitem.Price = updateItemDto.Price;

            await itemsRepository.UpdateAsync(existingitem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var item = await itemsRepository.GetAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            await itemsRepository.RemoveAsync(item.Id);
            return NoContent();
        }
    }
}