using TestTaskParcer.Data.Models;

namespace TestTaskParcer.Repositories.Interfaces;

public interface IPartGroupRepository
{
    public Task<PartGroup> Add(string name, List<SubGroup> groups);
    public Task<PartGroup?> Get(int id);
    public Task<PartGroup?> GetWith(int id);
}
