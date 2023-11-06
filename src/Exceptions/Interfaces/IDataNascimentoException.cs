using FluentResults;

namespace ms_processoSeletivo.Exceptions.Interfaces
{
    public interface IDataNascimentoExceptions
    {
        Result ValidarDtNascimento(DateTime dtNascimento);
    }
}
