namespace Health_Joy_Mobile_Backend.Data.Entity
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string FullName { get; set; }
        public bool IsActive { get; set; }
        public string Role { get; set; }

        public virtual ICollection<UserProductFavorite> Favorites { get; set; }
    }
}
