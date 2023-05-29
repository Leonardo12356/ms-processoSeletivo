using System;
using Xunit;
using Moq;
using ms_processoSeletivo.Exceptions;
using ms_processoSeletivo.Exceptions.Interfaces;
using ms_processoSeletivo.Models;
using ms_processoSeletivo.Data;
using Moq.EntityFrameworkCore;

namespace testesAPI
{
    public class PessoaExceptionTests
    {
        private readonly Mock<AppDbContext> _dbContextMock;
        private readonly IPessoaException _pessoaException;

        public PessoaExceptionTests()
        {
            _dbContextMock = new Mock<AppDbContext>();
            _pessoaException = new PessoaException(_dbContextMock.Object);
        }

        [Fact]
        public void ValidarCpf_CpfInvalido_ReturnsFail()
        {
            // Arrange
            int id = 1;
            string cpf = "12345678901"; // CPF inválido (menos de 11 dígitos)

            // Simular uma lista vazia de pessoas no contexto de banco de dados
            var pessoas = new List<Pessoa>();

            // Configurar o mock do DbContext
            var dbContextMock = new Mock<AppDbContext>();
            dbContextMock.Setup(c => c.Pessoas).ReturnsDbSet(pessoas);

            // Criar uma instância de PessoaException com o mock do DbContext
            var pessoaException = new PessoaException(dbContextMock.Object);

            // Act
            var result = pessoaException.ValidarCpf(cpf, id);

            // Assert
            Assert.True(result.IsFailed);
            Assert.Equal("CPF inválido", result.Errors[0].Message);
        }



        [Fact]
        public void ValidarCpf_CpfJaCadastradoPorOutraPessoa_ReturnsFail()
        {
            // Arrange
            int id = 1;
            string cpf = "916.220.480-75";

            string cpfSemFormatacao = cpf.Replace(".", "").Replace("-", "");

            // Simular a lista de pessoas com um CPF já cadastrado por outra pessoa
            var pessoas = new List<Pessoa>
            {
                new Pessoa { Id = 2, CPF = cpfSemFormatacao },
                new Pessoa { Id = 3, CPF = "123456789" }
            };

            _dbContextMock.Setup(c => c.Pessoas).ReturnsDbSet(pessoas);

            // Act
            var result = _pessoaException.ValidarCpf(cpf, id);

            // Assert
            Assert.False(result.IsFailed);


        }


        [Fact]
        public void ValidarCpf_CpfValido_ReturnsOk()
        {
            // Arrange
            int id = 1;
            string cpf = "916.220.480-75";

            string cpfSemFormatacao = cpf.Replace(".", "").Replace("-", "");

            var pessoas = new List<Pessoa>
        {
            new Pessoa { Id = 2, CPF = cpfSemFormatacao },
            new Pessoa { Id = 3, CPF = "91622048075" }
        };

            _dbContextMock.Setup(c => c.Pessoas).ReturnsDbSet(pessoas);

            // Act
            var result = _pessoaException.ValidarCpf(cpf, id);

            // Assert
            Assert.True(result.IsSuccess);
        }




        [Fact]
        public void ValidarDtNascimento_DataNascimentoInvalida_ReturnsFail()
        {
            // Arrange
            var dtNascimento = DateTime.Now.AddDays(1); // Data de nascimento no futuro

            // Act
            var result = _pessoaException.ValidarDtNascimento(dtNascimento);

            // Assert
            Assert.True(result.IsFailed);
            Assert.Equal("Data de nascimento inválida", result.Errors[0].Message);
        }


        [Fact]
        public void ValidarDtNascimento_IdadeCorreta_ReturnsOk()
        {
            // Arrange
            var dbContextMock = new Mock<AppDbContext>();
            var pessoaException = new PessoaException(dbContextMock.Object);
            var dtNascimento = new DateTime(2000, 1, 1);
            var idadeEsperada = 23;

            // Act
            var result = pessoaException.ValidarDtNascimento(dtNascimento);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(idadeEsperada, DateTime.Now.Year - dtNascimento.Year);
        }


        [Fact]
        public void ValidarDtNascimento_DataNascimentoMaiorDeIdade_ReturnsOk()
        {
            // Arrange
            var dtNascimento = DateTime.Now.AddYears(-20); // Data de nascimento com mais de 18 anos

            // Act
            var result = _pessoaException.ValidarDtNascimento(dtNascimento);

            // Assert
            Assert.True(result.IsSuccess);
        }
    }
}