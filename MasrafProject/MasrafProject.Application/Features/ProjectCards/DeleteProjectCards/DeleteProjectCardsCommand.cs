using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.ProjectCards.DeleteProjectCards;

public sealed record DeleteProjectCardCommand(Guid Id) : IRequest<Result<string>>;
