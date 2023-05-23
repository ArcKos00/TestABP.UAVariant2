namespace TestTaskParcer.Data.Models;

public class Detail
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public List<Infos> Infos { get; set; } = new List<Infos>();
}
