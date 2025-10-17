using FluentValidation;

namespace MasrafProject.Application.Features.ExpenseDetails.CreateExpenseDetails;

public class CreateExpenseDetailValidator : AbstractValidator<CreateExpenseDetailCommand>
{
    public CreateExpenseDetailValidator()
    {
        RuleFor(x => x.MasrafId).NotEmpty();
        RuleFor(x => x.Tarih)
            .NotEmpty()
            .LessThanOrEqualTo(DateTime.Today).WithMessage("Tarih bugünden ileri olamaz.");
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.ProjeId).NotEmpty();
        RuleFor(x => x.HizmetId).NotEmpty();
        RuleFor(x => x.MasrafMerkeziId).NotEmpty();
        RuleFor(x => x.ManagerUserId).NotEmpty();
        RuleFor(x => x.AccountUserId).NotEmpty();

        RuleFor(x => x.Miktar)
            .GreaterThan(0).WithMessage("Miktar sıfırdan büyük olmalıdır.");

        RuleFor(x => x.BirimFiyat)
            .GreaterThan(0).WithMessage("Birim fiyat sıfırdan büyük olmalıdır.");

        RuleFor(x => x.KdvOran)
            .InclusiveBetween(0, 100).WithMessage("KDV oranı 0 ile 100 arasında olmalıdır.");

        RuleFor(x => x.SatirAciklama)
            .NotEmpty().WithMessage("Satır açıklaması boş olamaz.")
            .MaximumLength(300).WithMessage("Satır açıklaması en fazla 300 karakter olabilir.");
    }
}

