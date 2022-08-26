﻿using Microsoft.EntityFrameworkCore;
using System.Net;
using TestMvcProject.Jikan;
using TestMvcProject.Models;

namespace TestMvcProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Position>().HasIndex(u => u.Name).IsUnique();
            modelBuilder.Entity<Genre>().HasIndex(g => g.Name).IsUnique();
        }

        public DbSet<Anime> Animies { get; set; }
        public DbSet<Manga> Mangas { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }

}
