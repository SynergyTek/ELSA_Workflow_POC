using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;
using Synergy.App.Data.Models;

namespace Synergy.App.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<User, Role, Guid>(options)
{
    public DbSet<Workflow> Workflow { get; set; }
    public DbSet<Notification> Notification { get; set; }
    public DbSet<Leave> Leave { get; set; }
    public DbSet<TableMetadata> TableMetadata { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Workflow>()
            .HasOne(w => w.AssignedToUser)
            .WithMany()
            .HasForeignKey(w => w.AssignedToUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Workflow>()
            .HasOne(w => w.AssignedByUser)
            .WithMany()
            .HasForeignKey(w => w.AssignedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Workflow>()
            .Property(r => r.Status)
            .HasConversion<string>();
        modelBuilder.Entity<Leave>()
            .Property(r => r.LeaveType)
            .HasConversion<string>();
    }
}