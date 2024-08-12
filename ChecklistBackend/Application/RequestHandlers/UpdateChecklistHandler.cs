using Contracts;
using Contracts.Requests;
using Domain.Repositories;
using MediatR;

namespace Application.RequestHandlers;

public class UpdateChecklistHandler : IRequestHandler<UpdateChecklistRequest, ChecklistDto>
{
    public UpdateChecklistHandler(
        IUnitOfWork unitOfWork,
        IChecklistRepository checklistRepository)
    {
        UnitOfWork = unitOfWork;
        ChecklistRepository = checklistRepository;
    }

    public IUnitOfWork UnitOfWork { get; }
    public IChecklistRepository ChecklistRepository { get; }

    public async Task<ChecklistDto> Handle(UpdateChecklistRequest request, CancellationToken cancellationToken)
    {
        var checklist = await ChecklistRepository.FindById(request.Id);

        var update = request.Checklist.Map(checklist);

        await ChecklistRepository.Update(update, cancellationToken);

        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return update.Map();

    }
}
