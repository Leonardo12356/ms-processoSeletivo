using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ms_processoSeletivo.Exceptions.Interfaces;
using ms_processoSeletivo.Interfaces;
using ms_processoSeletivo.Models.Entities.Dtos.Pessoa;

namespace ms_processoSeletivo.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    [Authorize(Roles = "User")]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoa _interfaces;
        private readonly IPessoaExceptions _exception;
        private readonly IDataNascimentoExceptions _dataNascimentoException;

        public PessoaController(IPessoa interfaces, IPessoaExceptions exception, IDataNascimentoExceptions dataNascimentoException)
        {
            _interfaces = interfaces;
            _exception = exception;
            _dataNascimentoException = dataNascimentoException;
        }

        public static IEnumerable<string> MessageException(Result resultado)
        {
            return resultado.Reasons.Select(reason => reason.Message);
        }

        [HttpPost]
        public IActionResult Adicionar([FromBody] AddPessoaDto dto)
        {
            Result validacaoCpf = _exception.ValidarCpf(dto.CPF, 0);
            Result validacaoDataNascimento = _dataNascimentoException.ValidarDtNascimento(dto.DtNascimento);

            if (validacaoCpf.IsFailed || validacaoDataNascimento.IsFailed)
            {
                Result combinedResult = Result.Merge(validacaoCpf, validacaoDataNascimento);
                IEnumerable<string> errorMessages = MessageException(combinedResult);
                return BadRequest(errorMessages);
            }

            var pessoa = _interfaces.Adicionar(dto);
            if (pessoa != null)
            {
                return Ok(pessoa);
            }
            return BadRequest("Falha ao fazer o cadastro");
        }

        [HttpGet]
        public IActionResult BuscarTodos()
        {
            var pessoas = _interfaces.BuscarTodos();
            if (pessoas != null)
            {
                return Ok(pessoas);
            }
            return NotFound("Cadastros não encontrados.");
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var pessoa = _interfaces.BuscarPorId(id);

            if (pessoa != null)
            {
                return Ok(pessoa);
            }
            return NotFound("Cadastro não encontrado.");
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, [FromBody] UpdatePessoaDto dto)
        {

            Result validacaoCpf = _exception.ValidarCpf(dto.CPF, id);
            Result validacaoDataNascimento = _dataNascimentoException.ValidarDtNascimento(dto.DtNascimento);

            if (validacaoCpf.IsFailed || validacaoDataNascimento.IsFailed)
            {
                Result combinedResult = Result.Merge(validacaoCpf, validacaoDataNascimento);
                IEnumerable<string> errorMessages = MessageException(combinedResult);
                return BadRequest(errorMessages);
            }

            ReadPessoaDto pessoaDto = _interfaces.Editar(id, dto);
            if (pessoaDto != null)
            {
                return Ok(pessoaDto);
            }
            return BadRequest("Falha ao atualizar cadastro.");
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var pessoa = _interfaces.Excluir(id);

            if (!pessoa)
            {
                return BadRequest("Falha ao deletar cadastro.");
            }
            return Ok("Cadastro deletado com sucesso.");
        }
    }
}