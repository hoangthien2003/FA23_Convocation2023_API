using System;
using System.Collections.Generic;

namespace FA23_Convocation2023_API.Entities;

public partial class CheckIn
{
    public int Id { get; set; }

    public int? BachelorId { get; set; }

    public DateTime? TimeCheckIn1 { get; set; }

    public DateTime? TimeCheckIn2 { get; set; }

    public bool? CheckIn1 { get; set; }

    public bool? CheckIn2 { get; set; }

    public virtual Bachelor? Bachelor { get; set; }
}
