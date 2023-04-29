namespace ClientesProdutos.Infrastructure.Validators;

public class AddClientValidator : AbstractValidator<AddClientViewModel>
{
    public AddClientValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nome do cliente deve ser informado!")
            .Length(1, 50).WithMessage("Nome do cliente deve conter entre 1 a 50 caractéres!");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Sobrenome do cliente deve ser informado!")
            .Length(1, 50).WithMessage("Sobrenome do cliente deve conter entre 1 a 50 caractéres!");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Email do cliente deve ser informado!")
            .EmailAddress().WithMessage("Um endereço de email válido deve ser informado!")
            .Length(1, 50).WithMessage("mail do cliente deve conter entre 1 a 50 caractéres!");
    }
}