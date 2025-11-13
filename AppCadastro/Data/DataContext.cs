using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppCadastro.Models;

namespace AppCadastro.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) {}

        public DbSet<Produtos> Produtos {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produtos>().HasKey(p => p.Codigo);
            modelBuilder.Entity<Produtos>().Property(p => p.Codigo).ValueGeneratedOnAdd();
        }
    }
}
