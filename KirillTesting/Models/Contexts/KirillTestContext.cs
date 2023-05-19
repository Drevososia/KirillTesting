using System;
using System.Collections.Generic;
using KirillTesting.Models.Sprs;
using Microsoft.EntityFrameworkCore;

namespace KirillTesting.Models.Contexts;

public partial class KirillTestContext : DbContext
{
    public KirillTestContext()
    {
    }

    public KirillTestContext(DbContextOptions<KirillTestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Person> Persons { get; set; }

    public virtual DbSet<Policy> Policies { get; set; }

    public virtual DbSet<PolicyType> PolicyTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=192.168.0.101; Port=5432; User Id=postgres; Password=ySEvBiId; Database=KirillTest; CommandTimeout=300");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Genders_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasColumnType("character varying");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Persons_pkey");
            entity.Property(e => e.Firstname).HasColumnType("character varying");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Lastname).HasColumnType("character varying");
            entity.Property(e => e.Patronymic).HasColumnType("character varying");

            entity.HasOne(d => d.Policy).WithOne(x => x.Person)
                .HasForeignKey<Policy>(d => d.PersonId)
                .HasConstraintName("fk_PersonId_Persons");
        });

        modelBuilder.Entity<Policy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Policies_pkey");

            entity.Property(e => e.Enp).HasColumnType("character varying");
            entity.Property(e => e.Number).HasColumnType("character varying");
            entity.Property(e => e.Serial).HasColumnType("character varying");

            entity.HasOne(d => d.TypeNavigation).WithMany(p => p.Policies)
                .HasForeignKey(d => d.Type)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Type_PolicyTypes");
        });

        modelBuilder.Entity<PolicyType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PolicyTypes_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasColumnType("character varying");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
