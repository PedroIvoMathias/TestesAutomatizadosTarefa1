using EnviarTrabalhos.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnviarTrabalhos.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Trabalho> Trabalhos{get;set; } 
    }
}
