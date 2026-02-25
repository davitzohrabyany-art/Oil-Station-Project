namespace OilChangeApp;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Car
{
    [Key]
    public int Id { get; set; }
    [Column("car_num")]
    public string car_num { get; set; }
    [Column("car_name")]
    public string car_name { get; set; }
    [Column("password")]
    public string password { get; set; }
    [Column("oil_type")]
    public string oil_type { get; set; }
}