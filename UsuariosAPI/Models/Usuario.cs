using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace UsuariosAPI.Models
{
    public class Usuario
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(250, ErrorMessage = "Nome excede os 250 caracteres máximos.")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail em formato inválido.")]
        public string Email { get; set; }
        [Required]
        public string CPF { get; set; }
        [Required]
        public DateTime DataNascimento { get; set; }
        public bool Status { get; set; } = true;
        public DateTime DataCriacao { get; set; }
        public DateTime DataModificacao { get; set; }
        public int EnderecoId { get; set; }

        [JsonIgnore]
        public virtual Endereco Endereco { get; set; }
    }
}
