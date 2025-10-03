using MasrafProject.Application.Features.Auth.Roles.GetRole;
using MasrafProject.Application.Features.Auth.Roles.SetRole;
using MasrafProject.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MasrafProject.WebAPI.Controllers;

[AllowAnonymous]
public class RolesController : ApiController
{
    public RolesController(IMediator mediator) : base(mediator)
    {
    }
    [HttpPost]
    public async Task<IActionResult> CreateRole(SetRoleCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    [HttpPost]
    public async Task<IActionResult> GetAllRoles(GetAllRoleQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}