
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ms_processoSeletivo.Models.Entities.Dtos.Pessoa
{
    public class AddPessoaDto
    {
        [Column("nome")]
        [Required]
        public string Nome { get; set; }
        [Column("cpf")]
        [Required]
        public string CPF { get; set; }
        [Column("data_nascimento")]
        [Required]
        public DateTime DtNascimento { get; set; }
        [Column("endereco")]
        [Required]
        public string Endereco { get; set; }
        [Column("sexo")]
        [Required]
        public string Sexo { get; set; }
    }
}