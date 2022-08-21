using System.ComponentModel.DataAnnotations;

namespace Data.Models;

public class Param
{
    [Key] public string Key { get; set; } = null!;
    public decimal Value { get; set; }
}