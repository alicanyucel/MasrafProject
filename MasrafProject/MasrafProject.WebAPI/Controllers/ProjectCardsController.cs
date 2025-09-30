using MasrafProject.WebAPI.Abstractions;
using MediatR;

namespace MasrafProject.WebAPI.Controllers;


public class ProjectCardsController : ApiController
{
    public ProjectCardsController(IMediator mediator) : base(mediator)
    {
    }
}
