namespace ClientesProdutos.Infrastructure.Validators.Client;

public class AddClientValidator : AbstractValidator<AddClientViewModel>
{
    public AddClientValidator()
    {
        RuleFor(x => x.ClientName)
            .NotNull().WithMessage("Nome do cliente deve ser informado!")
            .NotEmpty().WithMessage("Nome do cliente deve ser informado!")
            .Length(1, 50).WithMessage("Nome do cliente deve conter entre 1 a 50 caractéres!");

        RuleFor(x => x.LastName)
            .NotNull().WithMessage("Sobrenome do cliente deve ser informado!")
            .NotEmpty().WithMessage("Sobrenome do cliente deve ser informado!")
            .Length(1, 50).WithMessage("Sobrenome do cliente deve conter entre 1 a 50 caractéres!");

        RuleFor(x => x.Email)
            .NotNull().WithMessage("Email do cliente deve ser informado!")
            .NotEmpty().WithMessage("Email do cliente deve ser informado!")
            .EmailAddress().WithMessage("Um endereço de email válido deve ser informado!")
            .Length(1, 50).WithMessage("mail do cliente deve conter entre 1 a 50 caractéres!");
    }
}