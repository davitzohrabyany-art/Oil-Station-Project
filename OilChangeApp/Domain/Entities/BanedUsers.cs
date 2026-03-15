namespace OilChangeApp.Domain.Entities;

public class BanedUsers
{
    [Key]
    public long BanedTgId { get; set; }
    [Column("ExpiredDate")]
    public DateTime ExpiredDate { get; set; }
}