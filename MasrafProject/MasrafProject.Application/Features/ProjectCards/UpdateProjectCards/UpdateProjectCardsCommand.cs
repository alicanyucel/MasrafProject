using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.ProjectCards.UpdateProjectCards;

public record UpdateProjectCardCommand(Guid Id,string ProjeKodu, string ProjeAdi) : IRequest<Result<string>>;
