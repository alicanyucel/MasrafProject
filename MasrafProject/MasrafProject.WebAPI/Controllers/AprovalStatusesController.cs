using MasrafProject.Application.Features.ApprovalStatuses.CreateApprovalStatuses;
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
}