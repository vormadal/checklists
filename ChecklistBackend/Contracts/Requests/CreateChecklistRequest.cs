using MediatR;

namespace Contracts.Requests;

public record CreateChecklistRequest(CreateChecklistDto Checklist) : IRequest<ChecklistDto>
{
}
