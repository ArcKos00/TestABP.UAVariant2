using TestTaskParcer.Data.Models;

namespace TestTaskParcer.Repositories.Interfaces;

public interface ISubGroupRepository
{
    public Task<SubGroup> Add(string name, string image, List<Detail> details);
    public Task<SubGroup?> Get(int id);
    public Task<SubGroup?> GetWith(int id);
}
