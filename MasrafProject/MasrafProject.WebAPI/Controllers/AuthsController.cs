using MasrafProject.Application.Features.Auth.AdminApprovels;
using MasrafProject.Application.Features.Auth.Login;
using MasrafProject.Application.Features.Auth.Register;
using MasrafProject.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MasrafProject.WebAPI.Controllers;

[AllowAnonymous]
public sealed class AuthsController : ApiController
{
    public AuthsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

        return Ok(new
        {
            message = "Çıkış başarılı."
        });
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        if (response.IsSuccessful)
        {
            return Ok(new
            {
                message = "Kayıt başarılı.",
                data = response.Data
            });
        }

        return BadRequest(new
        {
            message = "Kayıt başarısız.",
            errors = response.ErrorMessages
        });
    }
    [HttpPost]
    public async Task<IActionResult> ApproveAsUser([FromBody] ApproveUserAsStandardCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        if (response.IsSuccessful)
        {
            return Ok(new
            {
                message = "Kullanıcı başarıyla onaylandı."
            });
        }

        return BadRequest(new
        {
            message = "Onaylama başarısız.",
            errors = response.ErrorMessages
        });
    }
}
