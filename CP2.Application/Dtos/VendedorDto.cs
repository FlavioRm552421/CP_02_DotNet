using CP2.Domain.Interfaces.Dtos;
using FluentValidation;

namespace CP2.Application.Dtos
{
    public class VendedorDto : IVendedorDto
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Endereco { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataContratacao { get; set; }
        public decimal ComissaoPercentual { get; set; }
        public decimal MetaMensal { get; set; }


        public void Validate()
        {
            var validateResult = new VendedorDtoValidation().Validate(this);

            if (!validateResult.IsValid)
                throw new Exception(string.Join(" e ", validateResult.Errors.Select(x => x.ErrorMessage)));
        }
    }

    internal class VendedorDtoValidation : AbstractValidator<VendedorDto>
    {
        public VendedorDtoValidation()
        {
            RuleFor(x => x.Nome)
           .NotEmpty().WithMessage("O nome é obrigatório")
           .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres");

            RuleFor(x => x.Telefone)
          .NotEmpty().WithMessage("O telefone é obrigatório")
          .Matches(@"^\d{10,11}$").WithMessage("O telefone deve conter 10 ou 11 dígitos");

            RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O email é obrigatório")
            .EmailAddress().WithMessage("O email inválido");

            RuleFor(x => x.Endereco)
           .NotEmpty().WithMessage("O endereço é obrigatório")
           .MaximumLength(200).WithMessage("O endereço deve ter no máximo 200 caracteres");

            RuleFor(x => x.CriadoEm)
          .LessThanOrEqualTo(DateTime.Now).WithMessage("A data de criação não pode ser futura");

            RuleFor(x => x.DataNascimento)
           .NotEmpty().WithMessage("A data de nascimento é obrigatória")
           .LessThan(DateTime.Now.AddYears(-18)).WithMessage("O vendedor deve ter no mínimo 18 anos");

            RuleFor(x => x.DataContratacao)
           .NotEmpty().WithMessage("A data de contratação é obrigatória")
           .GreaterThanOrEqualTo(x => x.DataNascimento).WithMessage("A data de contratação deve ser posterior à data de nascimento");

            RuleFor(x => x.ComissaoPercentual)
           .InclusiveBetween(0, 100).WithMessage("A comissão deve estar entre 0% e 100%");

            RuleFor(x => x.MetaMensal)
            .GreaterThan(0).WithMessage("A meta mensal deve ser um valor positivo");






        }
    }
}
