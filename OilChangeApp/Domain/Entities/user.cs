namespace OilChangeApp;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
    [Key]
    public int Id { get; set; }
    [Column("Telegram_id")]
    public long TelegramId { get; set; }
    [Column("Phone_number")]
    public string PhoneNumber { get; set; }
    
    
}