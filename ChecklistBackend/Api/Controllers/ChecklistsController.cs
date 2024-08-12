using Contracts;
using Contracts.Queries;
using Contracts.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Checklists.Controllers;

[ApiController]
[Route("[controller]")]
public class ChecklistsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ChecklistsController> _logger;

    public ChecklistsController(
        IMediator mediator,
        ILogger<ChecklistsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet(Name = "/")]
    [ProducesResponseType<IEnumerable<ChecklistDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetChecklists(
        [FromQuery(Name = "type")] ChecklistType? type,
        [FromQuery(Name = "isComplete")] bool? isComplete,
        [FromQuery(Name = "PageSize")] int pageSize = 10,
        [FromQuery(Name = "PageNo")] int pageNumber = 0)
    {
        return Ok(await _mediator.Send(new GetChecklistsQuery(type, isComplete, new Pagination(pageNumber, pageSize))));
    }

    [HttpGet("{id}")]
    [ProducesResponseType<ChecklistDetailsDto>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetChecklistById(int id)
    {
        return Ok(await _mediator.Send(new GetChecklistByIdQuery(id)));
    }

    [HttpPost]
    [ProducesResponseType<ChecklistDto>(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateChecklist([FromBody] CreateChecklistDto checklist)
    {
        var result = await _mediator.Send(new CreateChecklistRequest(checklist));
        return Created($"/checklists/{result.Id}", result);
    }

    [HttpPut("{id}")]
    [ProducesResponseType<ChecklistDto>(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateChecklist(int id, [FromBody] UpdateChecklistDto checklist)
    {
        return Ok(await _mediator.Send(new UpdateChecklistRequest(id, checklist)));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteChecklist(int id)
    {
        await _mediator.Send(new DeleteChecklistRequest(id));
        return Ok();
    }

    [HttpPost("{checklistId}/items")]
    [ProducesResponseType<ChecklistItemDto>(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateChecklistItem(int checklistId, [FromBody] CreateChecklistItemDto item)
    {
        var result = await _mediator.Send(new CreateChecklistItemRequest(checklistId, item));
        return Created($"checklists/{result.ChecklistId}/items/{result.Id}", result);
    }

    [HttpPut("{checklistId}/items/{id}")]
    [ProducesResponseType<ChecklistItemDto>(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateChecklistItem(int checklistId, int id, [FromBody] UpdateChecklistItemDto item)
    {
        return Ok(await _mediator.Send(new UpdateChecklistItemRequest(checklistId, id, item)));
    }

    [HttpDelete("{checklistId}/items/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteChecklistItem(int checklistId, int id)
    {
        await _mediator.Send(new DeleteChecklistItemRequest(checklistId, id));
        return Ok();
    }

    [HttpPost("{id}/copy")]
    [ProducesResponseType<ChecklistDto>(StatusCodes.Status201Created)]
    public async Task<IActionResult> CopyChecklist(int id, [FromBody] CopyChecklistDto request)
    {
        var result = await _mediator.Send(new CopyChecklistRequest(id, request));
        return Created($"/checklists/{result.Id}", result);
    }
}
