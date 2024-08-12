using MediatR;

namespace Contracts.Queries;

public record GetChecklistByIdQuery(int Id) : IRequest<ChecklistDetailsDto>
{
}
