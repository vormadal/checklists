using Contracts;
using Contracts.Queries;
using Domain.Repositories;
using MediatR;
using System.Threading;

namespace Application.QueryHandlers;

internal class GetChecklistsQueryHandler : IRequestHandler<GetChecklistsQuery, IEnumerable<ChecklistDto>>
{
    public GetChecklistsQueryHandler(IChecklistRepository checklistRepository)
    {
        ChecklistRepository = checklistRepository;
    }

    public IChecklistRepository ChecklistRepository { get; }

    public async Task<IEnumerable<ChecklistDto>> Handle(GetChecklistsQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Domain.Checklist> query = CreateQuery(request.Type, cancellationToken);
        query = FilterByCompletion(query, request.IsComplete);
        query = AddSorting(query);
        query = AddPagination(query, request.Pagination);

        return query.Select(x => x.Map());
    }

    private IQueryable<Domain.Checklist> AddSorting(IQueryable<Domain.Checklist> query)
    {
        return query.OrderBy(x => x.ModifiedOn);
    }

    private IQueryable<Domain.Checklist> AddPagination(IQueryable<Domain.Checklist> query, Pagination pagination)
    {
        return query.Skip(pagination.Page * pagination.Size).Take(pagination.Size);
    }

    private IQueryable<Domain.Checklist> CreateQuery(ChecklistType? type, CancellationToken cancellationToken)
    {
        if (type != null)
        {
            return ChecklistRepository.FindByCondition(x => x.Type == type.Map());
        }
        return ChecklistRepository.FindAll(cancellationToken);
    }

    private IQueryable<Domain.Checklist> FilterByCompletion(IQueryable<Domain.Checklist> query, bool? isComplete)
    {
        if (isComplete == null)
        {
            return query;
        }

        if (isComplete.Value)
        {
            return query.Where(x => x.Items.All(item => item.IsComplete));
        }
        else
        {
            return query.Where(x => x.Items.Any(item => !item.IsComplete));
        }
    }
}
