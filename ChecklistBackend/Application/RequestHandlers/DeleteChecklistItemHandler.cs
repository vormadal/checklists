using Contracts.Requests;
using Domain.Repositories;
using MediatR;

namespace Application.RequestHandlers;

public class DeleteChecklistItemHandler : IRequestHandler<DeleteChecklistItemRequest>
{
    public DeleteChecklistItemHandler(
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

    async Task IRequestHandler<DeleteChecklistItemRequest>.Handle(DeleteChecklistItemRequest request, CancellationToken cancellationToken)
    {
        var item = await ChecklistItemRepository.FindById(request.Id, cancellationToken);
        var checklist = await ChecklistRepository.FindById(item.ChecklistId, cancellationToken);

        checklist.ModifiedOn = DateTime.UtcNow;
        await ChecklistRepository.Update(checklist, cancellationToken);
        await ChecklistItemRepository.Delete(item, cancellationToken);
        await UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
