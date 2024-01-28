namespace Health_Joy_Mobile_Backend.Data.Entity
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int TotalRiskValue { get; set; }
        public string? Image { get; set; }
        public string? ProductType { get; set; }

        public int UserId { get; set; }
        public virtual User? User { get; set; }
        public virtual List<Ingredient>? Ingredients { get; set; }
    }
}

