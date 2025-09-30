using MasrafProject.WebAPI.Abstractions;
using MediatR;

namespace MasrafProject.WebAPI.Controllers;


public sealed class RolesController : ApiController
{
    public RolesController(IMediator mediator) : base(mediator)
    {
    }
}
