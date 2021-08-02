using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CadastroUsuario.Models
{
    [Table("Usuario")]
    public class Usuario
    {
            [Key]
            public int Id { get; set; }
            [StringLength(100)]
            public string Nome { get; set; }
            [StringLength(150)]
            public string CPF { get; set; }
            [StringLength(100)]
            public string Email { get; set; }
            [StringLength(40)]
            public string Telefone { get; set; }
            [Required]
            public virtual Endereco Endereco { get; set; }
            public virtual int EnderecoId { get; set; }
        
    }
}