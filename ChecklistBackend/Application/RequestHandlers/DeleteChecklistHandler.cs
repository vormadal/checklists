using Contracts.Requests;
using Domain.Repositories;
using MediatR;

namespace Application.RequestHandlers;

public class DeleteChecklistHandler : IRequestHandler<DeleteChecklistRequest>
{
    public DeleteChecklistHandler(
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

    async Task IRequestHandler<DeleteChecklistRequest>.Handle(DeleteChecklistRequest request, CancellationToken cancellationToken)
    {
        var checklist = await ChecklistRepository.FindById(request.Id, cancellationToken);
        var items = ChecklistItemRepository.FindByCondition(x => x.ChecklistId == request.Id).ToList();

        await ChecklistItemRepository.Delete(items, cancellationToken);
        await ChecklistRepository.Delete(checklist, cancellationToken);
        await UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
