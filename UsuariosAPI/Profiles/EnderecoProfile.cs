using AutoMapper;
using UsuariosAPI.Data.EnderecoDtos;
using UsuariosAPI.Models;

namespace UsuariosAPI.Profiles
{
    public class EnderecoProfile : Profile
    {
        public EnderecoProfile()
        {
            CreateMap<CreateEnderecoDto, Endereco>();
            CreateMap<ReadEnderecoJson, CreateEnderecoDto>();
            CreateMap<ReadEnderecoJson, Endereco>();
        }
    }
}
