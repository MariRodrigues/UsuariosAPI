using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UsuariosAPI.Data;
using UsuariosAPI.Data.EnderecoDtos;
using UsuariosAPI.Data.UsuarioDtos;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class UsuarioService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<CustomIdentityUser> _userManager;
        private readonly UserDbContext _userDbContext;

        static readonly HttpClient client = new HttpClient();

        public UsuarioService(IMapper mapper, UserManager<CustomIdentityUser> userManager, UserDbContext userDbContext)
        {
            _mapper = mapper;
            _userManager = userManager;
            _userDbContext = userDbContext;
        }

        public async Task<Result> CadastrarUsuario(CreateUsuarioDto userDto)
        {
            Usuario usuario = _mapper.Map<Usuario>(userDto);
            CustomIdentityUser usuarioIdentity = _mapper.Map<CustomIdentityUser>(usuario);

            if (ValidaDataNascimento(usuarioIdentity) == false 
                || ValidaCPF(usuarioIdentity.CPF) == false) return Result.Fail("Falha no cadastro do usuário");

            var resultadoIdentity = await _userManager.CreateAsync(usuarioIdentity, userDto.Password);

            await _userManager.AddToRoleAsync(usuarioIdentity, "cliente");

            if (resultadoIdentity.Succeeded) return Result.Ok();
            return Result.Fail("Falha ao cadastrar usuario!");
        }

        public async Task<Result> CadastrarEndereco(CreateEnderecoDto enderecoDto)
        {
            enderecoDto.CEP = enderecoDto.CEP.Replace("-", "");

            if (ValidaCEP(enderecoDto.CEP) == false) return Result.Fail("CEP no formato incorreto");

            var enderecoJson = await RetornarEnderecoViaCEP(enderecoDto.CEP);

            if (enderecoJson == null) return Result.Fail("Não houve retorno");

            Endereco endereco = _mapper.Map<Endereco>(enderecoJson);

            endereco.Numero = enderecoDto.Numero;
            endereco.Complemento = enderecoDto.Complemento;

            _userDbContext.Enderecos.Add(endereco);
            _userDbContext.SaveChanges();

            return Result.Ok();
        }

        public async Task<Result> CadastrarRoleUsuario(int id, string role)
        {
            var usuario = _userManager.Users.FirstOrDefault(x => x.Id == id);

            var result = await _userManager.AddToRoleAsync(usuario, role);

            if (!result.Succeeded) return Result.Fail("Falha ao inserir role");
            return Result.Ok();

        }

        public async Task<List<ReadUsuarioDto>> ListarUsuarios(string nome, string cpf, string email, bool? status)
        {
            var usuarios = await _userManager.Users.ToListAsync();

            List<ReadUsuarioDto> usuariosDto = new();

            foreach (var user in usuarios)
            {
                var usuarioDto = _mapper.Map<ReadUsuarioDto>(user);
                usuarioDto.Perfil = _userManager.GetRolesAsync(user).Result.FirstOrDefault();
                usuariosDto.Add(usuarioDto);
            }

            if (nome != null)
            {
                return usuariosDto.Where(u => u.UserName.ToLower().Contains(nome.ToLower())).ToList();
            }
            if (cpf != null)
            {
                return usuariosDto.Where(u => u.CPF == cpf).ToList();
            }
            if (email != null)
            {
                return usuariosDto.Where(u => u.Email == email).ToList();
            }
            if (status != null)
            {
                return usuariosDto.Where(u => u.Status == status).ToList();
            }

            return usuariosDto;
        }

        public async Task<Result> EditarUsuario(int id, UpdateUsuarioDto usuarioDto)
        {
            var usuarioAtualizar = _userManager.Users.FirstOrDefault(c => c.Id == id);

            if (usuarioAtualizar == null)
            {
                return Result.Fail("Usuário não encontrado");
            }

            var enderecoAtualizar = _userDbContext.Enderecos.FirstOrDefault(c => c.Id == usuarioAtualizar.EnderecoId);

            if (ValidaCEP(usuarioDto.CEP) == false)
            {
                return Result.Fail("CEP no formato incorreto");
            }

            var enderecoJson = await RetornarEnderecoViaCEP(usuarioDto.CEP);

            enderecoAtualizar.CEP = enderecoJson.CEP;
            enderecoAtualizar.Bairro = enderecoJson.Bairro;
            enderecoAtualizar.Logradouro = enderecoJson.Logradouro;

            _mapper.Map(usuarioDto, usuarioAtualizar);
            _mapper.Map(usuarioDto, enderecoAtualizar);

            await _userManager.UpdateAsync(usuarioAtualizar);
            await _userDbContext.SaveChangesAsync();

            return Result.Ok();
        }

        private static async Task<ReadEnderecoJson> RetornarEnderecoViaCEP(string cep)
        {
            string url = "https://viacep.com.br/ws/" + cep + "/json/";

            HttpResponseMessage response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var enderecoJson = JsonSerializer.Deserialize<ReadEnderecoJson>(responseBody, options);

            if (enderecoJson.Erro == "true")
            {
                return null;
            }

            return enderecoJson;
        }

        private static bool ValidaDataNascimento(CustomIdentityUser user)
        {
            if (user.DataNascimento > DateTime.Today) return false;
            return true;
        }

        private static bool ValidaCPF(string cpf)
        {
            Console.WriteLine("Valida CPF");
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf;
            string digito;
            int soma;
            int resto;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            Console.WriteLine("Tamanho: " + cpf.Length);
            if (cpf.Length != 11) { return false; }

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            }

            resto = soma % 11;

            if (resto < 2) { resto = 0; }
            else { resto = 11 - resto; }

            digito = resto.ToString();

            tempCpf += digito;

            soma = 0;

            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            }     

            resto = soma % 11;

            if (resto < 2) { resto = 0; }
            else { resto = 11 - resto; }

            digito += resto.ToString();

            return cpf.EndsWith(digito);
        }
        private static bool ValidaCEP(string cep)
        {
            if (cep.Length < 8 || cep.Length > 8 || !Regex.IsMatch(cep, @"^[0-9]+$"))
            {
                return false;
            }
            return true;
        }
    }
}
