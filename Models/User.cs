// Models/User.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokemonApi.Models;

[Table("usuarios")]
public class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Column("email")]
    public string Email { get; set; } = string.Empty;
    
    [Column("senhahash")] // Nome EXATO da coluna no banco
    public string SenhaHash { get; set; } = string.Empty;
}