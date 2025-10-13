namespace MasrafProject.Application.Dtos;

public sealed record UserDto(
 Guid Id,
 string FirstName,
 string LastName,
 string Email,
 bool IsDeleted,
 IList<string> Roles
);
