using System.ComponentModel.DataAnnotations.Schema;

namespace OilChangeApp;

public class User_Car
{
    [Column("User_id")]
    public int UserId { get; set; }

    [Column("Car_id")]
    public int CarId { get; set; }

    public User User { get; set; }
    public Car Car { get; set; }
}