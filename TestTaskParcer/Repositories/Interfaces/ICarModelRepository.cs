using TestTaskParcer.Data.Models;

namespace TestTaskParcer.Repositories.Interfaces;

public interface ICarModelRepository
{
    public Task<CarModel> Add(int id, string name, string date, string complectation, List<CarComplectation> complectations);
    public Task<CarModel?> Get(int id);
    public Task<CarModel?> GetWith(int id);
}
