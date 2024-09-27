using FlightProjectManagementApi.DataModels;
using Microsoft.AspNetCore.Mvc;

namespace FlightProjectManagementApi.Services
{
    public class AuthService
    {

        private readonly FlightProjectAdminContext _context;

        public AuthService(FlightProjectAdminContext context)
        {
            _context = context;
        }

        public IActionResult Register(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Password))
            {
                return new BadRequestObjectResult(new { message = "Şifre zorunludur." });
            }

            _context.Users.Add(user);
            _context.SaveChanges();
            return new OkObjectResult(new { message = "Kayıt başarılı!" });
        }

        public IActionResult Login(LoginModel model)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == model.Email);
            if (user == null || user.Password != model.Password)
            {
                return new UnauthorizedObjectResult(new { message = "Geçersiz e-posta veya şifre." });
            }

            return new OkObjectResult(new
            {
                message = "Giriş başarılı!",
                userId = user.Id,
                email = user.Email,
                firstName = user.FirstName,
                lastName = user.LastName
            });
        }
    }
}
