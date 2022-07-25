using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using play.catalog.service.Dtos;

namespace play.catalog.service.Controllers
{
    // https://localhost:5001/items
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private static readonly List<ItemDto> items = new()
        {
            new ItemDto(Guid.NewGuid(),"Potion","Reastores a small amount of Dell",5,DateTimeOffset.UtcNow),

            new ItemDto(Guid.NewGuid(),"Antidote","Cures Poison",7,DateTimeOffset.UtcNow),

            new ItemDto(Guid.NewGuid(),"Bronze","Deals a small amount of damage",20,DateTimeOffset.UtcNow)
        };

        [HttpGet]
        public IEnumerable<ItemDto> Get()
        {
            return items;
        }

        //Get /items/{id}
        [HttpGet("{id}")]
        public ItemDto GetById(Guid id)
        {
            var item = items.Where(item => item.Id == id).SingleOrDefault();
            return item;
        }

        [HttpPost]
        public ActionResult<ItemDto> Post(CreateItemDto createItemDto)
        {
            var item = new ItemDto(Guid.NewGuid(), createItemDto.Name, createItemDto.Description, createItemDto.Price, DateTimeOffset.UtcNow);
            items.Add(item);

            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        //Put /items/{id}
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, UpdateItemDto updateItemDto)
        {
            var existingitem = items.Where(item => item.Id == id).SingleOrDefault();
            var updatedItem = existingitem with
            {
                Name = updateItemDto.Name,
                Description = updateItemDto.Description,
                Price = updateItemDto.Price
            };

            var index = items.FindIndex(existingitem => existingitem.Id == id);
            items[index] = updatedItem;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var index = items.FindIndex(existingitem => existingitem.Id == id);
            items.RemoveAt(index);
            return NoContent();
        }
    }
}