using System;
using System.Collections.Generic;

namespace Security.Data.DBModels;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? OptionsJson { get; set; }

    public bool Active { get; set; }

    public DateTime? LastLogin { get; set; }

    public string? RefreshToken { get; set; }

    public virtual ICollection<UsersRole> UsersRoles { get; set; } = new List<UsersRole>();
}
