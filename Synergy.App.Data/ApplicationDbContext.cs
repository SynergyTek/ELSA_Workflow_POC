﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Synergy.App.Data.Model;

namespace Synergy.App.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<User, Role, Guid>(options)
{
    public DbSet<WorkflowModel> Workflow { get; set; }
    public DbSet<TemplateModel> Template { get; set; }
    public DbSet<TableModel> Table { get; set; }
    public DbSet<ColumnModel> Column { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>(entity => { entity.ToTable("User"); });
        modelBuilder.Entity<Role>(entity => { entity.ToTable("Role"); });
        modelBuilder.Entity<IdentityRoleClaim<Guid>>(entity => { entity.ToTable("RoleClaim"); });
        modelBuilder.Entity<IdentityUserClaim<Guid>>(entity => { entity.ToTable("UserClaim"); });
        modelBuilder.Entity<IdentityUserLogin<Guid>>(entity => { entity.ToTable("UserLogin"); });
        modelBuilder.Entity<IdentityUserToken<Guid>>(entity => { entity.ToTable("UserToken"); });
        modelBuilder.Entity<IdentityUserRole<Guid>>(entity => { entity.ToTable("UserRole"); });

    }
}