using MasrafProject.WebAPI.Abstractions;
using MediatR;

namespace MasrafProject.WebAPI.Controllers;


public sealed class ExpenseCenterCardsController : ApiController
{
    public ExpenseCenterCardsController(IMediator mediator) : base(mediator)
    {
    }
}
