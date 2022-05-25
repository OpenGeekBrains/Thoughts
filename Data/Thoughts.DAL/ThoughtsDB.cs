﻿
using Microsoft.EntityFrameworkCore;
using Thoughts.DAL.Entities;
using Thoughts.DAL.Entities.DefaultData;

namespace Thoughts.DAL;

public class ThoughtsDB:DbContext
{
    #region DbSet
    public DbSet<User> Users { get; set; } = null!;

    public DbSet<Role> Roles { get; set; } = null!;

    public DbSet<Status> Statuses { get; set; } = null!;

    public DbSet<Category> Categories { get; set; } = null!;

    public DbSet<Comment> Comments { get; set; } = null!;

    public DbSet<Post> Posts { get; set; } = null!;

    public DbSet<Tag> Tags { get; set; } = null!;
    #endregion

    public ThoughtsDB(DbContextOptions<ThoughtsDB> options) : base(options){ }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Status>().HasData(GetDefaultData.DefaultStatus());
        modelBuilder.Entity<Role>().HasData(GetDefaultData.DefaultRole());
    }



}