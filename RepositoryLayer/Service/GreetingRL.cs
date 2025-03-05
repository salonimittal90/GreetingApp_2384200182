using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class GreetingRL : IGreetingRL
    {
        HelloGreetingContext _dbContext;

        public GreetingRL(HelloGreetingContext dbContext)
        {
            _dbContext = dbContext;
        }
        public UserEntity SaveGreetings(string message)
        {
            var greeting = new UserEntity { Message = message };
            _dbContext.Greetings.Add(greeting);
            _dbContext.SaveChanges();
            return greeting;
        }
       
    }
}


