using ms_processoSeletivo.Models.Entities.Dtos.Pessoa;

namespace ms_processoSeletivo.Interfaces
{
    public interface IPessoa : IBaseInt<AddPessoaDto, ReadPessoaDto>, IUpdate<UpdatePessoaDto, ReadPessoaDto>
    {

    }
}