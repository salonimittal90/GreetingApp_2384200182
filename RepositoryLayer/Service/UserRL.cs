using ModelLayer.Model.UserModel;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
       
            private readonly HelloGreetingContext _dbContext;

            public UserRL(HelloGreetingContext dbContext)
            {
                _dbContext = dbContext;
            }

            // ✅ Register Logic
            public UserDetailsEntity Register(RegisterModel model)
            {
                // Check if user already exists
                if (_dbContext.UserDetails.Any(u => u.Email == model.Email))
                {
                    throw new Exception("User with this email already exists!");
                }

                // Generate Salt & Hash Password
                string salt = GenerateSalt();
                string hashedPassword = HashPassword(model.Password, salt);

            // Create new UserDetailsEntity
            var user = new UserDetailsEntity
            {
                    Name = model.Name,
                    Email = model.Email,
                    PasswordHash = hashedPassword,
                    Salt = salt
                };

                _dbContext.UserDetails.Add(user);
                _dbContext.SaveChanges();
                return user;
            }

            // ✅ Login Logic
            public UserDetailsEntity Login(LoginModel model)
            {
                var user = _dbContext.UserDetails.FirstOrDefault(u => u.Email == model.Email);
                if (user == null)
                {
                    throw new Exception("User not found!");
                }

                // Verify Password
                string hashedInputPassword = HashPassword(model.Password, user.Salt);
                if (hashedInputPassword != user.PasswordHash)
                {
                    throw new Exception("Invalid password!");
                }

                return user;  // (JWT baad me implement hoga)
            }

            // 🔒 Generate Salt
            private string GenerateSalt()
            {
                byte[] saltBytes = new byte[16];
                using (var rng = new RNGCryptoServiceProvider())
                {
                    rng.GetBytes(saltBytes);
                }
                return Convert.ToBase64String(saltBytes);
            }

            // 🔒 Hash Password with Salt
            private string HashPassword(string password, string salt)
            {
                byte[] saltBytes = Convert.FromBase64String(salt);
                using (var rfc2898 = new Rfc2898DeriveBytes(password, saltBytes, 10000))
                {
                    return Convert.ToBase64String(rfc2898.GetBytes(32));
                }
            }


       /* // ✅ Generate Password Reset Token (Simple GUID Token)
        public string GeneratePasswordResetToken(string email)
        {
            var user = _dbContext.UserDetails.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                return null;
            }

            // Generate Token (GUID)
            string resetToken = Guid.NewGuid().ToString();
            user.ResetToken = resetToken;
            user.ResetTokenExpiry = DateTime.UtcNow.AddMinutes(30); // Token valid for 30 minutes

            _dbContext.SaveChanges();
            return resetToken;
        }

        // ✅ Reset Password
        public bool ResetPassword(ResetPasswordModel model)
        {
            var user = _dbContext.UserDetails.FirstOrDefault(u => u.Email == model.Email && u.ResetToken == model.Token);
            if (user == null || user.ResetTokenExpiry < DateTime.UtcNow)
            {
                return false; // Token invalid or expired
            }

            // Hash new password
            string salt = GenerateSalt();
            string hashedPassword = HashPassword(model.NewPassword, salt);

            // Update Password
            user.PasswordHash = hashedPassword;
            user.Salt = salt;
            user.ResetToken = null;
            user.ResetTokenExpiry = null;

            _dbContext.SaveChanges();
            return true;
        }*/

        
    }
}
