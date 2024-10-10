using AutoMapper;
using GerenciamentoAssuntos.Domain.Entities;
using GerenciamentoAssuntos.Domain.Enums;
using GerenciamentoAssuntos.Domain.Interfaces;
using GerenciamentoAssuntos.Domain.Models;
using GerenciamentoAssuntos.Domain.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;


namespace GerenciamentoAssuntos.Service.Service
{
    public class AssuntoService : IAssuntoService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AssuntoService> _logger;
        private readonly IAssuntoRepository _assuntosRepository;
  
        public AssuntoService(IAssuntoRepository assuntosRepository, IMapper mapper)
        {
            _assuntosRepository = assuntosRepository;
            _mapper = mapper;
        

        }

        public async Task<AssuntoResponseModel> GetByIdAsync(int id)
        {
            try
            {
                var assunto = await _assuntosRepository.GetByIdAsync(id);
                var response = _mapper.Map<AssuntoResponseModel>(assunto);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[AssuntoService - GetByIdAsync] - Não foi possivel buscar os registros : {ex.Message}");
                throw;
            }
        }

        public async Task<List<AssuntoResponseModel>> GetAllAsync()
        {
            try
            {
                var assuntos = await _assuntosRepository.GetAllAsync();
                var response = _mapper.Map<List<AssuntoResponseModel>>(assuntos);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[AssuntoService - GetAllAsync] - Não foi possivel buscar os registros : {ex.Message}");
                throw;
            }
        }
        public async Task<AssuntoResponseModel> GetByAsssunto(string assuntosNome)
        {
            try
            {
                var assunto = await _assuntosRepository.GetByAsssunto(assuntosNome.Trim());

                var response = _mapper.Map<AssuntoResponseModel>(assunto);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[AssuntoService - GetByAsssunto] - Não foi possivel buscar o registro : {ex.Message}");
                throw;
            }
        }

        public async Task<AssuntoResponseModel> UpdateAsync(AssuntoReqModel request)
        {
            var response = new AssuntoResponseModel();

            try
            {

                var assunto = await _assuntosRepository.GetByIdAsync(request.Id);
                if (assunto == null)
                {
                    response.AddErrorValidation(StatusCodes.Status422UnprocessableEntity, $"Não existe o registro desse assunto id : {request.Id}");
                    return response;
                }
                var req = _mapper.Map<Assunto>(assunto);

                var assuntoResponse = await _assuntosRepository.UpdateAsync(req);

                response = _mapper.Map<AssuntoResponseModel>(assuntoResponse);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[AssuntosService - UpdateAsync] - Não foi possivel atualizar o registro : {ex.Message}");
                throw;
            }
        }

        public async Task<AssuntoResponseModel> AddAsync(AssuntoRequestModel request)
        {
            var response = new AssuntoResponseModel();

            try
            {
                if (await ValidaEntidadeAsync(request))
                {
                    var assunto = await _assuntosRepository.GetByAsssunto(request.PalavrasChaves);
                    if (assunto != null)
                    {
                        response.AddErrorValidation(StatusCodes.Status422UnprocessableEntity, $"Já existe um assunto com esse nome: {request.Titulo}");
                        return response;
                    }

                    var assuntoRequest = _mapper.Map<Assunto>(request);
                    assuntoRequest.DataCriacao = DateTime.Now;
                    assuntoRequest.Status = (int)AssuntoStatus.Pendente;
                    assuntoRequest.DataAtualizacao = DateTime.Now;

                    var assuntoResponse = await _assuntosRepository.AddAsync(assuntoRequest);

                    response = _mapper.Map<AssuntoResponseModel>(assuntoResponse);
                }
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[AssuntoService - AddAsync] - Não foi possivel adicionar o registro : {ex.Message}");
                throw;
            }
        }


        public async Task<AssuntoResponseModel> DeleteAsync(int id)
        {
            var responseModel = new AssuntoResponseModel();

            try
            {
                var assunto = await _assuntosRepository.GetByIdAsync(id);
                if (assunto == null)
                {
                    responseModel.AddErrorValidation(StatusCodes.Status404NotFound, $"Não foi possivel remover esse assunto");
                    return responseModel;
                }

                var assuntoResponse = await _assuntosRepository.DeleteAsync(id);

                var assunt = await _assuntosRepository.GetByIdAsync(id);
                responseModel = _mapper.Map<AssuntoResponseModel>(assunt);

                return responseModel;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[MotoService - DeleteAsync] - Não foi possivel remover o registro : {ex.Message}");
                throw;
            }
        }
        private async Task<bool> ValidaEntidadeAsync(AssuntoRequestModel assuntosRequestModel)
        {
            var validator = new AssuntosValidator();
            var validatorResult = await validator.ValidateAsync(assuntosRequestModel);

            foreach (var validation in validatorResult.Errors)
            {
                throw new ArgumentException($"Entidade inválida - {validation.ErrorMessage}");
            }

            return validatorResult.IsValid;
        }
    }
}
