using EmprestimoLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace EmprestimoLibrary.Data
{
    public class ControleEmprestimoLivroContext : DbContext
    {
        public ControleEmprestimoLivroContext (DbContextOptions<ControleEmprestimoLivroContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Emprestimo> Emprestimos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente);

                entity.Property(e => e.IdCliente).
                HasColumnName("id_cliente");

                entity.Property(e => e.NomeCliente)
                .HasColumnName("nome_cliente")
                .IsRequired()
                .HasMaxLength(100);

                entity.Property(e => e.CpfCliente)
                .HasColumnName("cpf_cliente")
                .IsRequired()
                .HasMaxLength(11);

                entity.Property(e => e.EnderecoCliente)
                .HasColumnName("endereco_cliente")
                .IsRequired()
                .HasMaxLength(200);

                entity.Property(e => e.TelefoneCliente)
                .HasColumnName("telefone_cliente")
                .IsRequired()
                .HasMaxLength(14);

                entity.HasIndex(e => e.IdCliente)
                .IsUnique();
            });
            modelBuilder.Entity<Livro>(entity =>
            {
                entity.ToTable("Livros");

                entity.HasKey(e => e.IdLivro);
                entity.Property(e => e.IdLivro)
                .HasColumnName("id_livro");

                entity.Property(e => e.LivroTitulo)
                .HasColumnName("livro_titulo")
                .IsRequired()
                .HasMaxLength(200);

                entity.Property(e => e.LivroAutor)
                .HasColumnName("livro_autor")
                .IsRequired()
                .HasMaxLength(100);

                entity.Property(e => e.LivroEditora)
                .HasColumnName("livro_editora")
                .IsRequired()
                .HasMaxLength(100);
                entity.Property(e => e.LivroEdicao)
                .HasColumnName("livro_edicao")
                .IsRequired()
                .HasMaxLength(50);

                entity.Property(e => e.LivroQuantidade)
                .HasColumnName("livro_quantidade")
                .IsRequired();
            });

            modelBuilder.Entity<Emprestimo>(entity =>
            {
                entity.ToTable("LIVRO_CLIENTE_EMPRESTIMO");

                entity.HasKey(e => e.IdEmprestimo);

                entity.Property(e => e.IdEmprestimo)
                .HasColumnName("id_emprestimo");

                entity.Property(e => e.IdCliente)
                .HasColumnName("lce_id_cliente")
                .IsRequired();

                entity.Property(e => e.IdLivro)
                .HasColumnName("lce_id_livro")
                .IsRequired();

                entity.Property(e => e.DataEmprestimo)
                .HasColumnName("lce_dataEmprestimo")
                .IsRequired();

                entity.Property(e => e.DataDevolucao)
                .HasColumnName("lce_dataDevolucao");

                entity.Property(e => e.Devolvido)
                .HasColumnName("lce_devolvido")
                .IsRequired();

                //relacionamento
                //um emprestimo tem 1 cliente e 1 cliente pode ter muitos emprestimos.
                entity.HasOne<Cliente>()
                .WithMany()
                .HasForeignKey(e => e.IdCliente)
                .OnDelete(DeleteBehavior.Restrict);

                //um emprestimo tem 1 livro e 1 livro pode ter muitos emprestimos.
                entity.HasOne<Livro>()
                .WithMany()
                .HasForeignKey(e => e.IdLivro)
                //on delete para o banco não apagar historico de emprestimos caso o
                //livro seja apagado, para manter o historico de emprestimos.
                .OnDelete(DeleteBehavior.Restrict);
            });
        }

    }
}
