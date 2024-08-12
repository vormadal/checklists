using MediatR;

namespace Contracts.Requests;

public record UpdateChecklistRequest(int Id, UpdateChecklistDto Checklist) : IRequest<ChecklistDto>
{
}
