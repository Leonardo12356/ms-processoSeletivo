using FluentResults;
using ms_processoSeletivo.Models;

namespace ms_processoSeletivo.Exceptions.Interfaces
{
    public interface IPessoaException
    {
        Result ValidarCpf(Pessoa pessoa);
    }
}