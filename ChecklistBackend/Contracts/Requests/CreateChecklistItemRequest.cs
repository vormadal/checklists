using MediatR;

namespace Contracts.Requests;

public record CreateChecklistItemRequest(int ChecklistId, CreateChecklistItemDto Item) : IRequest<ChecklistItemDto>
{
}
