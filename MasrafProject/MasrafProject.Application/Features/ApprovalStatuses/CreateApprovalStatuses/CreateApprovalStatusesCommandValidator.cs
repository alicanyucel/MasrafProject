using FluentValidation;

namespace MasrafProject.Application.Features.ApprovalStatuses.CreateApprovalStatuses;

public sealed class CreateApprovalStatusCommandValidator : AbstractValidator<CreateApprovalStatusCommand>
{
    public CreateApprovalStatusCommandValidator()
    {
        RuleFor(x => x.Onay).NotNull().WithMessage("Onay bilgisi boş olamaz.");
    }
}
