using FluentResults;
using ms_processoSeletivo.Data;
using ms_processoSeletivo.Exceptions.Interfaces;
using ms_processoSeletivo.Models;

namespace ms_processoSeletivo.Exceptions
{
    public class PessoaException : IPessoaException
    {

        private readonly AppDbContext _context;

        public PessoaException(AppDbContext context)
        {
            _context = context;
        }

        public Result ValidarCpf(string cpf, int id)
        {
            // Verificar se esse Cpf já foi cadastrado no banco por outra pessoa
            var cpfValido = _context.Pessoas.Any(p => p.CPF == cpf && p.Id != id);
            if (cpfValido)
            {
                return Result.Fail("CPF já cadastrado por outra pessoa");
            }

            // Remover caracteres não numéricos do CPF, mantendo apenas os números, pontos e traços
            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            // Verificar se o CPF possui 11 dígitos
            if (cpf.Length != 11)
            {
                return Result.Fail("CPF inválido");
            }

            // Verificar se todos os dígitos são iguais
            var digitosIguais = cpf.Distinct().Count() == 1;
            if (digitosIguais)
            {
                return Result.Fail("CPF inválido");
            }

            // Calcular os dígitos 
            int[] multiplicadores1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicadores2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string cpfDigitos = cpf.Substring(0, 9);
            int soma = cpfDigitos.Zip(multiplicadores1, (digito, multiplicador) => int.Parse(digito.ToString()) * multiplicador).Sum();
            int resto = soma % 11;
            int digito1 = resto < 2 ? 0 : 11 - resto;

            cpfDigitos += digito1;
            soma = cpfDigitos.Zip(multiplicadores2, (digito, multiplicador) => int.Parse(digito.ToString()) * multiplicador).Sum();
            resto = soma % 11;
            int digito2 = resto < 2 ? 0 : 11 - resto;

            // Verificar se os dígitos estão corretos
            var cpfCorreto = cpf.EndsWith($"{digito1}{digito2}");
            if (!cpfCorreto)
            {
                return Result.Fail("CPF inválido");
            }

            // CPF válido
            return Result.Ok();
        }

        public Result ValidarDtNascimento(DateTime dtNascimento)
        {
            // Verificar se a data de nascimento é uma data válida
            if (dtNascimento > DateTime.Now)
            {
                return Result.Fail("Data de nascimento inválida");
            }

            // Calcular a idade com base na data de nascimento
            int idade = DateTime.Now.Year - dtNascimento.Year;
            if (DateTime.Now.Month < dtNascimento.Month || (DateTime.Now.Month == dtNascimento.Month && DateTime.Now.Day < dtNascimento.Day))
            {
                idade--;
            }

            // Verificar se a pessoa é maior de idade
            if (idade < 18)
            {
                return Result.Fail("A pessoa na qual está tentando cadastrar deve ser maior de idade");
            }

            // Data de nascimento válida e pessoa é maior de idade
            return Result.Ok();
        }
    }
}