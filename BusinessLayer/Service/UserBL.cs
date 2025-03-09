using BusinessLayer.Interface;
using ModelLayer.Model.UserModel;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Service
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL _userRL;

        public UserBL(IUserRL userRL)
        {
            _userRL = userRL;
        }

        //  Register API
        public UserDetailsEntity Register(RegisterModel model)
        {
            return _userRL.Register(model);
        }

        //  Login API
        public UserDetailsEntity Login(LoginModel model)
        {
            var user = _userRL.Login(model);

            return user; 
        }

        // ✅ Generate Reset Password Token
        /*public string GeneratePasswordResetToken(string email)
        {
            return _userRL.GeneratePasswordResetToken(email);
        }

        // ✅ Reset Password
        public bool ResetPassword(ResetPasswordModel model)
        {
            return _userRL.ResetPassword(model);
        }*/
    }
    }
