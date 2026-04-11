using Microsoft.AspNetCore.Identity;
using RetailOrderWebsite.Data;
using RetailOrderWebsite.DTOs;
using RetailOrderWebsite.Helper;
using RetailOrderWebsite.Models;
using RetailOrderWebsite.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace RetailOrderWebsite.Services.Implementations
{
    public class AuthService:IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher<User> _hasher;
        private readonly JWTHelper _jwt;

        public AuthService(AppDbContext context,
                           IPasswordHasher<User> hasher,
                           JWTHelper jwt)
        {
            _context = context;
            _hasher = hasher;
            _jwt = jwt;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            bool emailExists = await _context.Users
                .AnyAsync(x => x.Username.ToLower() == dto.Username.ToLower());

            if (emailExists)
                throw new Exception("An account with this email already exists.");

            var user = new User
            {
                Username = dto.Username.ToLower(),
                Role = dto.Role
            };

            user.Password = _hasher.HashPassword(user, dto.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new AuthResponseDto
            {
                Username = user.Username,
                Role = user.Role,
                Token = _jwt.GenerateToken(user)
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Username.ToLower() == dto.Username.ToLower());

            if (user == null)
                throw new Exception("Invalid email or password.");

            var result = _hasher.VerifyHashedPassword(user, user.Password, dto.Password);

            if (result == PasswordVerificationResult.Failed)
                throw new Exception("Invalid email or password.");

            return new AuthResponseDto
            {
                Username = user.Username,
                Role = user.Role,
                Token = _jwt.GenerateToken(user)
            };
        }
    }
}
