

namespace OilChangeApp.Domain.Entities;

public class Service_visit
{
    [Key]
    public long Service_id { get; set; }

    [Column("Car_id")]
    public int Car_id { get; set; }
    [ForeignKey(nameof(Car_id))]
    public Car Car { get; set; }
    [Column("Visit_date")]
    public DateTime Visit_date { get; set; }
}