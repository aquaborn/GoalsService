using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pet.Common.Storage
{
    /// <summary>
    /// Класс пользоватлей
    /// </summary>
    public class User:Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public ICollection<Comment> Comments { get; set; }
    
        public User(string firstName, string lastName, string email)
        {
            Id= Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            CreatedAt= DateTime.Now;
            UpdatedAt= DateTime.Now;
        }
    }

}
