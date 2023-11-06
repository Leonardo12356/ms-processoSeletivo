
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ms_processoSeletivo.Models.Entities.Dtos.Pessoa
{
    public class ReadPessoaDto
    {

        public int Id { get; set; }
        [Column("nome")]
        [Required]
        public string Nome { get; set; } = string.Empty;
        [Column("cpf")]
        [Required]
        public string CPF { get; set; } = string.Empty;
        [Column("data_nascimento")]
        [Required]
        public DateTime DtNascimento { get; set; }
        [Column("endereco")]
        [Required]
        public string Endereco { get; set; } = string.Empty;
        [Column("sexo")]
        [Required]
        public string Sexo { get; set; } = string.Empty;
    }
}