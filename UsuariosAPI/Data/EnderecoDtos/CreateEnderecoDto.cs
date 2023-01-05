using System.ComponentModel.DataAnnotations;

namespace UsuariosAPI.Data.EnderecoDtos
{
    public class CreateEnderecoDto
    {
        [Required]
        public string CEP { get; set; }
        //public string Logradouro { get; set; }
        //public string Bairro { get; set; }

        public int Numero { get; set; }
        public string Complemento { get; set; }
    }
}
