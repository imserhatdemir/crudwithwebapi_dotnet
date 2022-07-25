using System;

namespace play.catalog.service.Dtos
{
    public record ItemDto(Guid Id, string Name, string Description, decimal Price, DateTimeOffset CreatedDate);

    public record CreateItemDto(string Name, string Description, Decimal Price);

    public record UpdateItemDto(string Name, string Description, Decimal Price);
}