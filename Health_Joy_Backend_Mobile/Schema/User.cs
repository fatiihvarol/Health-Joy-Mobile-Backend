namespace Health_Joy_Mobile_Backend.Schema;


public class UserRequest
{
    public int UserId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    
   


}

public class UpdateUserModel
{
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }
}

public class LoginResponse
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public int UserId { get; set; }
}
