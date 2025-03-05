using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Interface;
<<<<<<< HEAD
=======
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
>>>>>>> UC4

namespace BusinessLayer.Service
{
    public class GreetingBL : IGreetingBL
    {
<<<<<<< HEAD
        public string GetGreeting(string? firstName, string? lastName)
        {
=======
        IGreetingRL _greetingRL;

        public GreetingBL(IGreetingRL greetingRL)
        {
            _greetingRL = greetingRL;
        }

        public UserEntity SaveGreetings(string message)
        {
            var result = _greetingRL.SaveGreetings(message);
            return result;
        }



        public string GetGreeting(string? firstName, string? lastName)
        {
>>>>>>> UC4
            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                return "Hello " + firstName + " " + lastName;
            }

            else if (!string.IsNullOrEmpty(firstName))
            {
                return "Hello " + firstName;
            }
            else if (!string.IsNullOrEmpty(lastName))
            {
                return "Hello " + lastName;
            }
            else
            {
                return "Hello World!";
            }

        }
    }
}