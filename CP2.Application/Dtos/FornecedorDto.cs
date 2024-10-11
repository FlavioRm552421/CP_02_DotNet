using CP2.Domain.Interfaces.Dtos;
using FluentValidation;
using System.Security.Cryptography.X509Certificates;

namespace CP2.Application.Dtos
{
    public class FornecedorDto : IFornecedorDto
    {

        public string Nome { get; set; } = string.Empty;
        public string CNPJ { get; set; } = string.Empty;
        public string Telefone { get; set; }= string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public DateTime CriadoEm { get; set; } 

        public void Validate()
        {
            var validateResult = new FornecedorDtoValidation().Validate(this);

            if (!validateResult.IsValid)
                throw new Exception(string.Join(" e ", validateResult.Errors.Select(x => x.ErrorMessage)));
        }
    }

    internal class FornecedorDtoValidation : AbstractValidator<FornecedorDto>
    {
        public FornecedorDtoValidation()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório")
                .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres");

            RuleFor(x => x.CNPJ)
                .NotEmpty().WithMessage("O CNPJ é obrigatório")
                .MaximumLength(14).WithMessage("O CNPJ deve ter no máximo 14 caracteres");

            RuleFor(x => x.Telefone)
                .NotEmpty().WithMessage("O telefone é obrigatório")
                .MaximumLength(11).WithMessage("O telefone deve ter no máximo 11 caracteres");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O Email é obrigatório")
                .MaximumLength(100).WithMessage("O Email deve ter no máximo 100 caracteres");

            RuleFor(x => x.Endereco)
                .NotEmpty().WithMessage("O endereço é obrigatório")
                .MaximumLength(100).WithMessage("O endereço deve ter no máximo 100 caracteres");

            RuleFor(x => x.CriadoEm)
                .NotEmpty().WithMessage("A data de criação é obrigatória")
                .LessThan(DateTime.Now).WithMessage("A data de criação não pode ser maior que a data atual");
                ;

         
        }
    }
}
