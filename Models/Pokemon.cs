// Models/Pokemon.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonApi.Models;

public class Pokemon
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Required]
    [Column("nome")]
    public string Nome { get; set; } = string.Empty;
    
    [Required]
    [Column("tipo")]
    public string Tipo { get; set; } = string.Empty;
    
    [Required]
    [Column("poder")]
    public int Poder { get; set; }
    
    [Required]
    [Column("nivel")]
    public int Nivel { get; set; }
}