using Contracts;
using Domain;

namespace Application;

internal static class Mapper
{
    public static Checklist Map(this CreateChecklistDto dto)
    {
        var now = DateTime.UtcNow;
        var checklist = new Checklist
        {
            Title = dto.Title,
            Type = dto.Type.Map(),
            CreatedOn = now,
            ModifiedOn = now
        };
        return checklist;
    }

    public static Checklist Map(this UpdateChecklistDto source, Checklist? target = null)
    {
        var result = target ?? new Checklist();

        result.Title = source.Title;
        result.ModifiedOn = DateTime.UtcNow;
        return result;
    }

    public static ChecklistDto Map(this Checklist checklist)
    {
        return new ChecklistDto
        {
            Id = checklist.Id,
            CreatedOn = checklist.CreatedOn,
            ModifiedOn = checklist.ModifiedOn,
            Type = checklist.Type.Map(),
            Title = checklist.Title,
        };
    }

    public static ChecklistDetailsDto MapDetails(this Checklist checklist)
    {
        return new ChecklistDetailsDto
        {
            Id = checklist.Id,
            CreatedOn = checklist.CreatedOn,
            ModifiedOn = checklist.ModifiedOn,
            Type = checklist.Type.Map(),
            Title = checklist.Title,
            Items = checklist.Items?.Select(i => i.Map()).ToList() ?? new List<ChecklistItemDto>()
        };
    }

    public static ChecklistItem Map(this CreateChecklistItemDto source, Checklist parent)
    {
        return new ChecklistItem
        {
            Checklist = parent,
            ChecklistId = parent.Id,
            Title = source.Title,
            IsComplete = source.IsComplete,
            Order = source.Order
        };
    }

    public static ChecklistItem Map(this UpdateChecklistItemDto source, ChecklistItem? target = null)
    {
        var result = target ?? new ChecklistItem();

        result.Title = source.Title;
        result.IsComplete = source.IsComplete;
        result.Order = source.Order;
        return result;
    }

    public static ChecklistItemDto Map(this ChecklistItem item)
    {
        return new ChecklistItemDto
        {
            ChecklistId = item.ChecklistId,
            Title = item.Title,
            Id = item.Id,
            Order = item.Order,
            IsComplete = item.IsComplete
        };
    }

    public static Domain.ChecklistType? Map(this Contracts.ChecklistType? type)
    {
        return type?.Map() ?? null;
    }

    public static Domain.ChecklistType Map(this Contracts.ChecklistType type)
    {
        return type switch
        {
            Contracts.ChecklistType.Template => Domain.ChecklistType.Template,
            Contracts.ChecklistType.Checklist => Domain.ChecklistType.Checklist,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null),
        };
    }

    public static Contracts.ChecklistType Map(this Domain.ChecklistType type)
    {
        return type switch
        {
            Domain.ChecklistType.Template => Contracts.ChecklistType.Template,
            Domain.ChecklistType.Checklist => Contracts.ChecklistType.Checklist,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null),
        };
    }

    public static Contracts.ChecklistType? Map(this Domain.ChecklistType? type)
    {
        return type?.Map() ?? null;
    }
}
