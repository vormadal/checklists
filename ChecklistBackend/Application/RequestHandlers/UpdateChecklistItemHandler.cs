using Contracts;
using Contracts.Requests;
using Domain.Repositories;
using MediatR;

namespace Application.RequestHandlers;

public class UpdateChecklistItemHandler : IRequestHandler<UpdateChecklistItemRequest, ChecklistItemDto>
{
    public UpdateChecklistItemHandler(
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

    public async Task<ChecklistItemDto> Handle(UpdateChecklistItemRequest request, CancellationToken cancellationToken)
    {
        var item = await ChecklistItemRepository.FindById(request.Id, cancellationToken);
        var checklist = await ChecklistRepository.FindById(request.ChecklistId, cancellationToken);

        var update = request.Item.Map(item);

        if (request.ChecklistId != item.ChecklistId)
        {
            throw new Exception("Cannot change an item of a different checklist.");
        }

        checklist.ModifiedOn = DateTime.UtcNow;
        await ChecklistRepository.Update(checklist, cancellationToken);
        await ChecklistItemRepository.Update(update, cancellationToken);

        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return update.Map();

    }
}
