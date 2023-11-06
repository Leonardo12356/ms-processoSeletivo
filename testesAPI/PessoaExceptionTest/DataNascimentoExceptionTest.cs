
using ms_processoSeletivo.Exceptions;
using Xunit;

namespace testesAPI.PessoaExceptionTest
{
    public class DataNascimentoExceptionTest
    {
        [Fact]
        public void ValidarDtNascimento_DeveRetornarResultadoOk()
        {
            // Arrange
            var dataNascimentoException = new DataNascimentoExceptions();
            DateTime dtNascimento = new DateTime(1994, 8, 16);

            // Act
            var result = dataNascimentoException.ValidarDtNascimento(dtNascimento);

            // Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void ValidarDtNascimento_DeveRetornarFalhaParaDataInvalida()
        {
            // Arrange
            var dataNascimentoExceptions = new DataNascimentoExceptions();
            DateTime dataInvalida = DateTime.Now.AddYears(1);

            // Act
            var result = dataNascimentoExceptions.ValidarDtNascimento(dataInvalida);

            // Assert
            Assert.True(result.IsFailed);
            Assert.Contains("Data de nascimento inválida", result.Errors[0].Message);
        }

        [Fact]
        public void ValidarDtNascimento_DeveRetornarFalhaParaMenorDeIdade()
        {
            // Arrange
            var dataNascimentoExceptions = new DataNascimentoExceptions();
            DateTime dataMenorDeIdade = DateTime.Now.AddYears(-10);

            // Act
            var result = dataNascimentoExceptions.ValidarDtNascimento(dataMenorDeIdade);

            // Assert
            Assert.True(result.IsFailed);
            Assert.Contains("A pessoa na qual está tentando cadastrar deve ser maior de idade", result.Errors[0].Message);
        }

    }
}
