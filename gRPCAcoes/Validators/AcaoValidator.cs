using FluentValidation;

namespace gRPCAcoes.Validators
{
    public class AcaoValidator : AbstractValidator<AcaoRequest>
    {
        public AcaoValidator()
        {
            RuleFor(c => c.Codigo).NotEmpty().WithMessage("Preencha o campo 'Codigo'");

            RuleFor(c => c.Valor).NotEmpty().WithMessage("Preencha o campo 'Valor'")
                .GreaterThan(0).WithMessage("O campo 'Valor' deve ser maior do 0");
        }                
    }
}