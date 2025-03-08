using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace UnitTestProject.Models
{
    public partial class UnitTestContext : DbContext
    {
        public UnitTestContext()
        {
        }

        public UnitTestContext(DbContextOptions<UnitTestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            optionsBuilder.UseSqlServer(config.GetConnectionString("MyCnn"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.PhoneNum)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("phoneNum");

                entity.Property(e => e.Pw)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("pw");

                entity.Property(e => e.Un)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("un");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
