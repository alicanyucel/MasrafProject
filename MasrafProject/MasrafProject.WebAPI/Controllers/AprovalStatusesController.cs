using MasrafProject.WebAPI.Abstractions;
using MediatR;

namespace MasrafProject.WebAPI.Controllers;


public sealed class AprovalStatusesController : ApiController
{
    public AprovalStatusesController(IMediator mediator) : base(mediator)
    {
    }
}
