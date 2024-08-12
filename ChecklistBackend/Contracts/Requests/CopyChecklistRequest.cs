using MediatR;

namespace Contracts.Requests;

public record CopyChecklistRequest(int Id, CopyChecklistDto TargetOverrides) : IRequest<ChecklistDto>
{
}
