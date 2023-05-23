using TestTaskParcer.Data.Models;

namespace TestTaskParcer.Repositories.Interfaces
{
    public interface IInfoRepository
    {
        public Task<Infos> Add(string count, string date, string info);
    }
}
