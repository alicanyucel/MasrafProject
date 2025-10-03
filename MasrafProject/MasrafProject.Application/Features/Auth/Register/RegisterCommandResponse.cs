namespace MasrafProject.Application.Features.Auth.Register;

public sealed record RegisterCommandResponse(
    Guid UserId,
    string? UserName,
    string? Email
);
