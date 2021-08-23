using System;
using System.Collections.Generic;

#nullable disable

namespace Model.Entities
{
    public partial class User
    {
        public User()
        {
            Reviews = new HashSet<Review>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string IsAdmin { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
