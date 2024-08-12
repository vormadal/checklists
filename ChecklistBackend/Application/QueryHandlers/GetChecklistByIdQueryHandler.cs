using Contracts;
using Contracts.Queries;
using Domain.Repositories;
using MediatR;

namespace Application.QueryHandlers;

internal class GetChecklistByIdQueryHandler : IRequestHandler<GetChecklistByIdQuery, ChecklistDetailsDto>
{
    public GetChecklistByIdQueryHandler(
        IChecklistRepository checklistRepository,
        IChecklistItemRepository checklistItemRepository)
    {
        ChecklistRepository = checklistRepository;
        ChecklistItemRepository = checklistItemRepository;
    }

    public IChecklistRepository ChecklistRepository { get; }
    public IChecklistItemRepository ChecklistItemRepository { get; }

    public async Task<ChecklistDetailsDto> Handle(GetChecklistByIdQuery request, CancellationToken cancellationToken)
    {

        var checklist = await ChecklistRepository.FindById(request.Id, cancellationToken);
        var response = checklist.MapDetails();
        var items = ChecklistItemRepository.FindByCondition(x => x.ChecklistId == request.Id)
            .OrderBy(x => x.IsComplete)
            .ThenBy(x => x.Order)
            .ToList();
        response.Items = items.Select(x => x.Map()).ToList();
        return response;
    }
}
