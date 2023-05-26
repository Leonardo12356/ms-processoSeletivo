using AutoMapper;
using ms_processoSeletivo.Data;
using ms_processoSeletivo.Interfaces;
using ms_processoSeletivo.Models;
using ms_processoSeletivo.Models.Entities.Dtos.Pessoa;

namespace ms_processoSeletivo.Domain
{
    public class PessoaDomain : IPessoa
    {
        private readonly AppDbContext _context;

        private readonly IMapper _mapper;

        public PessoaDomain(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadPessoaDto Adicionar(AddPessoaDto dto)
        {
            Pessoa pessoa = _mapper.Map<Pessoa>(dto);
            _context.Pessoas.Add(pessoa);
            _context.SaveChanges();
            ReadPessoaDto pessoaDto = _mapper.Map<ReadPessoaDto>(pessoa);

            return pessoaDto;
        }

        public ReadPessoaDto BuscarPorId(int id)
        {
            Pessoa pessoa = _context.Pessoas.FirstOrDefault(pessoa => pessoa.Id == id);
            ReadPessoaDto pessoaDto = _mapper.Map<ReadPessoaDto>(pessoa);

            return pessoaDto;
        }

        public IEnumerable<ReadPessoaDto> BuscarTodos()
        {
            var lista = _context.Pessoas.ToList();
            IEnumerable<ReadPessoaDto> readPessoaDtos = _mapper.Map<List<ReadPessoaDto>>(lista);
            return readPessoaDtos;
        }

        public ReadPessoaDto Editar(int id, UpdatePessoaDto dto)
        {
            Pessoa pessoa = _context.Pessoas.FirstOrDefault(pessoa => pessoa.Id == id);
            if (pessoa != null)
            {
                _mapper.Map(dto, pessoa);
                ReadPessoaDto pessoaDto = _mapper.Map<ReadPessoaDto>(pessoa);
                _context.SaveChanges();

                return pessoaDto;
            }

            return null;
        }

        public bool Excluir(int id)
        {
            Pessoa pessoa = _context.Pessoas.FirstOrDefault(pessoa => pessoa.Id == id);

            if (pessoa != null)
            {
                _context.Remove(pessoa);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

    }
}