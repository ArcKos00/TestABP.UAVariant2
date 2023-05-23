namespace TestTaskParcer.Data.Models;

public class SubGroup
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string ImageName { get; set; } = null!;

    public List<Detail> Details { get; set; } = new List<Detail>();
}   
