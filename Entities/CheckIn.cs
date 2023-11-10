using System;
using System.Collections.Generic;

namespace FA23_Convocation2023_API.Entities;

public partial class CheckIn
{
    public string? HallName { get; set; }

    public int? SessionNum { get; set; }

    public bool? Status { get; set; }

    public int CheckinId { get; set; }
}
