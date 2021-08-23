using System;
using System.Collections.Generic;

#nullable disable

namespace Model.Entities
{
    public partial class Restaurant
    {
        public Restaurant()
        {
            Reviews = new HashSet<Review>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Zipcode { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
