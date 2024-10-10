using GerenciamentoAssuntos.Domain.Interfaces;
using GerenciamentoAssuntos.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoAssuntos.Api.Controllers
{

    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class ConteudoController : ControllerBase
    {
        private IConteudoService _conteudoService;

        public ConteudoController(IConteudoService conteudoService)
        {
            _conteudoService = conteudoService;
        }

        /// <summary>
        /// Realiza a consulta do conteudo atraves do id do assunto.
        /// </summary>
        /// <returns></returns>
        /// <response code ="200">Retorna o conteudo atraves do id do assunto</response>
        ///  <response code ="204">Nenhum assunto foi encontrado</response>
        [HttpGet("GetByConteudo/{id}")]
        [ProducesResponseType(typeof(ConteudoResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetByConteudo(int id)
        {

            var response = await _conteudoService.GetByConteudoAsync(id);

            if (!response.First().IsValid)
                return StatusCode(response.First().StatusCode, response.First().Description);

            return Ok(response);

        }
    }
}
