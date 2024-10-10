using AutoMapper;
using GerenciamentoAssuntos.Domain.Entities;
using GerenciamentoAssuntos.Domain.Interfaces;
using GerenciamentoAssuntos.Domain.Models;
using GerenciamentoAssuntos.Service.Service;
using Microsoft.Extensions.Logging;
using Moq;


namespace GerenciamentoAssunto.Test
{
    public class ConteudoServiceTest
    {
        private readonly Mock<IConteudoRepository> _conteudoRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<AssuntoService>> _loggerMock;
        private readonly Mock<IGdeltProjectIntegration> _gdeltProjectIntegrationMock;
        private readonly Mock<IAssuntoService> _assuntoServiceMock;
        private readonly ConteudoService _conteudoService;


        public ConteudoServiceTest()
        {
            _conteudoRepositoryMock = new Mock<IConteudoRepository>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<AssuntoService>>();
            _gdeltProjectIntegrationMock = new Mock<IGdeltProjectIntegration>();
            _assuntoServiceMock = new Mock<IAssuntoService>();

            _conteudoService = new ConteudoService(
                _conteudoRepositoryMock.Object,
                _mapperMock.Object,
                _loggerMock.Object,
                _gdeltProjectIntegrationMock.Object,
                _assuntoServiceMock.Object);

        }

        [Fact]
        public async Task AddlistAsync_ShouldReturnMappedResponse_WhenCalled()
        {
            // Arrange
            var conteudoRequest = new List<ConteudoRequestModel>
        {
            new ConteudoRequestModel { AssuntoId = 1, Link = "https://g1.globo.com/" }
        };

            var conteudoList = new List<Conteudo>
        {
            new Conteudo { Id = 1, AssuntoId = 1, Link = "https://ge.globo.com/" }
        };

            var conteudoResponse = new List<ConteudoResponseModel>
        {
            new ConteudoResponseModel { Id = 1, Link = "https://ge.globo.com/" }
        };

            // Configura o mock do mapeamento de request para o domínio
            _mapperMock.Setup(m => m.Map<List<Conteudo>>(conteudoRequest))
                       .Returns(conteudoList);

            // Simula a adição no repositório
            _conteudoRepositoryMock.Setup(r => r.AddlistAsync(conteudoList))
                                   .ReturnsAsync(It.IsAny<int>());

            // Simula a obtenção da lista com base no AssuntoId
            _conteudoRepositoryMock.Setup(r => r.GetByAsssuntoId(1))
                                   .ReturnsAsync(conteudoList);

            // Configura o mock do mapeamento da resposta
            _mapperMock.Setup(m => m.Map<List<ConteudoResponseModel>>(conteudoList))
                       .Returns(conteudoResponse);

            // Act
            var result = await _conteudoService.AddlistAsync(conteudoRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Count);
            Assert.Equal("Conteúdo 1", result.First().Link);

            // Verifica se os métodos foram chamados corretamente
            _mapperMock.Verify(m => m.Map<List<Conteudo>>(conteudoRequest), Times.Once);
            _conteudoRepositoryMock.Verify(r => r.AddlistAsync(conteudoList), Times.Once);
            _conteudoRepositoryMock.Verify(r => r.GetByAsssuntoId(1), Times.Once);
            _mapperMock.Verify(m => m.Map<List<ConteudoResponseModel>>(conteudoList), Times.Once);
        }
    }
}