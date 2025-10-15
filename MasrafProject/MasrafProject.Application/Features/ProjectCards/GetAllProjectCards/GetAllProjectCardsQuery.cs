using MasrafProject.Domain.Entities;
using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.ProjectCards.GetAllProjectCards;

public sealed record GetAllProjectCardQuery : IRequest<Result<List<ProjectCard>>>;
