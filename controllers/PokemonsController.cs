using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonApi.Data;
using PokemonApi.Models;

namespace PokemonApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class PokemonsController : ControllerBase
{
    private readonly AppDbContext _context;

    public PokemonsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/Pokemons
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pokemon>>> GetPokemons()
    {
        return await _context.Pokemons.ToListAsync();
    }

    // GET: api/Pokemons/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Pokemon>> GetPokemon(int id)
    {
        var pokemon = await _context.Pokemons.FindAsync(id);

        if (pokemon == null)
        {
            return NotFound();
        }

        return pokemon;
    }

    // PUT: api/Pokemons/5
   [HttpPut("{id}")]
    public async Task<IActionResult> PutPokemon(int id, [FromBody] Pokemon pokemon)
    {
        if (id != pokemon.Id)
        {
            return BadRequest("ID do Pokémon não corresponde");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.Entry(pokemon).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
            return Ok(pokemon); // Retorna o Pokémon atualizado
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PokemonExists(id))
            {
                return NotFound();
            }
            throw;
        }
    }

    // POST: api/Pokemons
    [HttpPost]
    public async Task<ActionResult<Pokemon>> PostPokemon(Pokemon pokemon)
    {
        _context.Pokemons.Add(pokemon);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetPokemon", new { id = pokemon.Id }, pokemon);
    }

    // DELETE: api/Pokemons/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePokemon(int id)
    {
        var pokemon = await _context.Pokemons.FindAsync(id);
        if (pokemon == null)
        {
            return NotFound();
        }

        _context.Pokemons.Remove(pokemon);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool PokemonExists(int id)
    {
        return _context.Pokemons.Any(e => e.Id == id);
    }
}