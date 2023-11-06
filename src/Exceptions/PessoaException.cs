using FluentResults;
using ms_processoSeletivo.Data;
using ms_processoSeletivo.Exceptions.Interfaces;
using ms_processoSeletivo.Models;

namespace ms_processoSeletivo.Exceptions
{
    public class PessoaExceptions : IPessoaExceptions
    {

        private readonly AppDbContext _context;

        public PessoaExceptions(AppDbContext context)
        {
            _context = context;
        }

        public Result ValidarCpf(string cpf, int id)
        {
            if (CPFJaCadastrado(cpf, id))
            {
                return Result.Fail("CPF já cadastrado por outra pessoa");
            }

            cpf = RemoverCaracteresNaoNumericos(cpf);

            if (CPFInvalido(cpf))
            {
                return Result.Fail("CPF inválido");
            }

            if (CPFDigitosIguais(cpf))
            {
                return Result.Fail("CPF inválido");
            }

            if (!CPFEhValido(cpf))
            {
                return Result.Fail("CPF inválido");
            }

            return Result.Ok();
        }

        private bool CPFJaCadastrado(string cpf, int id)
        {
            return _context.Pessoas.Any(p => p.CPF == cpf && p.Id != id);
        }

        private static string RemoverCaracteresNaoNumericos(string cpf)
        {
            return new string(cpf.Where(char.IsDigit).ToArray());
        }

        private static bool CPFInvalido(string cpf)
        {
            return cpf.Length != 11;
        }

        private static bool CPFDigitosIguais(string cpf)
        {
            return cpf.Distinct().Count() == 1;
        }

        private bool CPFEhValido(string cpf)
        {
            int[] multiplicadores1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicadores2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string cpfDigitos = cpf[..9];
            int digito1 = CalcularDigitoVerificador(cpfDigitos, multiplicadores1);
            cpfDigitos += digito1;
            int digito2 = CalcularDigitoVerificador(cpfDigitos, multiplicadores2);

            return cpf.EndsWith($"{digito1}{digito2}");
        }

        private static int CalcularDigitoVerificador(string cpfDigitos, int[] multiplicadores)
        {
            int soma = cpfDigitos.Zip(multiplicadores, (digito, multiplicador) => int.Parse(digito.ToString()) * multiplicador).Sum();
            int resto = soma % 11;
            return resto < 2 ? 0 : 11 - resto;
        }


       
        }
    }
