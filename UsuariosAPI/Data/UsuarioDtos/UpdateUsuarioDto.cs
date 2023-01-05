using System;
using System.ComponentModel.DataAnnotations;

namespace UsuariosAPI.Data.UsuarioDtos
{
    public class UpdateUsuarioDto
    {
        [StringLength(250, ErrorMessage = "Nome excede os 250 caracteres máximos.")]
        public string UserName { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail em formato inválido.")]
        public string Email { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public bool Status { get; set; }
        public DateTime DataModificacao { get; set; } = DateTime.Now;
        public string CEP { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
    }
}
