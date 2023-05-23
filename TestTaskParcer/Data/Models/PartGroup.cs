namespace TestTaskParcer.Data.Models;

public class PartGroup
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public List<SubGroup> Groups { get; set; } = new List<SubGroup>();
}
