using MasrafProject.Domain.Entities;
using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.ProjectCards.GetByIdProjectCards;

public sealed record GetByIdProjectCardQuery(Guid Id) : IRequest<Result<ProjectCard>>;
