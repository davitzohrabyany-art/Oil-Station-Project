namespace OilChangeApp;
public class Admin
{
    [Key]
    public int Admin_id { get; set; }
    [Column("nickname")]
    public string Nickname { get; set; }
    [Column("password")]
    public string Password { get; set; }
    [Column("oil_type")]
    public string OilType { get; set; }
}