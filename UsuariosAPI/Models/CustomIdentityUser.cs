using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UsuariosAPI.Models
{
    public class CustomIdentityUser : IdentityUser<int>
    {
        [Required]
        public string CPF { get; set; }
        [Required]
        public DateTime DataNascimento { get; set; }
        public bool Status { get; set; } = true;
        public DateTime DataCriacao { get; set; }
        public DateTime DataModificacao { get; set; }
        [JsonIgnore]
        public virtual Endereco Endereco { get; set; }
        public int EnderecoId { get; set; }


    }
}
