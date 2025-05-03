using Microsoft.EntityFrameworkCore;
using PokemonApi.Data;
using PokemonApi.Models;
using System.Security.Cryptography;
using System.Text;

namespace PokemonApi.Services;

public class AuthService
{
    private readonly AppDbContext _context;

    public AuthService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> Register(string email, string password)
    {
        // Verifica se o email já existe
        if (await _context.Users.AnyAsync(u => u.Email == email))
            return null;

        var user = new User
        {
            Email = email,
            SenhaHash = HashPassword(password)
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> Login(string email, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        if (user == null || !VerifyPassword(password, user.SenhaHash))
            return null;

        return user;
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }

    private bool VerifyPassword(string password, string storedHash)
    {
        return HashPassword(password) == storedHash;
    }
}