using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IGreetingBL  
    {
            public string GetGreeting(string? firstName, string? lastName);
            public UserEntity SaveGreetings(string message);

    }
}

