using MediatR;

namespace Contracts.Requests;

public record DeleteChecklistItemRequest(int ChecklistId, int Id) : IRequest
{
}
