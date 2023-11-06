using FluentResults;
using ms_processoSeletivo.Models;

namespace ms_processoSeletivo.Exceptions.Interfaces
{
    public interface IPessoaExceptions
    {
        Result ValidarCpf(string cpf, int id);

    }
}