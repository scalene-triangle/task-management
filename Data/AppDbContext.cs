using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using task_management.Models;

namespace task_management.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public override int SaveChanges()
        {

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry is { State: EntityState.Modified, Entity: GenericRecord entity })
                {
                    entity.UpdatedAt = DateTime.UtcNow;
                }
  
            }

            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Story>()
                .HasMany(s => s.MyTasks) 
                .WithOne(t => t.Story)   
                .HasForeignKey(t => t.StoryId) 
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Project>()
                .HasMany(p => p.MyTasks) 
                .WithOne(t => t.Project)  
                .HasForeignKey(t => t.ProjectId) 
                .OnDelete(DeleteBehavior.SetNull); 
        }

        public DbSet<task_management.Models.MyTask> Task { get; set; } = default!;

        public DbSet<task_management.Models.Story> Story { get; set; } = default!;
    }
}
