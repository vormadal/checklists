using Contracts;
using Contracts.Requests;
using Domain.Repositories;
using MediatR;

namespace Application.RequestHandlers;

public class CreateChecklistHandler : IRequestHandler<CreateChecklistRequest, ChecklistDto>
{
    public CreateChecklistHandler(
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

    public async Task<ChecklistDto> Handle(CreateChecklistRequest request, CancellationToken cancellationToken)
    {
        var checklist = request.Checklist.Map();
        await ChecklistRepository.Create(checklist, cancellationToken);

        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return checklist.Map();

    }
}
