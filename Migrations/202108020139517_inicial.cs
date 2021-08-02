namespace CadastroUsuario.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Enderecos",
                c => new
                    {
                        EnderecoId = c.Int(nullable: false, identity: true),
                        Local = c.String(maxLength: 150),
                        Cidade = c.String(maxLength: 100),
                        Estado = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.EnderecoId);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(maxLength: 100),
                        CPF = c.String(maxLength: 150),
                        Email = c.String(maxLength: 100),
                        Telefone = c.String(maxLength: 40),
                        EnderecoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Enderecos", t => t.EnderecoId, cascadeDelete: true)
                .Index(t => t.EnderecoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuario", "EnderecoId", "dbo.Enderecos");
            DropIndex("dbo.Usuario", new[] { "EnderecoId" });
            DropTable("dbo.Usuario");
            DropTable("dbo.Enderecos");
        }
    }
}
