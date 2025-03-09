using ModelLayer.Model.UserModel;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
       public UserDetailsEntity Register(RegisterModel model);  // Database me user save karega
       public UserDetailsEntity Login(LoginModel model);  // Login ke liye user check karega

        string GeneratePasswordResetToken(string email);
        bool ResetPassword(ResetPasswordModel model);

    }
}
