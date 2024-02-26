namespace Health_Joy_Mobile_Backend.Schema;


public class UserRequest
{
    public int UserId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    
   


}

public class UserResponse
{
    public string FullName { get; set; }
}