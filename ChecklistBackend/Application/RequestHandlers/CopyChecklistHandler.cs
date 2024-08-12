using Contracts;
using Contracts.Requests;
using Domain;
using Domain.Repositories;
using MediatR;

namespace Application.RequestHandlers;

public class CopyChecklistHandler : IRequestHandler<CopyChecklistRequest, ChecklistDto>
{
    public CopyChecklistHandler(
        IUnitOfWork unitOfWork,
        IChecklistRepository checklistRepository,
        IChecklistItemRepository checklistItemRepository)
    {
        UnitOfWork = unitOfWork;
        ChecklistRepository = checklistRepository;
        ChecklistItemRepository = checklistItemRepository;
    }

    public IUnitOfWork UnitOfWork { get; }
    public IChecklistRepository ChecklistRepository { get; }
    public IChecklistItemRepository ChecklistItemRepository { get; }

    public async Task<ChecklistDto> Handle(CopyChecklistRequest request, CancellationToken cancellationToken)
    {
        var checklist = await ChecklistRepository.FindById(request.Id, cancellationToken);
        var items = ChecklistItemRepository.FindByCondition(x => x.ChecklistId == request.Id).ToList();

        var copy = new Checklist
        {
            Title = string.IsNullOrWhiteSpace(request.TargetOverrides.NewTitle) ? checklist.Title : request.TargetOverrides.NewTitle,
            Type = request.TargetOverrides.NewType.Map() ?? checklist.Type,
        };

        if (checklist.Type == Domain.ChecklistType.Template)
        {
            copy.Template = checklist;
        }



        var created = await ChecklistRepository.Create(copy, cancellationToken);

        var copyItems = items.Select(x => new ChecklistItem
        {
            Title = x.Title,
            Checklist = created,
            Order = x.Order,
            CopiedFromId = x.Id,
        }).ToList();
        var createdItems = await ChecklistItemRepository.Create(copyItems, cancellationToken);

        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return copy.Map();

    }
}
