using GerenciamentoAssuntos.Domain.Interfaces;
using GerenciamentoAssuntos.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoAssuntos.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class AssuntosController : ControllerBase
    {
        private IAssuntoService _assuntoService;
        public AssuntosController(IAssuntoService assuntoService)
        {
            _assuntoService = assuntoService;
        }

        /// <summary>
        /// Consulta todas os assuntos.
        /// </summary>
        /// <returns></returns>
        /// <response code ="200">Retorna todos os assuntos</response>
        ///  <response code ="204">Nenhum assuntos foi encontrado</response>
        [HttpGet("all")]
        [ProducesResponseType(typeof(AssuntoResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAllMoto()
        {
            var response = await _assuntoService.GetAllAsync();

            if (response == null)
                return NoContent();

            return Ok(response);

        }

        /// <summary>
        /// Realiza a consulta dos dados atraves do assunto.
        /// </summary>
        /// <returns></returns>
        /// <response code ="200">Retorna os dados atraves do assunto</response>
        ///  <response code ="204">Nenhum assunto foi encontrado</response>
        [HttpGet("{assuntosNome}")]
        [ProducesResponseType(typeof(AssuntoResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetByAsssunto(string assuntosNome)
        {
            var response = await _assuntoService.GetByAsssunto(assuntosNome);

            if (response == null)
                return NoContent();

            return Ok(response);

        }

        /// <summary>
        /// Adiciona um novo assunto.
        /// </summary>
        /// <returns></returns>
        /// <response code ="200">Incluir um novo assunto</response>
        [HttpPost("assuntos")]
        [ProducesResponseType(typeof(AssuntoResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddMoto([FromBody] AssuntoRequestModel request)
        {
            var response = await _assuntoService.AddAsync(request);

            if (!response.IsValid)
                return StatusCode(response.StatusCode, response.Description);

            return Ok(response);
        }

        /// <summary>
        /// Atualizar o assunto.
        /// </summary>
        /// <returns></returns>
        /// <response code ="200">Atualizar assunto</response>
        [HttpPut("{id}/{nomeAssunto}")]
        [ProducesResponseType(typeof(AssuntoResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> updateMoto(AssuntoReqModel request)
        {
            var response = await _assuntoService.UpdateAsync(request);

            if (!response.IsValid)
                return StatusCode(response.StatusCode, response.Description);

            return Ok(response);
        }

        /// <summary>
        /// Remover um assunto.
        /// </summary>
        /// <returns></returns>
        /// <response code ="200">Remover um assunto</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(AssuntoResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> deleteMoto(int id)
        {
            var response = await _assuntoService.DeleteAsync(id);

            if (!response.IsValid)
                return StatusCode(response.StatusCode, response.Description);

            return Ok();
        }
    }
}
