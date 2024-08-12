using MediatR;

namespace Contracts.Requests;

public record DeleteChecklistRequest(int Id) : IRequest
{
}
