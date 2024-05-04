using Health_Joy_Mobile_Backend.Data.Entity;

namespace Health_Joy_Mobile_Backend.Schema;


public class ProductRequest
{
    public string? BarcodeNo { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ProductType { get; set; }
    public int UserId { get; set; }
    public virtual List<IngredientRequestCreateProduct>? Ingredients { get; set; }

}

public class ProductResponse
{
    public int ProductId { get; set; }
    public string? BarcodeNo { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public long TotalRiskValue { get; set; }
    public string? ProductType { get; set; }
    public bool IsApprovedByAdmin { get; set; }
    public int UserId { get; set; }
    public virtual ICollection<IngredientResponse>? Ingredients { get; set; }
}

public class ProductUpdateModel
{
    public int ProductId { get; set; }
    public string? BarcodeNo { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public long TotalRiskValue { get; set; }
    public string? ProductType { get; set; }
    public int UserId { get; set; }
    public virtual List<IngredientRequestCreateProduct>? Ingredients { get; set; }
    
}