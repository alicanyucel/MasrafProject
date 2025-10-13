using MasrafProject.Application.Features.ApprovalStatuses.CreateApprovalStatuses;
using MasrafProject.Application.Features.ApprovalStatuses.DeleteApprovalStatuses;
using MasrafProject.Application.Features.ApprovalStatuses.GetAllApprovalStatuses;
using MasrafProject.Application.Features.ApprovalStatuses.GetByIdApprovalStatuses;
using MasrafProject.Application.Features.ApprovalStatuses.UpdateApprovalStatuses;
using MasrafProject.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MasrafProject.WebAPI.Controllers;

[AllowAnonymous]
public sealed class AprovalStatusesController : ApiController
{
    public AprovalStatusesController(IMediator mediator) : base(mediator)
    {
    }
    [HttpPost]
    public async Task<IActionResult> CreateApprovalStatus(CreateApprovalStatusCommand request,
    CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return response.IsSuccessful? Ok(new { message = "Ekleme işlemi başarılı." })
        : BadRequest(new { message = "Ekleme işlemi başarısız." });
    }
    [HttpPost]
    public async Task<IActionResult> ApprovalStatusGetById(GetApprovalStatusByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> ApprovalStatusDelete(DeleteApprovalStatusCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);
        return NoContent();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllApprovalStatus(GetAllApprovalStatusQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateApprovalStatus(UpdateApprovalStatusCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return response.IsSuccessful ? Ok(new { message = "Güncelleme işlemi başarılı." })
         : BadRequest(new { message = "Güncelleme işlemi başarısız." });
    }
}