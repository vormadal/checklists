using MediatR;

namespace Contracts.Requests;

public record UpdateChecklistItemRequest(int ChecklistId, int Id, UpdateChecklistItemDto Item) : IRequest<ChecklistItemDto>
{
}
