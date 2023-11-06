using FluentResults;
using ms_processoSeletivo.Exceptions.Interfaces;

namespace ms_processoSeletivo.Exceptions
{
    public class DataNascimentoExceptions : IDataNascimentoExceptions
    {

        public Result ValidarDtNascimento(DateTime dtNascimento)
        {
            if (DataNascimentoInvalida(dtNascimento))
            {
                return Result.Fail("Data de nascimento inválida");
            }

            int idade = CalcularIdade(dtNascimento);

            if (MenorDeIdade(idade))
            {
                return Result.Fail("A pessoa na qual está tentando cadastrar deve ser maior de idade");
            }

            return Result.Ok();
        }

        private static bool DataNascimentoInvalida(DateTime dtNascimento)
        {
            return dtNascimento > DateTime.Now;
        }

        private static int CalcularIdade(DateTime dtNascimento)
        {
            int idade = DateTime.Now.Year - dtNascimento.Year;

            if (DateTime.Now.Month < dtNascimento.Month || (DateTime.Now.Month == dtNascimento.Month && DateTime.Now.Day < dtNascimento.Day))
            {
                idade--;
            }

            return idade;
        }

        private static bool MenorDeIdade(int idade)
        {
            return idade < 18;
        }

    }
}
