using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CadastroUsuario.Models
{
    [Table("Enderecos")]
    public class Endereco
    {
        [Key]
        public int EnderecoId { get; set; }
        [StringLength(150)]
        public string Local { get; set; }
        [StringLength(100)]
        public string Cidade { get; set; }
        [StringLength(100)]
        public string Estado { get; set; }
    }
}