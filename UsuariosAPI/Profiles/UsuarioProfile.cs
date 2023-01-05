using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsuariosAPI.Data.UsuarioDtos;
using UsuariosAPI.Models;
using System.Linq;

namespace UsuariosAPI.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<CreateUsuarioDto, Usuario>();
            CreateMap<Usuario, IdentityUser<int>>();
            CreateMap<Usuario, CustomIdentityUser>();
            CreateMap<CustomIdentityUser, ReadUsuarioDto>();
            CreateMap<ReadUsuarioDto, CustomIdentityUser>();
            CreateMap<UpdateUsuarioDto, CustomIdentityUser>();
            CreateMap<UpdateUsuarioDto, Endereco>();
        }
    }
}
