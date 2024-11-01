using Microsoft.EntityFrameworkCore;
using salaoDaLora.Models;

namespace salaoDaLora.Data
{
    public class SalaoDaLoraContext : DbContext //herança | DbContexté uma classe base para comuicação com o database
    {
        //construtor
        public SalaoDaLoraContext(DbContextOptions<SalaoDaLoraContext> options)
            : base(options)
        {
        }

        //DbStes | cada um representa uma tabela que pode ser lida e modicada
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Profissional> Profissionais { get; set; }
        public DbSet<Servico> Servicos { get; set; }
        public DbSet<Agendamento> Agendamentos { get; set; }

        //configração do modelo
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().ToTable("Cliente");
            modelBuilder.Entity<Profissional>().ToTable("Profissional");
            modelBuilder.Entity<Servico>().ToTable("Servico");
            modelBuilder.Entity<Agendamento>().ToTable("Agendamento");
        }
    }
}
