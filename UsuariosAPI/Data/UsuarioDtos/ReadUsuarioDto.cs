using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UsuariosAPI.Models;

namespace UsuariosAPI.Data.UsuarioDtos
{
    public class ReadUsuarioDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public bool Status { get; set; }
        public DateTime DataCriacao { get; set; }

        public string Perfil { get; set; }
        public Endereco Endereco { get; set; }
    }
}
