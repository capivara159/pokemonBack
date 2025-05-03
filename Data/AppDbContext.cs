using Microsoft.EntityFrameworkCore;
using PokemonApi.Models;

namespace PokemonApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Pokemon> Pokemons { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("usuarios");
            entity.Property(e => e.SenhaHash).HasColumnName("senhahash"); // Mapeamento explícito
        });
        
        modelBuilder.Entity<Pokemon>(entity => 
        {
            entity.ToTable("pokemons");
        });
    }
}