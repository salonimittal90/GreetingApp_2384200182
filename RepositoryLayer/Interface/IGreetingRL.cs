﻿using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IGreetingRL
    {
        public UserEntity SaveGreetings(string message);
       public  UserEntity GetGreetingById(int id);

        public List<UserEntity> GetAllGreetings();

        public UserEntity UpdateGreeting(int id, string newMessage);

        public bool DeleteGreeting (int id);

    }

}
