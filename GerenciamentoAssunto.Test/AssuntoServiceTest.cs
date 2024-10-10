using AutoMapper;
using GerenciamentoAssuntos.Domain.Entities;
using GerenciamentoAssuntos.Domain.Interfaces;
using GerenciamentoAssuntos.Domain.Models;
using GerenciamentoAssuntos.Service.Service;
using Microsoft.Extensions.Logging;
using Moq;

namespace GerenciamentoAssunto.Test
{
    public class AssuntoServiceTest
    {
        private readonly Mock<IAssuntoRepository> _assuntoRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly AssuntoService _assuntoService;

        public AssuntoServiceTest()
        {
            _assuntoRepositoryMock = new Mock<IAssuntoRepository>();
            _mapperMock = new Mock<IMapper>();
            _assuntoService = new AssuntoService(_assuntoRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnMappedResponse_WhenIdIsValid()
        {
            // Arrange
            var assunto = new Assunto { Id = 1, PalavrasChaves = "Assunto Teste" }; // Simula o retorno do repositório
            var assuntoResponse = new AssuntoResponseModel { Id = 1, PalavrasChaves = "Assunto Teste" }; // Simula a resposta mapeada

            // Configura o mock do repositório para retornar um assunto simulado
            _assuntoRepositoryMock.Setup(repo => repo.GetByIdAsync(1))
                                  .ReturnsAsync(assunto);

            // Configura o mock do mapeador para mapear o assunto para o AssuntoResponseModel
            _mapperMock.Setup(mapper => mapper.Map<AssuntoResponseModel>(assunto))
                       .Returns(assuntoResponse);

            // Act
            var result = await _assuntoService.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Assunto Teste", result.PalavrasChaves);

            // Verifica se os métodos foram chamados corretamente
            _assuntoRepositoryMock.Verify(repo => repo.GetByIdAsync(1), Times.Once);
            _mapperMock.Verify(mapper => mapper.Map<AssuntoResponseModel>(assunto), Times.Once);
        }

    }
}