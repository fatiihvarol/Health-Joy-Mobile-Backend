namespace Health_Joy_Mobile_Backend.Schema;


public class IngredientRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int RiskLevel { get; set; }
}

public class IngredientResponse
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int RiskLevel { get; set; }
}

//bir fotoğraf çekildikten sonra içindekiler listesinin array halinde gelmesi halinde
//bütün arrayin sadece içinde isimler olacak
//bu yüzden sadece ismi alınan bir arraye ihtiyaç duydum (CEREN)

public class IngredientsListReadFromPhoto
{
    public string Name { get; set; }
}
