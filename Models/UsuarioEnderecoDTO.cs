﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CadastroUsuario.Models
{
    public class UsuarioEnderecoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public string Local { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
    }
}