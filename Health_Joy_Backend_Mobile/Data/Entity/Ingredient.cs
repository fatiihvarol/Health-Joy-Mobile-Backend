namespace Health_Joy_Mobile_Backend.Data.Entity
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int RiskLevel { get; set; }

        // Navigation property for many-to-many relationship with Product
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
