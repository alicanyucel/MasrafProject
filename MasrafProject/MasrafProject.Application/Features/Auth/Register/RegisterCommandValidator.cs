using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace MasrafProject.Application.Features.Auth.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.EmailOrUserName)
            .NotEmpty().WithMessage("E-posta veya kullanıcı adı boş olamaz.")
            .Must(BeValidEmailOrUsername).WithMessage("Geçerli bir e-posta adresi girin veya kullanıcı adı kullanın.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifre boş olamaz.")
            .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalı.");

        RuleFor(x => x.RePassword)
            .Equal(x => x.Password).WithMessage("Şifreler eşleşmiyor.");
    }

    private bool BeValidEmailOrUsername(string input)
    {
        if (input.Contains("@"))
        {
            return new EmailAddressAttribute().IsValid(input);
        }
        return input.Length >= 3 && input.All(c => char.IsLetterOrDigit(c));
    }
}