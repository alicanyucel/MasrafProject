using MediatR;
using TS.Result;

namespace MasrafProject.Application.Features.ProjectCards.CreateProjectCards;

public record CreateProjectCardCommand(string ProjeKodu, string ProjeAdi) : IRequest<Result<string>>;
