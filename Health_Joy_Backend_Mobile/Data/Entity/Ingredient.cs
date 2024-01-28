namespace Health_Joy_Mobile_Backend.Data.Entity
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int RiskLevel { get; set; }

        public virtual List<Product>? Products { get; set; }
    }
}
