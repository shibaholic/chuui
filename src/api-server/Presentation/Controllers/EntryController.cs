using Application.Response;
using Application.UseCaseCommands;
using Application.UseCaseQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Utilities;

namespace Presentation.Controllers;

[ApiController]
[Route("/api/entry")]
public class EntryController : ControllerBase
{
    private readonly IMediator _mediatr;
    public EntryController(IMediator mediatr)
    {
        _mediatr = mediatr;
    }
    
    [HttpGet("/id")]
    public async Task<IActionResult> GetById([FromQuery] EntryIdGetRequest request)
    {
        var response = await _mediatr.Send(request);
        
        return this.ToActionResult(response);
    }

    [HttpGet]
    public async Task<IActionResult> Search([FromQuery] EntryQueryRequest request)
    {
        var response = await _mediatr.Send(request);
        
        return Ok(response);
    }

    public record UploadJMdictPayload(
        IFormFile File
    );

    [HttpPost]
    public async Task<IActionResult> UploadJMdict([FromForm] UploadJMdictPayload payload, CancellationToken cancellationToken)
    {
        using var memoryStream = new MemoryStream();
        await payload.File.CopyToAsync(memoryStream);

        var fileBytes = memoryStream.ToArray();
        var command = new ImportJMdictRequest()
        {
            Content = fileBytes
        };
        
        var response = await _mediatr.Send(command, cancellationToken);
        
        return this.ToActionResult(response);
    }
}