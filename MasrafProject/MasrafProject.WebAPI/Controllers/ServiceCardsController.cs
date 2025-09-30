using MasrafProject.WebAPI.Abstractions;
using MediatR;

namespace MasrafProject.WebAPI.Controllers;

public sealed class ServiceCardsController : ApiController
{
    public ServiceCardsController(IMediator mediator) : base(mediator)
    {
    }
}
