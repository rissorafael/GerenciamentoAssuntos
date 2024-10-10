using AutoMapper;
using GerenciamentoAssuntos.Domain.Entities;
using GerenciamentoAssuntos.Domain.Enums;
using GerenciamentoAssuntos.Domain.Interfaces;
using GerenciamentoAssuntos.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace GerenciamentoAssuntos.Service.Service
{
    public class ConteudoService : IConteudoService
    {
        private readonly IConteudoRepository _conteudoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AssuntoService> _logger;
        private readonly IGdeltProjectIntegration _gdeltProjectIntegration;
        private readonly IAssuntoService _assuntoService;

        public ConteudoService(IConteudoRepository conteudoRepository, IMapper mapper, ILogger<AssuntoService> logger, IGdeltProjectIntegration gdeltProjectIntegration, IAssuntoService assuntoService)
        {
            _conteudoRepository = conteudoRepository;
            _mapper = mapper;
            _logger = logger;
            _gdeltProjectIntegration = gdeltProjectIntegration;
            _assuntoService = assuntoService;
        }

        public async Task<List<ConteudoResponseModel>> AddlistAsync(List<ConteudoRequestModel> request)
        {
            var response = new List<ConteudoResponseModel>();

            try
            {
                var req = _mapper.Map<List<Conteudo>>(request);

                var conteudo = await _conteudoRepository.AddlistAsync(req);
                var conteudolist = await _conteudoRepository.GetByAsssuntoId(request.First().AssuntoId);

                response = _mapper.Map<List<ConteudoResponseModel>>(conteudolist);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[ConteudoService - AddAsync] - Não foi possivel adicionar o registro : {ex.Message}");
                throw;
            }
        }

        public async Task<List<ConteudoResponseModel>> GetByConteudoAsync(int assuntoId)
        {
            var assuntoResponse = new AssuntoResponseModel();
            var responseModel = new List<ConteudoResponseModel>();
            var conteudoList = new List<ConteudoRequestModel>();
            var conteudosResponse = new List<ConteudoResponseModel>();

            try
            {
                assuntoResponse = await _assuntoService.GetByIdAsync(assuntoId);

                if (assuntoResponse == null)
                {
                    var error = new ConteudoResponseModel();
                    error.AddErrorValidation(StatusCodes.Status404NotFound, $"Não foi possivel buscar esse assunto");
                    responseModel.Add(error);

                    return responseModel;
                }
                if (assuntoResponse.Status == (int)AssuntoStatus.Concluido)
                {
                    var error = new ConteudoResponseModel();
                    error.AddErrorValidation(StatusCodes.Status404NotFound, $"Não foi possivel buscar esse assunto");
                    responseModel.Add(error);

                    return responseModel;
                }


                await _assuntoService.UpdateAsync(_mapper.Map<AssuntoReqModel>(assuntoResponse));

                var noticias = await _gdeltProjectIntegration.GetByNoticiasAsync(assuntoResponse.PalavrasChaves);
                var artigosPortugues = noticias.articles.Where(a => a.language == "Portuguese").ToList();

                if (artigosPortugues == null)
                {
                    assuntoResponse.DataAtualizacao = DateTime.Now;
                    assuntoResponse.Status = (int)AssuntoStatus.EmProgresso;
                    await _assuntoService.UpdateAsync(_mapper.Map<AssuntoReqModel>(assuntoResponse));

                    responseModel.First().AddErrorValidation(StatusCodes.Status404NotFound, $"Não foi possivel buscar esse assunto");
                    return responseModel;
                }
                else
                {
                    assuntoResponse.DataAtualizacao = DateTime.Now;
                    assuntoResponse.Status = (int)AssuntoStatus.Concluido;

                    foreach (var artigo in artigosPortugues)
                    {
                        var conteudo = new ConteudoRequestModel();

                        conteudo.AssuntoId = assuntoResponse.Id;
                        conteudo.Link = artigo.url;

                        conteudoList.Add(conteudo);
                    }

                    conteudosResponse = await AddlistAsync(conteudoList);

                }

                await _assuntoService.UpdateAsync(_mapper.Map<AssuntoReqModel>(assuntoResponse));


                return conteudosResponse;
            }
            catch (Exception ex)
            {
                assuntoResponse.DataAtualizacao = DateTime.Now;
                assuntoResponse.Status = (int)AssuntoStatus.Pendente;
                await _assuntoService.UpdateAsync(_mapper.Map<AssuntoReqModel>(assuntoResponse));

                _logger.LogError($"[ConteudoService - GetByConteudoAsync] - Não foi possivel buscar os registros : {ex.Message}");
                throw;
            }
        }

    }
}




