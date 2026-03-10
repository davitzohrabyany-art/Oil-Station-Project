namespace OilChangeApp;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
    [Key]
    public int Id { get; set; }
    [Column("Telegram_id")]
    public long TelegramId { get; set; }
    public ICollection<user_Car> UserCars { get; set; }
}