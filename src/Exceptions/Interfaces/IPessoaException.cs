using FluentResults;
using ms_processoSeletivo.Models;

namespace ms_processoSeletivo.Exceptions.Interfaces
{
    public interface IPessoaException
    {
        Result ValidarCpf(string cpf, int id);

        Result ValidarDtNascimento(DateTime dtNascimento);


    }
}