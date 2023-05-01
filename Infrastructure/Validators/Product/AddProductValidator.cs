namespace ClientesProdutos.Infrastructure.Validators.Product;

public class AddProductValidator : AbstractValidator<AddProductViewModel>
{
    public AddProductValidator()
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage("Nome do produto deve ser informado!")
            .NotEmpty().WithMessage("Nome do produto deve ser informado!")
            .Length(1, 100).WithMessage("Nome do produto deve conter entre 1 a 100 caractéres!");

        RuleFor(x => x.Price)
            .NotNull().WithMessage("Preço do produto deve ser informado!")
            .NotEmpty().WithMessage("Preço do produto deve ser informado!")
            .GreaterThan(1).WithMessage($"Preço do produto deve ser entre {1:C} e {100000:C}!")
            .LessThanOrEqualTo(100000).WithMessage($"Preço do produto deve ser entre {1:C} e {100000:C}!");
    }
}