namespace TestTaskParcer.Data.Models;

public class CarModel
{
    public int Id { get; set; }
    public int CarId { get; set; }
    public string? Name { get; set; }
    public string? Date { get; set; }
    public string? Complectation { get; set; }

    public List<CarComplectation> Complectations { get; set; } = new List<CarComplectation>();
}
