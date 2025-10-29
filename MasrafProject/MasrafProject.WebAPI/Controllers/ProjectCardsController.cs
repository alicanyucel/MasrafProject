using MasrafProject.Application.Features.ProjectCards.CreateProjectCards;
using MasrafProject.Application.Features.ProjectCards.DeleteProjectCards;
using MasrafProject.Application.Features.ProjectCards.GetAllProjectCards;
using MasrafProject.Application.Features.ProjectCards.GetByIdProjectCards;
using MasrafProject.Application.Features.ProjectCards.UpdateProjectCards;
using MasrafProject.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MasrafProject.WebAPI.Controllers;
[Authorize(Roles = "Admin,Manager")]
public class ProjectCardsController : ApiController
{
    public ProjectCardsController(IMediator mediator) : base(mediator)
    {
    }
    [HttpPost]
    public async Task<IActionResult> CreateProjectCard(CreateProjectCardCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return response.IsSuccessful ? Ok(new { message = "Ekleme işlemi başarılı." })
        : BadRequest(new { message = "Ekleme işlemi başarısız." });
    }
    [HttpPost]
    public async Task<IActionResult> ProjectCardGetById(GetByIdProjectCardQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> ProjectCardDelete(DeleteProjectCardCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> GetAllProejctCard(GetAllProjectCardQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
    [HttpPost]
    public async Task<IActionResult> UpdateProjectCard(UpdateProjectCardCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return response.IsSuccessful ? Ok(new { message = "Güncelleme işlemi başarılı." })
        : BadRequest(new { message = "Güncelleme işlemi başarısız." });
    }
}
