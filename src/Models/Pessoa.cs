using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ms_processoSeletivo.Models
{
    [Table("pessoa")]
    public class Pessoa
    {
        [Required]
        [Key]
        [Column("id_pessoa")]
        public int Id { get; set; }
        [Column("nome")]
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string Nome { get; set; } = string.Empty;
        [Column("cpf")]
        [Required(ErrorMessage = "O campo CPF é obrigatório.")]
        public string CPF { get; set; } = string.Empty;
        [Column("data_nascimento")]
        [Required(ErrorMessage = "O campo Data de Nascimento é obrigatório.")]
        public DateTime DtNascimento { get; set; }
        [Column("endereco")]
        [Required(ErrorMessage = "O campo Endereço é obrigatório.")]
        public string Endereco { get; set; } = string.Empty;
        [Column("sexo")]
        [Required(ErrorMessage = "O campo Sexo é obrigatório.")]
        public string Sexo { get; set; } = string.Empty;
    }
}