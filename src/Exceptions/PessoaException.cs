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
        public Result ValidarCpf(Pessoa pessoa)
        {
            throw new NotImplementedException();
        }
    }
}