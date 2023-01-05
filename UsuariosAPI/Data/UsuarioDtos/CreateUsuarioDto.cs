using System;
using System.ComponentModel.DataAnnotations;

namespace UsuariosAPI.Data.UsuarioDtos
{
    public class CreateUsuarioDto
    {
        [Required]
        [StringLength(250, ErrorMessage = "Nome excede os 250 caracteres máximos.")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail em formato inválido.")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "As senhas não conferem.")]
        public string Repassword { get; set; }
        [Required]
        public string CPF { get; set; }
        [Required]
        public DateTime DataNascimento { get; set; }
        public bool Status { get; set; } = true;
        public DateTime DataCriacao { get; set; } = DateTime.Now;

        public int EnderecoId { get; set; }
    }
}
