using MediatR;

namespace Contracts.Queries;

public record GetChecklistsQuery(ChecklistType? Type, bool? IsComplete, Pagination Pagination) : IRequest<IEnumerable<ChecklistDto>>
{
}
