using Microsoft.EntityFrameworkCore;
using PokemonApi.Data;
using PokemonApi.Models;

namespace PokemonApi.Services;

public class PokemonService
{
    private readonly AppDbContext _context;

    public PokemonService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Pokemon>> GetAllPokemons()
    {
        return await _context.Pokemons.ToListAsync();
    }

    public async Task<Pokemon?> GetPokemonById(int id)
    {
        return await _context.Pokemons.FindAsync(id);
    }

    public async Task<Pokemon> CreatePokemon(Pokemon pokemon)
    {
        _context.Pokemons.Add(pokemon);
        await _context.SaveChangesAsync();
        return pokemon;
    }

    public async Task UpdatePokemon(int id, Pokemon pokemon)
    {
        var existingPokemon = await _context.Pokemons.FindAsync(id);
        if (existingPokemon != null)
        {
            existingPokemon.Nome = pokemon.Nome;
            existingPokemon.Tipo = pokemon.Tipo;
            existingPokemon.Poder = pokemon.Poder;
            existingPokemon.Nivel = pokemon.Nivel;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeletePokemon(int id)
    {
        var pokemon = await _context.Pokemons.FindAsync(id);
        if (pokemon != null)
        {
            _context.Pokemons.Remove(pokemon);
            await _context.SaveChangesAsync();
        }
    }
}