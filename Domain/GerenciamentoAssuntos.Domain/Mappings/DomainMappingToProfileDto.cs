using AutoMapper;
using GerenciamentoAssuntos.Domain.Entities;
using GerenciamentoAssuntos.Domain.Models;


namespace GerenciamentoAssuntos.Domain.Mappings
{
    public class DomainMappingToProfileDto : Profile
    {
        public DomainMappingToProfileDto()
        {
            CreateMap<AssuntoRequestModel, Assunto>();
            CreateMap<AssuntoResponseModel, AssuntoReqModel>();
            CreateMap<ConteudoRequestModel, Conteudo>();

        }
    }
}
