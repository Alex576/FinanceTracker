using System;
using System.Collections.Generic;

namespace Security.Data.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<UsersRole> UsersRoles { get; set; } = new List<UsersRole>();
}
