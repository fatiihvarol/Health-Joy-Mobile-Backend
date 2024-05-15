namespace Health_Joy_Mobile_Backend.Data.Entity;

public class UserProductFavorite
{
    public int UserId { get; set; }
    public User User { get; set; }
        
    public int ProductId { get; set; }
    public Product Product { get; set; }
}