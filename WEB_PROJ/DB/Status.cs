using System;
using System.Collections.Generic;

#nullable disable

namespace WEB_PROJ
{
    public partial class Status
    {
        public Status()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
