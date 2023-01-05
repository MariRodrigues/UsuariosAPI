using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using UsuariosAPI.Data.EnderecoDtos;
using UsuariosAPI.Data.UsuarioDtos;
using UsuariosAPI.Services;

namespace UsuariosAPI.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cadastra usuário",
                          OperationId = "Post")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [AllowAnonymous]
        public async Task<IActionResult> CadastrarUsuario([FromBody] CreateUsuarioDto userDto)
        {
            var resultado = await _usuarioService.CadastrarUsuario(userDto);
            if (resultado.IsFailed) return StatusCode(400);

            return Ok();
        }

        [HttpPost("endereco")]
        [SwaggerOperation(Summary = "Cadastra endereço",
                          OperationId = "Post")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [AllowAnonymous]
        public async Task<IActionResult> CadastrarEndereco([FromBody] CreateEnderecoDto enderecoDto)
        {
            var resultado = await _usuarioService.CadastrarEndereco(enderecoDto);
            if (resultado.IsFailed) return StatusCode(400);

            return Ok();
        }

        [HttpPost("cadastrarRole/{id}")]
        [SwaggerOperation(Summary = "Cadastra role",
                          OperationId = "Post")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CadastrarRole(int id, [FromQuery] string role)
        {
            var retornoRole = await _usuarioService.CadastrarRoleUsuario(id, role);
            if (!retornoRole.IsSuccess) return StatusCode(400);
            return Ok();
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Lista usuários cadastrados",
                          OperationId = "Get")]
        [ProducesResponseType(200)]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ListarUsuarios([FromQuery] string nome, 
            [FromQuery] string cpf, [FromQuery] string email, [FromQuery] bool? status)
        {
            var resultado = await _usuarioService.ListarUsuarios(nome, cpf, email, status);
            return Ok(resultado);
        }

        [HttpPut("editar/{id}")]
        [SwaggerOperation(Summary = "Edita usuário por id",
                          OperationId = "Put")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Authorize(Roles = "cliente")]
        public async Task<IActionResult> EditarUsuario(int id, [FromBody] UpdateUsuarioDto userDto)
        {
            var retornoRole = await _usuarioService.EditarUsuario(id, userDto);
            if (!retornoRole.IsSuccess) return StatusCode(400);
            return Ok();
        }

    }
}
