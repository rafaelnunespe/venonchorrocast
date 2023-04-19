using System.Data.Entity;
using System.IO;
using System.Web;

namespace Venonchorrocast.Models
{
    public partial class ConexaoBancoDeDados : DbContext
    {
        public ConexaoBancoDeDados()
            : base("name=ConexaoBancoDeDados")
        {
        }

        public virtual DbSet<Comentario> Comentario { get; set; }
        public virtual DbSet<Episodio> Episodio { get; set; }
        public virtual DbSet<Ouvinte> Ouvinte { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comentario>()
                .Property(e => e.Conteudo)
                .IsUnicode(false);

            modelBuilder.Entity<Episodio>()
                .Property(e => e.Titulo)
                .IsUnicode(false);

            modelBuilder.Entity<Episodio>()
                .Property(e => e.Descriçao)
                .IsUnicode(false);

            modelBuilder.Entity<Episodio>()
                .Property(e => e.Link)
                .IsUnicode(false);

            modelBuilder.Entity<Episodio>()
                .Property(e => e.Qntde_Play)
                .HasPrecision(10, 0);

            modelBuilder.Entity<Episodio>()
                .Property(e => e.Qntde_Download)
                .HasPrecision(10, 0);

            modelBuilder.Entity<Episodio>()
                .Property(e => e.Participantes)
                .IsUnicode(false);

            modelBuilder.Entity<Episodio>()
                .HasMany(e => e.Comentario)
                .WithOptional(e => e.Episodio)
                .HasForeignKey(e => e.fk_id_episodio);

            modelBuilder.Entity<Ouvinte>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Ouvinte>()
                .Property(e => e.Apelido)
                .IsUnicode(false);

            modelBuilder.Entity<Ouvinte>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Ouvinte>()
                .Property(e => e.Senha)
                .IsUnicode(false);

            modelBuilder.Entity<Ouvinte>()
                .HasMany(e => e.Comentario)
                .WithOptional(e => e.Ouvinte)
                .HasForeignKey(e => e.fk_id_ouvinte);
        }

    }
}
