using Microsoft.EntityFrameworkCore;
using StringReverseService.Models;
using System;

namespace StringReverseService.DBContext
{
    public class InputStringContext : DbContext
    {

        public InputStringContext(DbContextOptions<InputStringContext> options) :base(options)
        {

        }

        public DbSet<InputString> InputStrings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InputString>().HasData(
                new InputString
                {
                    Id = Guid.NewGuid(),
                   InputValue = "TestValue",
                   RequestedOn = DateTime.UtcNow
                });
        }
    }
}
