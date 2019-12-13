using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace aibot_console.Models
{
    public partial class aibotContext : DbContext
    {
        public aibotContext()
        {
        }

        public aibotContext(DbContextOptions<aibotContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Options> Options { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost;Database=ai-bot;User Id=sa;Password=F3rn4nd0;;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Options>(entity =>
            {
                entity.HasKey(e => e.OptionId);

                entity.Property(e => e.OptionId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Body)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
