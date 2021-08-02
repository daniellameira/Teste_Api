using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CadastroUsuario.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("TestePratico")
        { }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
    }
}