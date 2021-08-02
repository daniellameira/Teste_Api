using CadastroUsuario.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CadastroUsuario.Controllers
{
    public class UsuarioController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetTodosUsuarios(bool incluirEndereco = false)
        {
            IList<Usuario> usuarios = null;
            using (var ctx = new AppDbContext())
            {
                usuarios = ctx.Usuarios.Include("Endereco").ToList()
                            .Select(s => new Usuario()
                            {
                                Id = s.Id,
                                Nome = s.Nome,
                                CPF = s.CPF,
                                Email = s.Email,
                                Telefone = s.Telefone,
                                Endereco = s.Endereco == null || incluirEndereco == false ? null : new Endereco()
                                {
                                    EnderecoId = s.Endereco.EnderecoId,
                                    Local = s.Endereco.Local,
                                    Cidade = s.Endereco.Cidade,
                                    Estado = s.Endereco.Estado
                                }
                            }).ToList();
            }
            if (usuarios.Count == 0)
            {
                return BadRequest("Sem usuários registrados!");
            }
            return Ok(usuarios);
        }
        public IHttpActionResult GetUsuarioPorId(int? id)
        {
            if (id == null)
                return BadRequest("O Id do usuario é inválido");

            Usuario usuario = null;
            using (var ctx = new AppDbContext())
            {
                usuario = ctx.Usuarios.Include("Endereco").ToList()
                         .Where(c => c.Id == id)
                         .Select(c => new Usuario()
                         {
                             Id = c.Id,
                             Nome = c.Nome,
                             CPF = c.CPF,
                             Email = c.Email,
                             Telefone = c.Telefone,
                             Endereco = c.Endereco == null ? null : new Endereco()
                             {
                                 EnderecoId = c.Endereco.EnderecoId,
                                 Local = c.Endereco.Local,
                                 Cidade = c.Endereco.Cidade,
                                 Estado = c.Endereco.Estado
                             }
                         }).FirstOrDefault<Usuario>();
            }
            if (usuario == null)
            {
                return BadRequest("Usuário não existe, forneça outro ID");
            }
            return Ok(usuario);
        }

        public IHttpActionResult GetUsuarioPorNome(string nome)
        {
            if (nome == null)
                return BadRequest("Nome é inválido");

            IList<Usuario> usuario = null;
            using (var ctx = new AppDbContext())
            {
                usuario = ctx.Usuarios.Include("Endereco").ToList()
                         .Where(s => s.Nome.ToLower() == nome.ToLower())
                         .Select(s => new Usuario()
                         {
                             Id = s.Id,
                             Nome = s.Nome,
                             CPF = s.CPF,
                             Email = s.Email,
                             Telefone = s.Telefone,
                             Endereco = s.Endereco == null ? null : new Endereco()
                             {
                                 EnderecoId = s.Endereco.EnderecoId,
                                 Local = s.Endereco.Local,
                                 Cidade = s.Endereco.Cidade,
                                 Estado = s.Endereco.Estado
                             }
                         }).ToList();
            }
            if (usuario.Count == 0)
            {
                return BadRequest("Nome inválido, forneça outro nome");
            }
            return Ok(usuario);
        }
        public IHttpActionResult GetUsuarioPorCpf(string cpf)
        {
            if (cpf == null)
                return BadRequest("cpf é inválido");

            IList<Usuario> usuario = null;
            using (var ctx = new AppDbContext())
            {
                usuario = ctx.Usuarios.Include("Endereco").ToList()
                         .Where(s => s.CPF.ToLower() == cpf.ToLower())
                         .Select(s => new Usuario()
                         {
                             Id = s.Id,
                             Nome = s.Nome,
                             CPF = s.CPF,
                             Email = s.Email,
                             Telefone = s.Telefone,
                             Endereco = s.Endereco == null ? null : new Endereco()
                             {
                                 EnderecoId = s.Endereco.EnderecoId,
                                 Local = s.Endereco.Local,
                                 Cidade = s.Endereco.Cidade,
                                 Estado = s.Endereco.Estado
                             }
                         }).ToList();
            }
            if (usuario.Count == 0)
            {
                return BadRequest("Cpf inválido, forneça outro Cpf");
            }
            return Ok(usuario);
        }

        public IHttpActionResult PostNovoUsuario(UsuarioEnderecoDTO usuario)
        {
            if (!ModelState.IsValid || usuario == null)
                return BadRequest("Dados do usuário inválidos.");
            using (var ctx = new AppDbContext())
            {
                ctx.Usuarios.Add(new Usuario()
                {
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    CPF = usuario.Cpf,
                    Telefone = usuario.Telefone,
                    Endereco = new Endereco()
                    {
                        Local = usuario.Local,
                        Cidade = usuario.Cidade,
                        Estado = usuario.Estado
                    }
                });
                ctx.SaveChanges();
            }
            return Ok(usuario);
        }
        public IHttpActionResult Put(Usuario usuario)
        {
            if (!ModelState.IsValid || usuario == null)
                return BadRequest("Dados do usuário inválidos");
            using (var ctx = new AppDbContext())
            {
                var usuarioSelecionado = ctx.Usuarios.Where(c => c.Id == usuario.Id)
                                                           .FirstOrDefault<Usuario>();
                if (usuarioSelecionado != null)
                {
                    usuarioSelecionado.Nome = usuario.Nome;
                    usuarioSelecionado.Email = usuario.Email;
                    usuarioSelecionado.CPF = usuario.CPF;
                    usuarioSelecionado.Telefone = usuario.Telefone;
                    ctx.Entry(usuarioSelecionado).State = EntityState.Modified;
                    var enderecoSelecionado = ctx.Enderecos.Where(e =>
                                                  e.EnderecoId == usuarioSelecionado.Endereco.EnderecoId)
                                                  .FirstOrDefault<Endereco>();
                    if (enderecoSelecionado != null)
                    {
                        enderecoSelecionado.Local = usuario.Endereco.Local;
                        enderecoSelecionado.Cidade = usuario.Endereco.Cidade;
                        enderecoSelecionado.Estado = usuario.Endereco.Estado;
                        ctx.Entry(enderecoSelecionado).State = EntityState.Modified;
                    }
                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }
            return Ok($"Usuário {usuario.Nome} atualizado com sucesso");
        }

        public IHttpActionResult Delete(int? id)
        {
            if (id == null)
                return BadRequest("Dados inválidos");
            using (var ctx = new AppDbContext())
            {
                var usuarioSelecionado = ctx.Usuarios.Where(c => c.Id == id)
                                                           .FirstOrDefault<Usuario>();
                if (usuarioSelecionado != null)
                {
                    ctx.Entry(usuarioSelecionado).State = EntityState.Deleted;
                    var enderecoSelecionado = ctx.Enderecos.Where(e =>
                                             e.EnderecoId == usuarioSelecionado.EnderecoId)
                                             .FirstOrDefault<Endereco>();
                    if (enderecoSelecionado != null)
                    {
                        ctx.Entry(enderecoSelecionado).State = EntityState.Deleted;
                    }
                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }
            return Ok($"Usuário {id} foi deletado com sucesso");
        }

    }
}