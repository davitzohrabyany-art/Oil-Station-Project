namespace OilChangeApp.Domain.Entities;

public class Oil_change
{
    [Key]
    public int Oil_id { get; set; }
    [Column("Service_id")]
    public long Service_id { get; set; }
    [ForeignKey(nameof(Service_id))]
    public Service_visit Service_visit { get; set; }
    
    [Column("Oil_name")]
    public string Oil_name { get; set; }
    [Column("Oil_liters")]
    public long Oil_liters { get; set; }
    [Column("Next_change_km")]
    public long Next_change_km { get; set; }
    [Column("Oil_location")]
    public string Oil_location { get; set; }
    [Column("Next_change_date")]
    public DateTime Next_change_date { get; set; }
}