using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace toDoApp.Models;

public partial class TodoappdbContext : DbContext
{
    public TodoappdbContext()
    {
    }

    public TodoappdbContext(DbContextOptions<TodoappdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tags_pkey");

            entity.ToTable("tags");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Tag1)
                .HasMaxLength(25)
                .HasColumnName("tag");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tasks_pkey");

            entity.ToTable("tasks");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Isfinish)
                .HasDefaultValue(false)
                .HasColumnName("isfinish");
            entity.Property(e => e.Task1)
                .HasMaxLength(500)
                .HasColumnName("task");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");

            entity.HasMany(d => d.Tags).WithMany(p => p.Tasks)
                .UsingEntity<Dictionary<string, object>>(
                    "Tagoftask",
                    r => r.HasOne<Tag>().WithMany()
                        .HasForeignKey("TagId")
                        .HasConstraintName("tagoftask_tag_id_fkey"),
                    l => l.HasOne<Task>().WithMany()
                        .HasForeignKey("TaskId")
                        .HasConstraintName("tagoftask_task_id_fkey"),
                    j =>
                    {
                        j.HasKey("TaskId", "TagId").HasName("tagoftask_pkey");
                        j.ToTable("tagoftask");
                        j.IndexerProperty<long>("TaskId").HasColumnName("task_id");
                        j.IndexerProperty<long>("TagId").HasColumnName("tag_id");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
