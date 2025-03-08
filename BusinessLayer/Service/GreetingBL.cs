
using BusinessLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;


namespace BusinessLayer.Service
{
    public class GreetingBL : IGreetingBL
    {


        IGreetingRL _greetingRL;

        // constructor
        public GreetingBL(IGreetingRL greetingRL)
        {
            _greetingRL = greetingRL;
        }


        public UserEntity GetGreetingById(int id)
        {
            var result = _greetingRL.GetGreetingById(id);
            return result;
        }

        public UserEntity SaveGreetings(string message)
        {
                var result = _greetingRL.SaveGreetings(message);
                return result;
        }

        public List<UserEntity> GetAllGreetings()
        {
            return _greetingRL.GetAllGreetings();
        }

        public string GetGreeting(string? firstName, string? lastName)
            {

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

        public UserEntity UpdateGreeting(int id, string newMessage)
        {
            var result = _greetingRL.UpdateGreeting(id, newMessage);
            return result;
        }

        public bool DeleteGreeting(int id)
        {
           var result = _greetingRL.DeleteGreeting(id);
            return result;

        }
    }
    }
