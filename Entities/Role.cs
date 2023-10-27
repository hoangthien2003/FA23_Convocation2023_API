using System;
using System.Collections.Generic;

namespace FA23_Convocation2023_API.Entities;

public partial class Role
{
    public string RoleId { get; set; } = null!;

    public string? RoleName { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
