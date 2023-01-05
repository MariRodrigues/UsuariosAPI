using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using UsuariosAPI.Data.Requests;
using UsuariosAPI.Models;


namespace UsuariosAPI.Services
{
    public class LoginService
    {
        private readonly SignInManager<CustomIdentityUser> _signInManager;
        private readonly TokenService _tokenService;

        public LoginService(SignInManager<CustomIdentityUser> signInManager, TokenService tokenService)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public Result LogaUsuario(LoginRequest request)
        {
            var usuario = _signInManager.UserManager.FindByEmailAsync(request.Email);

            if (usuario.Result == null) return Result.Fail("E-mail incorreto!");

            var resultadoIdentity = _signInManager
                .PasswordSignInAsync(usuario.Result.UserName, request.Password, false, false);

            if (!resultadoIdentity.Result.Succeeded)
            {
                return Result.Fail("Login falhou");
            }

            var identityUser = _signInManager
                    .UserManager
                    .Users
                    .FirstOrDefault(user =>
                    user.NormalizedUserName == usuario.Result.UserName.ToUpper());

            if (identityUser.Status != true) return Result.Fail("Usuário inativo");

            Token token = _tokenService.CreateToken(identityUser,
                _signInManager.UserManager.GetRolesAsync(identityUser).Result.FirstOrDefault());
            
            return Result.Ok().WithSuccess(token.Value);
        }
    }
}
