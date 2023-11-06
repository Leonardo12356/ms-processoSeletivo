using ms_processoSeletivo.Exceptions;
using Moq;
using Xunit;
using ms_processoSeletivo.Data;
using Moq.EntityFrameworkCore;
using ms_processoSeletivo.Models;


namespace testesAPI.PessoaExceptionTest
{
    public class PessoaExceptionTest
    {
        [Fact]
        public void ValidarCpf_CpfValido_DeveRetornarSucesso()
        {
            // Arrange
            var context = new Mock<AppDbContext>();
            var pessoas = new List<Pessoa>
            {
                new Pessoa { Id = 1, CPF = "12345678901" },
                new Pessoa { Id = 2, CPF = "98765432101" }
            };
            context.Setup(c => c.Pessoas).ReturnsDbSet(pessoas);

            var pessoaException = new PessoaExceptions(context.Object);
            string cpf = "12319556788";
            int id = 3;

            // Act
            var result = pessoaException.ValidarCpf(cpf, id);

            // Assert
            Assert.True(result.IsSuccess);
            context.Verify(c => c.Pessoas, Times.Once());
        }


        [Fact]
        public void ValidarCpf_CpfInvalido_DeveRetornarFalha()
        {
            // Arrange
            var context = new Mock<AppDbContext>();
            var pessoas = new List<Pessoa>
            {
                new Pessoa { Id = 1, CPF = "98765432101" },
                new Pessoa { Id = 2, CPF = "98765432102" }
            };
            context.Setup(c => c.Pessoas).ReturnsDbSet(pessoas);

            var pessoaException = new PessoaExceptions(context.Object);
            string cpf = "12345678901";
            int id = 3;

            // Act
            var result = pessoaException.ValidarCpf(cpf, id);

            // Assert
            Assert.True(result.IsFailed);
            Assert.Equal("CPF inválido", result.Errors[0].Message);
        }


        [Theory]
        [InlineData("12345678909", 1, true)] // CPF válido e não duplicado
        [InlineData("123.195.567-88", 1, true)] // CPF válido e não duplicado (com formatação)
        [InlineData("123.195.567-88", 2, false)] // CPF válido, mas duplicado
        [InlineData("12345678900", 1, false)] // CPF inválido
        [InlineData("11111111111", 1, false)] // CPF com dígitos iguais
        public void ValidarCpf_RetornaResultadoExperado(string cpf, int id, bool expectedResult)
        {
            // Arrange
            var context = new Mock<AppDbContext>();
            var pessoas = new List<Pessoa>
            {
                new Pessoa { Id = 1, CPF = "123.195.567-88" },
                new Pessoa { Id = 2, CPF = "987.654.321-01" }
            };
            context.Setup(c => c.Pessoas).ReturnsDbSet(pessoas);

            var pessoaExceptions = new PessoaExceptions(context.Object);

            // Act
            var result = pessoaExceptions.ValidarCpf(cpf, id);

            // Assert
            Assert.Equal(expectedResult, result.IsSuccess);
        }




    }
}
