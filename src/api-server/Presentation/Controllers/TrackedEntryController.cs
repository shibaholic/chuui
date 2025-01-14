using Application.Mappings.EntityDtos.Tracking;
using Application.Services;
using Domain.Entities.Tracking;
using Microsoft.AspNetCore.Mvc;
using Application.Response;
using Application.UseCaseQueries;
using MediatR;
using Presentation.Utilities;

namespace Presentation.Controllers;

[ApiController]
public class TrackedEntryController: BaseApiController
{
    private readonly ICrudService<TrackedEntry, TrackedEntryDto> _crudService;
    private readonly IMediator _mediator;

    public TrackedEntryController(ICrudService<TrackedEntry, TrackedEntryDto> crudService, IMediator mediator)
    {
        _crudService = crudService;
        _mediator = mediator;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var response = await _crudService.GetAllAsync();

        return this.ToActionResult(response);
    }

    [HttpGet("id")]
    public async Task<IActionResult> GetById([FromQuery] string ent_seq, CancellationToken cancellationToken)
    {
        // user id thing
        var request = new TrackedEntryIdGetRequest
        {
            ent_seq = ent_seq,
            UserId = new Guid("faeb2480-fbdc-4921-868b-83bd93324099")
        };
        
        var response = await _mediator.Send(request, cancellationToken);
        
        return this.ToActionResult(response);
    }
}