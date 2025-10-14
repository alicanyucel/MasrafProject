using FluentValidation;

namespace MasrafProject.Application.Features.Expenses.CreateExpense;

public sealed class CreateExpenseCommandValidator : AbstractValidator<CreateExpenseCommand>
{
    public CreateExpenseCommandValidator()
    {
        RuleFor(x => x.MasrafNo)
            .NotEmpty().WithMessage("Masraf numarası boş olamaz.")
            .MaximumLength(50).WithMessage("Masraf numarası en fazla 50 karakter olabilir.");

        RuleFor(x => x.BelgeNo)
            .NotEmpty().WithMessage("Belge numarası boş olamaz.")
            .MaximumLength(50).WithMessage("Belge numarası en fazla 50 karakter olabilir.");

        RuleFor(x => x.Tarih)
            .LessThanOrEqualTo(DateTime.Today).WithMessage("Tarih bugünden ileri olamaz.");

        RuleFor(x => x.ToplamTutar)
            .GreaterThan(0).WithMessage("Toplam tutar sıfırdan büyük olmalıdır.");

        RuleFor(x => x.ToplamKdvTutar)
            .GreaterThanOrEqualTo(0).WithMessage("KDV tutarı negatif olamaz.");

        RuleFor(x => x.GenelToplam)
            .Equal(x => x.ToplamTutar + x.ToplamKdvTutar)
            .WithMessage("Genel toplam, tutar + KDV olmalıdır.");

        RuleFor(x => x.PicturePath)
            .NotEmpty().WithMessage("Belge görsel yolu boş olamaz.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("Kullanıcı ID boş olamaz.");

        RuleFor(x => x.MuhasebeId)
            .NotEmpty().WithMessage("Muhasebe ID boş olamaz.");

        RuleFor(x => x.MuhasebeOnayId)
            .NotEmpty().WithMessage("Muhasebe onay ID boş olamaz.");
    }
}
