using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.SqlServer
{
    public class UserPerformanceDbContext : DbContext
    {
        public UserPerformanceDbContext(DbContextOptions<UserPerformanceDbContext> options) : base(options)
        {
        }
        public DbSet<UserError> UserErrors { get; set; }
    }
}
