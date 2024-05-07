using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace LXP.Data;

public partial class LXPDbContext : DbContext
{
    public LXPDbContext()
    {
    }

    public LXPDbContext(DbContextOptions<LXPDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Profile> Profiles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=LMS;uid=root;pwd=Password@12345", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.33-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.HasKey(e => e.ProfileId).HasName("PRIMARY");

            entity.ToTable("profile");

            entity.Property(e => e.ProfileId).HasColumnName("ProfileID");
            entity.Property(e => e.ContactNumber).HasMaxLength(15);
            entity.Property(e => e.Createdby)
                .HasMaxLength(255)
                .HasColumnName("createdby");
            entity.Property(e => e.Createddate)
                .HasColumnType("datetime")
                .HasColumnName("createddate");
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Domain).HasMaxLength(255);
            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsFixedLength();
            entity.Property(e => e.LastName).HasMaxLength(255);
            entity.Property(e => e.Updatedby).HasMaxLength(255);
            entity.Property(e => e.Updateddate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
