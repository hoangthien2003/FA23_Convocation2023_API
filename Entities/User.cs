using System;
using System.Collections.Generic;

namespace FA23_Convocation2023_API.Entities;

public partial class User
{
    public string UserId { get; set; } = null!;

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? RoleId { get; set; }

    public virtual Role? Role { get; set; }
}
