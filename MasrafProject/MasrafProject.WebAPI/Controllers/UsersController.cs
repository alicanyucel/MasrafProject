using MasrafProject.Application.Features.Auth.Roles.SetUserRoles;
using MasrafProject.Application.Features.Users.CreateUsers;
using MasrafProject.Application.Features.Users.DeleteUsers;
using MasrafProject.Application.Features.Users.GetAllUsers;
using MasrafProject.Application.Features.Users.GetByIdUsers;
using MasrafProject.Application.Features.Users.UpdateUsers;
using MasrafProject.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MasrafProject.WebAPI.Controllers;

[Authorize(Roles = "Admin,User,Manager")]
public sealed class UsersController : ApiController
{
    public UsersController(IMediator mediator) : base(mediator)
    {
    }
    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return response.IsSuccessful ? Ok(new { message = "Ekleme işlemi başarılı." })
        : BadRequest(new { message = "Ekleme işlemi başarısız." });
    }


    [HttpPost]
    public async Task<IActionResult> UserGetById(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(result);


    }
    [HttpPost]
    public async Task<IActionResult> UserDelete(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        await _mediator.Send(request, cancellationToken);

        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> GetAllUsers(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
    [HttpPost]
    public async Task<IActionResult> UpdateUser(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return response.IsSuccessful ? Ok(new { message = "Güncelleme işlemi başarılı." })
         : BadRequest(new { message = "Güncelleme işlemi başarısız." });
    }
    [HttpPost("{id}/roles")]
    public async Task<IActionResult> SetUserRoles([FromRoute] Guid id, [FromBody] IList<string> roles, CancellationToken cancellationToken)
    {
        var command = new SetUserRoleCommand(id, roles);
        var result = await _mediator.Send(command, cancellationToken);
        if (result.IsSuccessful)
        {
            return Ok(new { message = result.Data });
        }
        var errorMessage = result.ErrorMessages is { Count: > 0 }
            ? string.Join("; ", result.ErrorMessages)
            : "Rol atama işlemi sırasında bir hata oluştu.";
        return BadRequest(new { message = errorMessage });
    }
}