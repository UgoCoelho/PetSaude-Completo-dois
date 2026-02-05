using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PetSaude_Completo.Models;

namespace PetSaude_Completo.PetSaude.Banco
{
    public class PetSaude_CompletoContext : DbContext
    {
        public PetSaude_CompletoContext(DbContextOptions<PetSaude_CompletoContext> options)
            : base(options)
        {
        }

        public DbSet<Area> Area { get; set; } = default!;
        public DbSet<Categoria> Categoria { get; set; } = default!;
        public DbSet<Comorbidade> Comorbidade { get; set; } = default!;
        public DbSet<Frequencia> Frequencia { get; set; } = default!;
        public DbSet<Mensagem> Mensagem { get; set; } = default!;
        public DbSet<Paciente> Pacientes { get; set; }


        // 🔥 TABELA DE JUNÇÃO
        public DbSet<MensagemComorbidade> MensagemComorbidade { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MensagemComorbidade>()
                .HasKey(mc => new { mc.MensagemId, mc.ComorbidadeId });

            modelBuilder.Entity<MensagemComorbidade>()
                .HasOne(mc => mc.Mensagem)
                .WithMany(m => m.MensagemComorbidades)
                .HasForeignKey(mc => mc.MensagemId);

            modelBuilder.Entity<MensagemComorbidade>()
                .HasOne(mc => mc.Comorbidade)
                .WithMany(c => c.MensagemComorbidades)
                .HasForeignKey(mc => mc.ComorbidadeId);
        }
    }
}