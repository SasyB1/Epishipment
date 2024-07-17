using Epishipment.Models;
using Epishipment.Models.Dto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Epishipment.Services
{
    public class AuthService
    {
        private readonly IHttpContextAccessor _http;
        public AuthService(IHttpContextAccessor httpContextAccessor)
        {
            _http = httpContextAccessor;
        }

        public async Task Login(User user)
        {
            var claims = new List<Claim>()
           {
                               new Claim(ClaimTypes.Name, user.Username),
                               new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
           };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties();
            await _http.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
           
        }

        public User Logout()
        {
            throw new NotImplementedException();
        }

        public int? GetCurrentUserId()
        {
            var user = _http.HttpContext.User;

            // Controlla se l'utente è autenticato
            if (user == null || !user.Identity.IsAuthenticated)
            {
                return null;
            }

            // Trova il claim dell'ID utente
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Prova a convertire il claim in un intero
            if (userIdClaim == null || !int.TryParse(userIdClaim, out var userId))
            {
                return null;
            }

            return userId;
        }

    }
}
