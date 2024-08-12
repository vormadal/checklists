using Contracts;
using Contracts.Requests;
using Domain.Repositories;
using MediatR;

namespace Application.RequestHandlers;

public class CreateChecklistItemHandler : IRequestHandler<CreateChecklistItemRequest, ChecklistItemDto>
{
    public CreateChecklistItemHandler(
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

    public async Task<ChecklistItemDto> Handle(CreateChecklistItemRequest request, CancellationToken cancellationToken)
    {
        var checklist = await ChecklistRepository.FindById(request.ChecklistId, cancellationToken);
        var item = request.Item.Map(checklist);

        checklist.ModifiedOn = DateTime.UtcNow;
        await ChecklistRepository.Update(checklist, cancellationToken);
        await ChecklistItemRepository.Create(item, cancellationToken);

        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return item.Map();

    }
}
