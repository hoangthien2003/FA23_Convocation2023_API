using System;
using System.Collections.Generic;

namespace FA23_Convocation2023_API.Entities;

public partial class Bachelor
{
    public int Id { get; set; }

    public string StudentCode { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string? Mail { get; set; }

    public string? Faculty { get; set; }

    public string? Major { get; set; }

    public string? Image { get; set; }

    public bool? Status { get; set; }

    public string? StatusBaChelor { get; set; }

    public string? HallName { get; set; }

    public int? SessionNum { get; set; }

    public DateTime? TimeCheckIn1 { get; set; }

    public DateTime? TimeCheckIn2 { get; set; }

    public bool? CheckIn1 { get; set; }

    public bool? CheckIn2 { get; set; }
    public string? Chair { get; set; }
    public string? ChairParent { get; set; }

    public override string? ToString()
    {
        return $"StudentCode = {StudentCode}, " +
            $"FullName = {FullName}, " +
            $"Mail = {Mail}, " +
            $"Major = {Major}, " +
            $"Image = {Image}, " +
            $"Status = {Status}, " +
            $"StatusBachelor = {StatusBaChelor}, " +
            $"HallName = {HallName}, " +
            $"SessionNum = {SessionNum}, " +
            $"TimeCheckin1 = {TimeCheckIn1}, " +
            $"TimeCheckin2 = {TimeCheckIn2}, " +
            $"Checkin1 = {CheckIn1}, " +
            $"Checkin2 = {CheckIn2}, " +
            $"Chair  = {Chair}, " +
            $"ChairParent  = {ChairParent}";
    }
}
