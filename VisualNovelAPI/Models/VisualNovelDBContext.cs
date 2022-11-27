using System;
using Microsoft.EntityFrameworkCore;
using VisualNovelAPI.Models;
using System.Collections.Generic;

namespace VisualNovelAPI.Models
{
    public class VisualNovelDBContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public VisualNovelDBContext(DbContextOptions<VisualNovelDBContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = Configuration.GetConnectionString("MyConnection");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        public DbSet<VisualNovel> VisualNovel { get; set; } = null!;
        public DbSet<Character> Characters { get; set; } = null!;
    }
}

