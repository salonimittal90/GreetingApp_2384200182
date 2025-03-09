using ModelLayer.Model.UserModel;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
       public UserDetailsEntity Register(RegisterModel model);  // ✅ Register API ke liye
       public UserDetailsEntity Login(LoginModel model);  // ✅ Login API ke liye (JWT baad me add hoga)

       /* string GeneratePasswordResetToken(string email);
        bool ResetPassword(ResetPasswordModel model);*/
    }
}
