using AutoMapper;
using ms_processoSeletivo.Models;
using ms_processoSeletivo.Models.Entities.Dtos.Pessoa;

namespace ms_processoSeletivo.Profiles
{
    public class PessoaProfile : Profile
    {
        public PessoaProfile()
        {
            CreateMap<AddPessoaDto, Pessoa>();
            CreateMap<Pessoa, ReadPessoaDto>();
            CreateMap<UpdatePessoaDto, Pessoa>();
        }
    }
}