using TestTaskParcer.Data.Models;

namespace TestTaskParcer.Repositories.Interfaces;

public interface IDetailRepository
{
    public Task<Detail> Add(string treeCode, string tree, List<Infos> info);
    public Task<Detail?> Get(string id);
}
