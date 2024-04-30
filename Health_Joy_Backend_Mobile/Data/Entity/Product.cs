namespace Health_Joy_Mobile_Backend.Data.Entity
{
    public class Product
    {
        public int ProductId { get; set; }
        public int BarcodeNo { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public long TotalRiskValue { get; set; }
        public string? ProductType { get; set; }
        public bool IsApprovedByAdmin { get; set; }
        public int UserId { get; set; }
        public virtual User? User { get; set; }

        // Navigation property for many-to-many relationship with Ingredient
        public virtual ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    }
}
