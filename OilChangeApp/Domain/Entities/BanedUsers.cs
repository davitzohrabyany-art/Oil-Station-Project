namespace OilChangeApp.Domain.Entities;

public class BanedUsers
{
    [Key]
    public int BanedTgId { get; set; }
    [Column("ExpiredDate")]
    public DateTime ExpiredDate { get; set; }
}