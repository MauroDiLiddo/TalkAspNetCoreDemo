using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TalkAspNetCoreDemo.Data.Models;

namespace TalkAspNetCoreDemo.Data
{
    public class TalkDbContext : DbContext
    {
        public TalkDbContext(DbContextOptions<TalkDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Lesson> Lessons { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<MenuItem> MenuItems { get; set; }
    }
}
