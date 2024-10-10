using AutoMapper;
using GerenciamentoAssuntos.Domain.Entities;
using GerenciamentoAssuntos.Domain.Models;

namespace GerenciamentoAssuntos.Domain.Mappings
{
    public class DtoMappingToProfileDomain : Profile
    {
        public DtoMappingToProfileDomain()
        {
            CreateMap<Assunto, AssuntoResponseModel>();
            CreateMap<Conteudo, ConteudoResponseModel>();

        }
    }
}
