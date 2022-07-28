using play.catalog.service.Dtos;
using play.catalog.service.Entities;

namespace play.catalog.service
{
    public static class Extensions
    {
        public static ItemDto AsDto(this Item item)
        {
            return new ItemDto(item.Id, item.Name, item.Description, item.Price, item.CreateDate);

        }
    }
}