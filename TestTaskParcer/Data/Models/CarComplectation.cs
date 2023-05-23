namespace TestTaskParcer.Data.Models;

public class CarComplectation
{
    public int Id { get; set; }
    public string ComplectationId { get; set; } = null!;
    public string? Date { get; set; }
    public string? Engine { get; set; }
    public string? Body { get; set; }
    public string? Grade { get; set; }
    public string? ATM { get; set; }
    public string? Gear { get; set; }
    public string? Position { get; set; }
    public string? Doors { get; set; }
    public string? Destination { get; set; }
    
    public int CarId { get; set; }
    public CarModel Car { get; set; } = new CarModel();
    public List<PartGroup> PartGroups { get; set; } = new List<PartGroup>();
}
